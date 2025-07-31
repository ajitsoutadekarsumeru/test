using Newtonsoft.Json;

namespace ENTiger.ENCollect;

// Simple nested classes used in scenario #8
internal class Address
{
    public string Street { get; set; }
}

internal class Person
{
    public string Name { get; set; }
    public Address Address { get; set; }
}

public class AuditDiffGeneratorTests
{
    /// <summary>
    /// SCENARIO 2:
    /// Objects are identical (no differences).
    /// Expect: "[]"
    /// </summary>
    [Fact]
    public void IdenticalObjects_ReturnsEmptyArray()
    {
        // Arrange
        var oldObj = new { Id = 1, Name = "Alice", Age = 30 };
        var newObj = new { Id = 1, Name = "Alice", Age = 30 };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldObj, newObj);

        // Assert
        Assert.Equal("[]", diffJson);
    }

    /// <summary>
    /// SCENARIO 3:
    /// Single property difference.
    /// Expect a JSON array with exactly one FieldChange.
    /// </summary>
    [Fact]
    public void SinglePropertyDifference_ReturnsOneFieldChange()
    {
        // Arrange
        var oldObj = new { Id = 2, Name = "Bob" };
        var newObj = new { Id = 2, Name = "Bobby" };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldObj, newObj);
        var changes = JsonConvert.DeserializeObject<List<FieldChange>>(diffJson);

        // Assert
        Assert.NotNull(changes);
        Assert.Single(changes);

        var change = changes[0];
        Assert.Equal("Name", change.Field);
        Assert.Equal("Bob", change.Old);
        Assert.Equal("Bobby", change.New);
    }

    /// <summary>
    /// SCENARIO 4:
    /// Multiple property differences.
    /// Expect a JSON array with multiple FieldChange objects.
    /// </summary>
    [Fact]
    public void MultipleDifferences_ReturnsMultipleFieldChanges()
    {
        // Arrange
        var oldObj = new { Id = 3, Name = "Carol", Email = "old@example.com" };
        var newObj = new { Id = 3, Name = "Caroline", Email = "new@example.com" };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldObj, newObj);
        var changes = JsonConvert.DeserializeObject<List<FieldChange>>(diffJson);

        // Assert
        Assert.NotNull(changes);
        Assert.Equal(2, changes.Count);

        // Because we do not guarantee an order in the differences, we check them by property name.
        var nameChange = changes.Find(c => c.Field == "Name");
        Assert.NotNull(nameChange);
        Assert.Equal("Carol", nameChange.Old);
        Assert.Equal("Caroline", nameChange.New);

        var emailChange = changes.Find(c => c.Field == "Email");
        Assert.NotNull(emailChange);
        Assert.Equal("old@example.com", emailChange.Old);
        Assert.Equal("new@example.com", emailChange.New);
    }

    /// <summary>
    /// SCENARIO 5:
    /// Empty string vs. null (with TreatStringEmptyAndNullTheSame=true).
    /// Should be considered no difference at top-level.
    /// Expect: "[]"
    /// </summary>
    [Fact]
    public void EmptyStringAndNull_ReturnsNoDifferences()
    {
        // Arrange
        var oldObj = new { Name = "" };
        var newObj = new { Name = (string?)null };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldObj, newObj);

        // Assert
        Assert.Equal("[]", diffJson);
    }

    /// <summary>
    /// SCENARIO 6:
    /// Case sensitivity check.
    /// By default, Compare-NET-Objects is case-sensitive
    /// unless we explicitly set Config.CaseSensitive = false.
    /// Here, we assume it's 'true', so "John" vs "john" is a difference.
    /// </summary>
    [Fact]
    public void CaseDifferenceIsDetected_WhenCaseSensitive()
    {
        // Arrange
        var oldObj = new { Name = "John" };
        var newObj = new { Name = "john" };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldObj, newObj);
        var changes = JsonConvert.DeserializeObject<List<FieldChange>>(diffJson);

        // Assert
        // Because "John" != "john" in a case-sensitive context,
        // we expect a single difference.
        Assert.NotNull(changes);
        Assert.Single(changes);

        var change = changes[0];
        Assert.Equal("Name", change.Field);
        Assert.Equal("John", change.Old);
        Assert.Equal("john", change.New);
    }

    /// <summary>
    /// SCENARIO 7:
    /// Ignore collection order at the top-level.
    /// If two arrays contain the same elements in different orders,
    /// we consider them no difference.
    /// </summary>
    [Fact]
    public void CollectionOrderIsIgnored_ReturnsNoDifferences()
    {
        // Arrange
        // We'll keep the property name the same in both objects, e.g. "Numbers",
        // with the same set of numbers in different order.
        var oldObj = new { Numbers = new[] { 1, 2, 3 } };
        var newObj = new { Numbers = new[] { 3, 2, 1 } };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldObj, newObj);

        // Assert
        Assert.Equal("[]", diffJson);
    }

    /// <summary>
    /// SCENARIO 8:
    /// No deep comparison of child objects.
    /// If the child references differ, we get one difference for the property,
    /// but we do NOT see sub-property differences like Street changing.
    /// </summary>
    [Fact]
    public void NoDeepComparison_ChildObjectChanges_NoDifferencesReported()
    {
        // Arrange: both Person objects have the same top-level property name "Address",
        // but the actual Street is different. Because CompareChildren=false,
        // changes to Street do not register at the top level.
        var oldPerson = new Person
        {
            Name = "Alice",
            Address = new Address { Street = "123 Main" }
        };
        var newPerson = new Person
        {
            Name = "Alice",
            Address = new Address { Street = "456 Elm" }
        };

        // Act
        var diffJson = AuditDiffGenerator.GenerateDiff(oldPerson, newPerson);
        var changes = JsonConvert.DeserializeObject<List<FieldChange>>(diffJson);

        // Assert
        // Because only a child property changed (Street),
        // and we have CompareChildren=false, we expect an empty array.
        Assert.Equal("[]", diffJson);
        Assert.NotNull(changes);
        Assert.Empty(changes);
    }
}