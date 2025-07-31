using System.Collections;

namespace ENTiger.ENCollect;
public static class CollectionDiffHelper
{
    public static List<JsonPatchOperation> CompareCollections(string collectionName, IEnumerable oldCollectionObj, IEnumerable newCollectionObj)
    {
        var ops = new List<JsonPatchOperation>();

        var oldDict = new Dictionary<string, object>();
        if (oldCollectionObj != null)
        {
            foreach (var item in oldCollectionObj)
            {
                var idProp = item.GetType().GetProperty("Id");
                if (idProp != null)
                {
                    string id = idProp.GetValue(item)?.ToString();
                    if (!string.IsNullOrEmpty(id))
                        oldDict[id] = item;
                }
            }
        }

        var newDict = new Dictionary<string, object>();
        if (newCollectionObj != null)
        {
            foreach (var item in newCollectionObj)
            {
                var idProp = item.GetType().GetProperty("Id");
                if (idProp != null)
                {
                    string id = idProp.GetValue(item)?.ToString();
                    if (!string.IsNullOrEmpty(id))
                        newDict[id] = item;
                }
            }
        }

        // Identify removals.
        foreach (var kvp in oldDict)
        {
            if (!newDict.ContainsKey(kvp.Key))
            {
                ops.Add(new JsonPatchOperation
                {
                    op = "remove",
                    path = $"/{kvp.Value}/",
                    Id = kvp.Key,
                    oldValue = null,
                    newValue = null
                });
            }
        }

        // Identify additions.
        foreach (var kvp in newDict)
        {
            if (!oldDict.ContainsKey(kvp.Key))
            {
                ops.Add(new JsonPatchOperation
                {
                    op = "add",
                    path = $"/{kvp.Value}/",
                    Id = kvp.Key,
                    oldValue = null,
                    newValue = null
                });
            }
        }

        return ops;
    }
}
