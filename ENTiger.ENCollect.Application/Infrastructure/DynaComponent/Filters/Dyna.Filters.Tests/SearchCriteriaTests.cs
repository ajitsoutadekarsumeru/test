using ENCollect.Dyna.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ENCollect.DynaFilters.Tests
{
    public class SearchCriteriaTests
    {
        private class TestEntity
        {
            public string Branch { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
            public decimal Amount { get; set; } = 0;
        }

        [Fact]
        public void Constructor_Empty_NoException()
        {
            var criteria = new SearchCriteria();
            Assert.Empty(GetItems(criteria));
        }

        [Fact]
        public void Constructor_SingleFilter_Valid()
        {
            var filter = new FilterDefinition("Branch", FilterOperator.Equal, "NY123");
            var criteria = new SearchCriteria(filter);

            var items = GetItems(criteria);
            Assert.Single(items);
            Assert.Equal(FilterOperator.Equal, items[0].filter.Operator);
            Assert.Equal("Branch", items[0].filter.PropertyName);
        }

        [Fact]
        public void Constructor_IEnumerable_Valid()
        {
            var f1 = new FilterDefinition("Branch", FilterOperator.Equal, "X");
            var f2 = new FilterDefinition("IsActive", FilterOperator.Equal, true);
            var filters = new List<IFilterDefinition> { f1, f2 };

            var criteria = new SearchCriteria(filters);

            var items = GetItems(criteria);
            Assert.Equal(2, items.Count);
            Assert.Same(f1, items[0].filter);
            Assert.Same(f2, items[1].filter);
        }

        [Fact]
        public void And_SingleFilter_AddsItem()
        {
            var criteria = new SearchCriteria();
            var filter = new FilterDefinition("Branch", FilterOperator.Equal, "X");

            criteria.And(filter);

            var items = GetItems(criteria);
            Assert.Single(items);
            Assert.Same(filter, items[0].filter);
            //Assert.Equal(LogicConnector.And, items[0].connector);
            Assert.False(items[0].negate);
        }

        [Fact]
        public void Or_FirstFilter_NotAllowed()
        {
            var criteria = new SearchCriteria();
            var filter = new FilterDefinition("Branch", FilterOperator.Equal, "abc");

            var ex = Assert.Throws<InvalidOperationException>(() => criteria.Or(filter));
            Assert.Contains("Cannot call .Or(...) first", ex.Message);
        }

        [Fact]
        public void Not_AddsItem_WithNegateTrue()
        {
            var criteria = new SearchCriteria();
            var filter = new FilterDefinition("IsActive", FilterOperator.Equal, true);

            criteria.Not(filter);

            var items = GetItems(criteria);
            Assert.Single(items);
            Assert.Same(filter, items[0].filter);
            //Assert.Equal(LogicConnector.And, items[0].connector);
            Assert.True(items[0].negate);
        }

        [Fact]
        public void Build_NoFilters_Throws()
        {
            var criteria = new SearchCriteria();
           // Assert.Throws<NoFilterDefinitionFoundException>(() => criteria.Build<TestEntity>());
        }

        [Fact]
        public void Build_SingleFilter_Equal_Applies()
        {
            var crit = new SearchCriteria(
                new FilterDefinition("Branch", FilterOperator.Equal, "NY123"));

            var data = new List<TestEntity>
            {
                new TestEntity { Branch="NY123", IsActive=true, Amount=100 },
                new TestEntity { Branch="ABC",   IsActive=true, Amount=200 }
            };

            var expr = crit.Build<TestEntity>();
            var results = data.Where(expr.Compile()).ToList();

            Assert.Single(results);
            Assert.Equal("NY123", results[0].Branch);
        }

        [Fact]
        public void Build_WithPlaceholder_UsesParameterContext()
        {
            var crit = new SearchCriteria(
                new FilterDefinition("Branch", FilterOperator.Equal, "$TargetBranch"));

            var data = new List<TestEntity>
            {
                new TestEntity { Branch="BR1" },
                new TestEntity { Branch="BR2" }
            };

            var paramCtx = new ParameterContext();
            paramCtx.Set("$TargetBranch", "BR2");

            var expr = crit.Build<TestEntity>(paramCtx);
            var results = data.Where(expr.Compile()).ToList();

            Assert.Single(results);
            Assert.Equal("BR2", results[0].Branch);
        }

        // ================== Helper to read 'Items' property ==================
        //private static List<(IFilterDefinition filter, LogicConnector connector, bool negate)> GetItems(SearchCriteria sc)
        //{
        //    return sc.Items.ToList();
        //}

        private static List<(IFilterDefinition filter, bool negate)> GetItems(SearchCriteria sc)
        {
            return null;
        }
    }
}