using KellermanSoftware.CompareNetObjects; // Compare-NET-Objects
using Newtonsoft.Json; // JSON Serialization

namespace ENTiger.ENCollect;

/// <summary>
/// Provides a method to compare two objects and return
/// the differences as a JSON array of <see cref="FieldChange"/>.
///
/// Configured to:
/// 1) Stop at 100 differences,
/// 2) Ignore collection order,
/// 3) Treat empty string and null as the same,
/// 4) NOT compare child objects deeply (no recursion).
/// </summary>
public static class AuditDiffGenerator
{
    private static readonly CompareLogic _compareLogic = new CompareLogic
    {
        Config =
        {
            MaxDifferences = 100, // Limit # of differences
            IgnoreCollectionOrder = true, // Order in lists doesn't matter
            TreatStringEmptyAndNullTheSame = true, // "" is considered equal to null
            CompareChildren = true // No deep recursion
            // Other defaults remain as-is
        }
    };

    /// <summary>
    /// Compares two objects (top-level only) and returns a JSON array
    /// describing the changed fields. If they are identical (or both null),
    /// returns "[]".
    /// </summary>
    /// <param name="oldObj">The original object state (could be null).</param>
    /// <param name="newObj">The new object state (could be null).</param>
    /// <returns>A JSON string representing an array of FieldChange objects.</returns>
    public static string GenerateDiff(object oldObj, object newObj)
    {
        // If both null, no differences at all.
        if (oldObj == null && newObj == null)
        {
            return "[]";
        }

        // Compare them using Compare-NET-Objects.
        var result = _compareLogic.Compare(oldObj, newObj);

        // If they are equal, return an empty array.
        if (result.AreEqual)
        {
            return "[]";
        }

        // Build our list of FieldChange items from the library's differences.
        var fieldChanges = new List<FieldChange>();
        foreach (var diff in result.Differences)
        {
            fieldChanges.Add(new FieldChange
                (
                    Field: diff.PropertyName,
                    Old: diff.Object1Value,
                    New: diff.Object2Value
                )
            );
        }

        // Return as JSON array.
        return JsonConvert.SerializeObject(fieldChanges);
    }
}