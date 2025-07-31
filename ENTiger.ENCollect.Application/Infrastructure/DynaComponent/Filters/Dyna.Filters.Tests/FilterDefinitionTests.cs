using ENCollect.Dyna.Filters;
using System;
using Xunit;

namespace ENCollect.DynaFilters.Tests
{
    public class FilterDefinitionTests
    {
        // 1) PROPERTY NAME VALIDATION

        [Fact]
        public void Constructor_Throws_WhenPropertyNameIsNull()
        {
            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FilterDefinition(null!, FilterOperator.Equal, "someValue"));
        }

        [Fact]
        public void Constructor_Throws_WhenPropertyNameIsEmpty()
        {
            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new FilterDefinition(string.Empty, FilterOperator.Equal, "someValue"));

            Assert.Contains("Property name cannot be empty.", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Constructor_DoesNotThrow_WhenPropertyNameIsValid()
        {
            // Arrange
            string propertyName = "Branch";

            // Act
            var def = new FilterDefinition(propertyName, FilterOperator.Equal, "ABC");

            // Assert
            Assert.Equal(propertyName, def.PropertyName);
            Assert.Equal(FilterOperator.Equal, def.Operator);
            Assert.Single(def.Values);
            Assert.Equal("ABC", def.Values[0]);
        }

        // 2) VALUE VALIDATION

        [Fact]
        public void Constructor_Throws_WhenValueIsNull()
        {
            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FilterDefinition("Branch", FilterOperator.Equal, null!));
        }

        // 3) OPERATOR VALIDATION (We assume your aggregator handles unknown operators,
        //    but let's confirm typical single-value operators.)

        [Theory]
        [InlineData(FilterOperator.Equal)]
        [InlineData(FilterOperator.NotEqual)]
        [InlineData(FilterOperator.GreaterThan)]
        [InlineData(FilterOperator.GreaterOrEqual)]
        [InlineData(FilterOperator.LessThan)]
        [InlineData(FilterOperator.LessOrEqual)]
        [InlineData(FilterOperator.Has)]
        public void Constructor_ValidSingleValueOperator(FilterOperator op)
        {
            // Act
            var def = new FilterDefinition("Branch", op, "someValue");

            // Assert
            Assert.Equal("Branch", def.PropertyName);
            Assert.Equal(op, def.Operator);
            Assert.Single(def.Values);
            Assert.Equal("someValue", def.Values[0]);
        }

        [Fact]
        public void Constructor_UnknownOperator_DoesNotThrowHere_ButMightFailInAggregator()
        {
            // If your aggregator is the place that throws for unknown operators,
            // FilterDefinition itself might not throw. 
            // Or you can choose to throw here if you want.
            // For now, let's show that we do not throw in FilterDefinition.

            var unknownOp = (FilterOperator)999;

            var def = new FilterDefinition("Branch", unknownOp, "XYZ");
            Assert.Equal("Branch", def.PropertyName);
            Assert.Equal(unknownOp, def.Operator);
            Assert.Single(def.Values);
            Assert.Equal("XYZ", def.Values[0]);
        }
    }
}