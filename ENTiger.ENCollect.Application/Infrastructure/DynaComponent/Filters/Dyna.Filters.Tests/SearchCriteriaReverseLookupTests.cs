using ENCollect.Dyna.Filters;

namespace ENCollect.DynaFilters.Tests
{
    public class SearchCriteriaReverseLookupTests
    {
        private class TestEntity
        {
            public string Branch { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
        }

        [Fact]
        public void Matches_SimpleEqual_ReturnsTrueForMatchingCandidate()
        {
            // Arrange: Branch == "NY"
            var filter = new FilterDefinition("Branch", FilterOperator.Equal, "NY");
            var sc = new SearchCriteria(filter);

            var candidate1 = new TestEntity { Branch = "NY", IsActive = true };
            var candidate2 = new TestEntity { Branch = "LA", IsActive = true };

            // Act
            bool res1 = sc.Matches(candidate1); // expect true
            bool res2 = sc.Matches(candidate2); // expect false

            // Assert
            Assert.True(res1);
            Assert.False(res2);
        }

        [Fact]
        public void Matches_WithPlaceholder_UsesParamContext()
        {
            // Branch == $TargetBranch
            var filter = new FilterDefinition("Branch", FilterOperator.Equal, "$TargetBranch");
            var sc = new SearchCriteria(filter);

            var ctx = new ParameterContext();
            ctx.Set("$TargetBranch", "LA");

            var candidate1 = new TestEntity { Branch = "NY" };
            var candidate2 = new TestEntity { Branch = "LA" };

            // 'candidate1' => Branch="NY" => false
            // 'candidate2' => Branch="LA" => true
            Assert.False(sc.Matches(candidate1, ctx));
            Assert.True(sc.Matches(candidate2, ctx));
        }

        [Fact]
        public void Matches_NullCandidate_Throws()
        {
            var filter = new FilterDefinition("Branch", FilterOperator.Equal, "NY");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentNullException>(() =>
                sc.Matches<TestEntity>(null!));
        }

        [Fact]
        public void Matches_MultipleFilters_AndLogic()
        {
            // Branch == "NY" && IsActive == true
            var f1 = new FilterDefinition("Branch", FilterOperator.Equal, "NY");
            var f2 = new FilterDefinition("IsActive", FilterOperator.Equal, true);
            var sc = new SearchCriteria(new[] { f1, f2 });

            var c1 = new TestEntity { Branch="NY", IsActive=true };
            var c2 = new TestEntity { Branch="NY", IsActive=false };
            var c3 = new TestEntity { Branch="LA", IsActive=true };

            Assert.True(sc.Matches(c1));
            Assert.False(sc.Matches(c2));
            Assert.False(sc.Matches(c3));
        }

        [Fact]
        public void Matches_NotFilter()
        {
            // NOT(IsActive==true)
            var f = new FilterDefinition("IsActive", FilterOperator.Equal, true);
            var sc = new SearchCriteria().Not(f);

            var activeEntity = new TestEntity { Branch="X", IsActive=true };
            var inactiveEntity = new TestEntity { Branch="Y", IsActive=false };

            Assert.False(sc.Matches(activeEntity));
            Assert.True(sc.Matches(inactiveEntity));
        }
    }
}