using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using System.Collections;

namespace ENTiger.ENCollect;

public class DiffGenerator : IDiffGenerator
{
    private readonly CompareLogic _compareLogic;

    public DiffGenerator()
    {
        _compareLogic = new CompareLogic
        {
            Config =
                {
                    MaxDifferences = 100,                  // Limit number of differences
                    IgnoreCollectionOrder = false,          // Order in lists doesn't matter
                    TreatStringEmptyAndNullTheSame = true,   // "" is considered equal to null
                    CompareChildren = true                  // Enable deep recursion for nested objects
                }
        };
    }

    /// <summary>
    /// Compares the old and new objects and returns a minified JSON Patch document.
    /// The output follows RFC 6902 (JSON Patch) with an added "oldValue" for replace operations
    /// and "recordId" for changes inside the "Products" collection.
    /// </summary>
    /// <param name="oldObj">The original object state.</param>
    /// <param name="newObj">The new object state.</param>
    /// <returns>A minified JSON string representing the JSON Patch operations.</returns>
    public string GenerateDiff(object oldObj, object newObj)
    {
        if (oldObj == null && newObj == null)
            return "[]";

        var result = _compareLogic.Compare(oldObj, newObj);
        if (result.AreEqual)
            return "[]";

        var nonCollectionOps = new List<JsonPatchOperation>();
        var groupedChanges = new Dictionary<(string objectType, string recordId), (Dictionary<string, object> oldValues, Dictionary<string, object> newValues)>();

        foreach (var diff in result.Differences)
        {
            string jsonPointer = ConvertPropertyNameToJsonPointer(diff.PropertyName);
            object newValue = diff.Object2Value;
            object oldValue = diff.Object1Value;
            string opType;
            if (oldValue == null && newValue != null)
                opType = "add";
            else if (oldValue != null && (newValue == null || newValue.ToString() == "(null)"))
                opType = "remove";
            else
                opType = "replace";

            // Use the ParentObject1's type name to group collection changes (for uniqueness)
            string objectType = diff.ParentObject1?.GetType().Name ?? string.Empty;
            var segments = jsonPointer.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length >= 3)
            {
                string collectionName = segments[0];
                if (int.TryParse(segments[1], out int index))
                {
                    string recordId = ExtractRecordId(collectionName, index, oldObj, newObj, opType);
                    if (!string.IsNullOrEmpty(recordId))
                    {
                        string changedField = string.Join("/", segments.Skip(2));
                        var key = (objectType, recordId);
                        if (!groupedChanges.ContainsKey(key))
                            groupedChanges[key] = (new Dictionary<string, object>(), new Dictionary<string, object>());
                        if (opType == "replace" || opType == "remove")
                            groupedChanges[key].oldValues[changedField] = oldValue;
                        if (opType == "replace" || opType == "add")
                            groupedChanges[key].newValues[changedField] = newValue;
                        continue;
                    }
                }
            }
            else
            {
                string rootId = ExtractRootId(oldObj, newObj);
                var patchOp = new JsonPatchOperation
                {
                    op = opType,
                    path = jsonPointer,
                    Id = rootId,
                    oldValue = oldValue?.ToString() ?? string.Empty,
                    newValue = newValue?.ToString() ?? string.Empty
                };
                nonCollectionOps.Add(patchOp);
            }
        }

        var groupedOps = new List<JsonPatchOperation>();
        foreach (var kvp in groupedChanges)
        {
            var (objectType, recordId) = kvp.Key;
            // Build a simplified path for the collection record.
            // Here we use the collection property name from one of the changed diffs.
            string path = $"/{objectType}/Id:{recordId}";
            var patchOp = new JsonPatchOperation
            {
                op = "replace",
                path = path,
                Id = recordId,
                oldValue = kvp.Value.oldValues,
                newValue = kvp.Value.newValues
            };
            groupedOps.Add(patchOp);
        }

        var allOps = new List<JsonPatchOperation>();
        allOps.AddRange(nonCollectionOps);

        // Now integrate collection-level differences (additions/removals)
        var root = newObj ?? oldObj;
        if (root != null)
        {
            var collectionProps = root.GetType().GetProperties()
                .Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string));
            foreach (var prop in collectionProps)
            {
                var oldCollection = prop.GetValue(oldObj) as IEnumerable;
                var newCollection = prop.GetValue(newObj) as IEnumerable;
                var collectionOps = CollectionDiffHelper.CompareCollections(prop.Name, oldCollection, newCollection);
                allOps.AddRange(collectionOps);
            }
        }

        allOps.AddRange(groupedOps);

        return JsonConvert.SerializeObject(allOps, Formatting.None);
    }
    /// <summary>
    /// Converts a property name from the Kellerman diff (e.g. "Products[0].Price")
    /// to a JSON pointer (e.g. "/Products/0/Price").
    /// </summary>
    public static string ConvertPropertyNameToJsonPointer(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return "";
        string pointer = propertyName.Replace("[", "/").Replace("]", "");
        pointer = pointer.Replace(".", "/");
        if (!pointer.StartsWith("/"))
            pointer = "/" + pointer;
        return pointer;
    }
    /// <summary>
    /// If the JSON pointer indicates a change inside the "Products" collection,
    /// attempts to extract the product's "Id" property using reflection.
    /// For "remove" operations, the old object is used; for "add"/"replace", the new object is used.
    /// Returns null if not applicable.
    /// </summary>
    public static string ExtractRecordId(string collectionName, int index, object oldObj, object newObj, string opType)
    {
        object rootObject = (opType == "remove") ? oldObj : newObj ?? oldObj;
        if (rootObject == null)
            return string.Empty;
        var collectionProp = rootObject.GetType().GetProperty(collectionName);
        if (collectionProp == null)
            return string.Empty;
        var collection = collectionProp.GetValue(rootObject) as IEnumerable;
        if (collection == null)
            return string.Empty;
        int current = 0;
        foreach (var item in collection)
        {
            if (current == index)
            {
                var idProp = item.GetType().GetProperty("Id");
                if (idProp != null)
                    return idProp.GetValue(item)?.ToString() ?? string.Empty;
            }
            current++;
        }
        return string.Empty;
    }
    /// <summary>
    /// Attempts to extract an entity's "Id" property from a collection based on the JSON pointer.
    /// If the JSON pointer does not reference a collection, returns null.
    /// </summary>
    public static string ExtractRootId(object oldObj, object newObj)
    {
        object root = newObj ?? oldObj;
        if (root == null)
            return string.Empty;
        var idProp = root.GetType().GetProperty("Id");
        if (idProp != null)
        {
            object idVal = idProp.GetValue(root);
            if (idVal != null)
                return idVal.ToString();
        }
        return string.Empty;
    }

}
