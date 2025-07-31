using ENCollect.Dyna.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ENCollect.DynaFilters.Tests
{
    public class HasOperatorTests
    {
        // ---------------------------------------------------------
        // 1) SAMPLE TEST ENTITIES
        // ---------------------------------------------------------

        private class WorkItem
        {
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
        }

        private class SingleObject
        {
            public string Name { get; set; } = string.Empty;
        }

        // One entity that has a list and a single object
        private class TestEntity
        {
            // List scenario -> e.g. "[WorkItems].City"
            public List<WorkItem> WorkItems { get; set; } = [];

            // Single object -> e.g. "NestedObj.Name"
            public SingleObject? NestedObj { get; set; }
        }

        // ---------------------------------------------------------
        // 2) HELPER: Retrieve aggregator items if needed
        // ---------------------------------------------------------

        //private static List<(IFilterDefinition filter, LogicConnector connector, bool negate)>
        //    GetItems(SearchCriteria sc)
        //{
        //    return sc.Items.ToList();
        //}

        // ---------------------------------------------------------
        // 3) BRACKET SYNTAX TESTS (list scenario)
        // ---------------------------------------------------------

        [Fact]
        public void Has_BracketSyntax_ListProp_Valid()
        {
            // e.g. "[WorkItems].City" => means x => x.WorkItems.Any(item => item.City == ???)
            var filter = new FilterDefinition("[WorkItems].City", FilterOperator.Has, "Bengaluru");
            var criteria = new SearchCriteria(filter);

            // We'll test .Build<TestEntity>() and run on sample data
            var data = new List<TestEntity>
            {
                new TestEntity
                {
                    WorkItems = new List<WorkItem>
                    {
                        new WorkItem{City="Bengaluru"},
                        new WorkItem{City="Mysuru"}
                    }
                },
                new TestEntity
                {
                    WorkItems = new List<WorkItem>
                    {
                        new WorkItem{City="Chennai"},
                        new WorkItem{City="Mysuru"}
                    }
                },
                new TestEntity { WorkItems = null! }, // no items
            };

            var expr = criteria.Build<TestEntity>();
            
            var matched = data.Where(expr.Compile()).ToList();

            // Expect the first entity => has "Bengaluru"
            Assert.Single(matched);
            Assert.Same(data[0], matched[0]); 
        }

        [Fact]
        public void Has_BracketSyntax_NoSubproperty_Throws()
        {
            // e.g. "[WorkItems]" with no dot => invalid
            var filter = new FilterDefinition("[WorkItems]", FilterOperator.Has, "Bengaluru");

            Assert.Throws<InvalidCastException>(() =>
            {
                var c = new SearchCriteria(filter);
                c.Build<TestEntity>();
            });
        }

        [Fact]
        public void Has_BracketSyntax_NoClosingBracket_Throws()
        {
            var filter = new FilterDefinition("[WorkItems.City", FilterOperator.Has, "Bengaluru");
            Assert.Throws<ArgumentException>(() =>
            {
                var c = new SearchCriteria(filter);
                c.Build<TestEntity>();
            });
        }

        [Fact]
        public void Has_BracketSyntax_PropertyNotFound_Throws()
        {
            // "[BogusList].City" => property doesn't exist
            var filter = new FilterDefinition("[BogusList].City", FilterOperator.Has, "X");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentException>(() =>
                sc.Build<TestEntity>());
        }

        [Fact]
        public void Has_BracketSyntax_Subproperty_UnsupportedProperty_Throws()
        {
            // "[WorkItems].Bogus" => subproperty not found on WorkItem
            var filter = new FilterDefinition("[WorkItems].Bogus", FilterOperator.Has, "X");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentException>(() =>
                sc.Build<TestEntity>());
        }

        // Additional scenario: the property is not a List, but we used bracket => should throw
        [Fact]
        public void Has_BracketSyntax_IsntAList_Throws()
        {
            // "[NestedObj].Name" => but NestedObj is a single object, not a List
            var filter = new FilterDefinition("[NestedObj].Name", FilterOperator.Has, "Bob");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentException>(() =>
                sc.Build<TestEntity>());
        }

        // ---------------------------------------------------------
        // 4) SINGLE OBJECT TESTS (dot syntax, no bracket)
        // ---------------------------------------------------------

        [Fact]
        public void Has_SingleObject_NestedObj_Valid()
        {
            // "NestedObj.Name" => x => x.NestedObj != null && x.NestedObj.Name == value
            var filter = new FilterDefinition("NestedObj.Name", FilterOperator.Has, "Master");
            var sc = new SearchCriteria(filter);

            var data = new List<TestEntity>
            {
                new TestEntity
                {
                    NestedObj = new SingleObject{ Name="Master" }
                },
                new TestEntity
                {
                    NestedObj = new SingleObject{ Name="Apprentice" }
                },
                new TestEntity
                {
                    NestedObj = null!
                }
            };

            var expr = sc.Build<TestEntity>();
            var matched = data.Where(expr.Compile()).ToList();

            // expect the first entity => "Master"
            Assert.Single(matched);
            Assert.Same(data[0], matched[0]);
        }

        [Fact]
        public void Has_SingleObject_NoDot_Throws()
        {
            // "NestedObj" => we have no subproperty
            var filter = new FilterDefinition("NestedObj", FilterOperator.Has, "Master");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentException>(() =>
                sc.Build<TestEntity>());
        }

        [Fact]
        public void Has_SingleObject_ParentNotFound_Throws()
        {
            // "Bogus.Name" => 'Bogus' property doesn't exist
            var filter = new FilterDefinition("Bogus.Name", FilterOperator.Has, "X");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentException>(() =>
                sc.Build<TestEntity>());
        }

        [Fact]
        public void Has_SingleObject_SubpropertyNotFound_Throws()
        {
            // "NestedObj.Bogus" => no property named 'Bogus' on SingleObject
            var filter = new FilterDefinition("NestedObj.Bogus", FilterOperator.Has, "whatever");
            var sc = new SearchCriteria(filter);

            Assert.Throws<ArgumentException>(() =>
                sc.Build<TestEntity>());
        }

        [Fact]
        public void Has_SingleObject_NullParent_NotMatched()
        {
            // If NestedObj is null, we do x => x.NestedObj != null && x.NestedObj.Name== val => false
            var filter = new FilterDefinition("NestedObj.Name", FilterOperator.Has, "Master");
            var sc = new SearchCriteria(filter);

            var data = new List<TestEntity>
            {
                new TestEntity
                {
                    NestedObj = new SingleObject{ Name="Master" }
                },
                new TestEntity
                {
                    NestedObj = null!
                }
            };

            var expr = sc.Build<TestEntity>();
            var matched = data.Where(expr.Compile()).ToList();

            // the second entity => NestedObj is null => won't match
            Assert.Single(matched);
            Assert.Same(data[0], matched[0]);
        }

        // ---------------------------------------------------------
        // 5) MULTI-DOT SCENARIOS => Should Throw
        // ---------------------------------------------------------

        [Fact]
        public void Has_MultipleDots_Throws()
        {
            // e.g. "NestedObj.SomethingElse.Name"
            // your code only handles one dot for single object, or bracket + one dot for list.
            var filter = new FilterDefinition("NestedObj.SomethingElse.Name", FilterOperator.Has, "X");
            var sc = new SearchCriteria(filter);

            // Assuming your code has "Multiple dots not supported" or NotImplemented
            Assert.Throws<NotImplementedException>(() =>
                sc.Build<TestEntity>());
        }

        [Fact]
        public void Has_BracketSyntax_MultipleDots_Throws()
        {
            // e.g. "[WorkItems].SomeNestedObj.Name" => 2 dots
            var filter = new FilterDefinition("[WorkItems].SomeNestedObj.Name", FilterOperator.Has, "X");
            var sc = new SearchCriteria(filter);

            Assert.Throws<NotImplementedException>(() =>
                sc.Build<TestEntity>());
        }
    }
}