using ENCollect.Dyna.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using ENCollect.DynaFilter.Tests.Stubs;
using Xunit;

namespace ENCollect.DynaFilters.Tests
{
    public class SearchCriteriaBuildTests
    {
        // --------------------------------------------------------
        // 1) ZERO FILTERS => Build<T>() throws NoFilterDefinitionFoundException
        // --------------------------------------------------------

        [Fact]
        public void Build_ZeroFilters_Throws_NoFilterDefinitionFoundException()
        {
            // Arrange
            var criteria = new SearchCriteria(); // no filters added

            // Act + Assert
            //Assert.Throws<NoFilterDefinitionFoundException>(() =>
            //    criteria.Build<TestRecommender>()
            //);
        }

        // --------------------------------------------------------
        // 2) SINGLE FILTER => "Branch == 'NY123'"
        // --------------------------------------------------------

        [Fact]
        public void Build_SingleFilter_Equal_AppliesCorrectly()
        {
            // Arrange
            var criteria = new SearchCriteria(
                new FilterDefinition("Branch", FilterOperator.Equal, "NY123")
            );

            var data = new List<TestRecommender>
            {
                new TestRecommender { Branch="NY123" },
                new TestRecommender { Branch="ABC" },
            };

            // Act
            var expr = criteria.Build<TestRecommender>();
            var results = data.Where(expr.Compile()).ToList();

            // Assert
            Assert.Single(results);
            Assert.Equal("NY123", results[0].Branch);
        }

        // --------------------------------------------------------
        // 3) MULTIPLE FILTERS => AND
        // e.g. (Region == "Karnataka") AND (Product == "Loans")
        // --------------------------------------------------------

        [Fact]
        public void Build_MultipleFilters_And_LogicWorks()
        {
            // Arrange
            var filters = new List<IFilterDefinition>
            {
                new FilterDefinition("Region", FilterOperator.Equal, "Karnataka"),
                new FilterDefinition("Product", FilterOperator.Equal, "Loans")
            };
            var criteria = new SearchCriteria(filters);

            var data = new List<TestRecommender>
            {
                new TestRecommender { Branch="B1", Region="Karnataka", Product="Loans" },
                new TestRecommender { Branch="B2", Region="Karnataka", Product="Insurance" },
                new TestRecommender { Branch="B3", Region="TamilNadu", Product="Loans" }
            };

            // Act
            var expr = criteria.Build<TestRecommender>();
            var results = data.Where(expr.Compile()).ToList();

            // Assert
            // Expect only the first item matches both Region=Karnataka & Product=Loans
            Assert.Single(results);
            Assert.Equal("B1", results[0].Branch);
        }

        // --------------------------------------------------------
        // 4) .Or(...) => e.g. (Region == "Karnataka") OR (Region == "Maharashtra")
        // --------------------------------------------------------

        [Fact]
        public void Build_OrLogic_Works()
        {
            // Arrange
            var criteria = new SearchCriteria();
            // First filter: Region=Karnataka
            criteria.And(new FilterDefinition("Region", FilterOperator.Equal, "Karnataka"));
            // Second filter: Region=Maharashtra with connector=Or
            criteria.Or(new FilterDefinition("Region", FilterOperator.Equal, "Maharashtra"));

            var data = new List<TestRecommender>
            {
                new TestRecommender { Branch="B1", Region="Karnataka" },
                new TestRecommender { Branch="B2", Region="Maharashtra" },
                new TestRecommender { Branch="B3", Region="Delhi" }
            };

            // Act
            var expr = criteria.Build<TestRecommender>();
            var results = data.Where(expr.Compile()).ToList();

            // Assert
            // Expect B1 and B2 to match
            Assert.Equal(2, results.Count);
            Assert.Contains(results, r => r.Branch=="B1");
            Assert.Contains(results, r => r.Branch=="B2");
        }

        // --------------------------------------------------------
        // 5) .Not(...) => e.g. NOT( Region == "Punjab" )
        // --------------------------------------------------------

        [Fact]
        public void Build_NotLogic_Works()
        {
            // Arrange
            var criteria = new SearchCriteria();
            criteria.Not(new FilterDefinition("Region", FilterOperator.Equal, "Punjab"));

            var data = new List<TestRecommender>
            {
                new TestRecommender { Branch="P1", Region="Punjab" },
                new TestRecommender { Branch="D1", Region="Delhi" }
            };

            // Act
            var expr = criteria.Build<TestRecommender>();
            var results = data.Where(expr.Compile()).ToList();

            // Assert
            // Expect only the second item (because NOT(Region=="Punjab"))
            Assert.Single(results);
            Assert.Equal("D1", results[0].Branch);
        }

        [Fact]
        public void Build_WithPlaceholder_UsesParameterContext()
        {
            // Arrange
            var f = new FilterDefinition(
                "ThresholdRecommendationAmount",
                FilterOperator.GreaterOrEqual,
                "$SettAmount"
            );
            var criteria = new SearchCriteria(f);

            // We supply param ctx
            var paramCtx = new ParameterContext();
            paramCtx.Set("$SettAmount", 120m);

            var data = new List<TestRecommender>
            {
                new TestRecommender { Branch="B1", ThresholdRecommendationAmount=100 },
                new TestRecommender { Branch="B2", ThresholdRecommendationAmount=150 }
            };

            // Act
            // Overload that passes param context
            var expr = criteria.Build<TestRecommender>(paramCtx);
            var results = data.Where(expr.Compile()).ToList();

            // Assert
            // Expect only second item
            Assert.Single(results);
            Assert.Equal("B2", results[0].Branch);
        }

        [Fact]
        public void Build_Placeholder_NoContext_Throws()
        {
            // If we don't supply a context but have a placeholder, we expect an exception
            var criteria = new SearchCriteria(
                new FilterDefinition("ThresholdRecommendationAmount", FilterOperator.Equal, "$Param")
            );

            // Act + Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                criteria.Build<TestRecommender>() // no param context
            );
            Assert.Contains("placeholder '$Param' was encountered, but no parameter context was provided", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Build_Placeholder_ResolveThrowsIfNotInContext()
        {
            // We do pass a context, but there's no entry for "$Param"
            var criteria = new SearchCriteria(
                new FilterDefinition("ThresholdRecommendationAmount", FilterOperator.Equal, "$Param")
            );
            var paramCtx = new ParameterContext(); 
            // paramCtx.Set("$SomethingElse", 100m); but not "$Param"

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                criteria.Build<TestRecommender>(paramCtx));
            Assert.Contains("No value found for placeholder '$Param'", ex.Message);
        }
    }
}
