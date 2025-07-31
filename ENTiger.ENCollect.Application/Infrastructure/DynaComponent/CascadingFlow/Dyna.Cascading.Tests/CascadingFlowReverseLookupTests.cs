using ENCollect.Dyna.Filters;
using ENCollect.Dyna.Cascading;
using System;
using System.Collections.Generic;
using Xunit;

namespace ENCollect.Dyna.CascadingFlow.Tests
{
    public class CascadingFlowReverseLookupTests
    {
        // Sample domain context + condition
        private class FlowContext : IContextDataPacket
        {
            public bool IsSpecialCase { get; set; }
        }

        private class SpecialCaseCondition : IDynaCondition<FlowContext>
        {
            public bool IsMatch(FlowContext domainContext)
            {
                return domainContext.IsSpecialCase;
            }

            public ISearchCriteria GetSearchCriteria()
            {
                // e.g. Branch == "XYZ" for TEntity
                // We'll just do something trivial
                var f = new FilterDefinition("Branch", FilterOperator.Equal, "XYZ");
                return new SearchCriteria(f);
            }

            public bool StopAfterMatch => false;
        }

        private class NormalCaseCondition : IDynaCondition<FlowContext>
        {
            public bool IsMatch(FlowContext domainContext)
            {
                return !domainContext.IsSpecialCase;
            }

            public ISearchCriteria GetSearchCriteria()
            {
                // Branch == "ABCD"
                var f = new FilterDefinition("Branch", FilterOperator.Equal, "ABCD");
                return new SearchCriteria(f);
            }

            public bool StopAfterMatch => false;
        }

        private class Recommender
        {
            public string Branch { get; set; } = string.Empty;
        }

        [Fact]
        public void IsIncluded_Throws_IfNotEvaluated()
        {
            var flow = new CascadingFlow<FlowContext>();
            // no Evaluate() call yet

            Assert.Throws<InvalidOperationException>(() =>
                flow.IsIncluded(new Recommender { Branch="XYZ" }));
        }

        [Fact]
        public void IsIncluded_ReturnsTrueForMatchingCandidate()
        {
            // arrange: we add 2 conditions
            var flow = new CascadingFlow<FlowContext>();
            flow.AddConditions(
                new SpecialCaseCondition(),
                new NormalCaseCondition()
            );

            // evaluate with a FlowContext => let's pick IsSpecialCase = true
            var ctx = new FlowContext { IsSpecialCase = true };
            flow.Evaluate(ctx);
            // that means aggregator => (Branch == "XYZ") from SpecialCaseCondition

            // now let's do reverse-lookup
            var candidate = new Recommender { Branch="XYZ" };

            bool inSet = flow.IsIncluded(candidate);
            Assert.True(inSet);
        }

        [Fact]
        public void IsIncluded_ReturnsFalseIfCandidateDoesNotMatchAggregator()
        {
            var flow = new CascadingFlow<FlowContext>();
            flow.AddConditions(
                new SpecialCaseCondition(),
                new NormalCaseCondition()
            );

            // evaluate => pick IsSpecialCase = true => aggregator => "Branch == XYZ"
            var ctx = new FlowContext { IsSpecialCase = true };
            flow.Evaluate(ctx);

            // candidate => Branch="ABCD" => doesn't match "XYZ"
            var candidate = new Recommender { Branch="ABCD" };

            Assert.False(flow.IsIncluded(candidate));
        }

        [Fact]
        public void IsIncluded_OtherCondition_WhenIsSpecialCaseFalse()
        {
            var flow = new CascadingFlow<FlowContext>();
            flow.AddConditions(new SpecialCaseCondition(), new NormalCaseCondition());

            var ctx = new FlowContext { IsSpecialCase = false };
            flow.Evaluate(ctx);
            // aggregator => from NormalCaseCondition => "Branch == ABCD"

            var c1 = new Recommender { Branch="ABCD" };
            var c2 = new Recommender { Branch="XYZ" };

            Assert.True(flow.IsIncluded(c1));
            Assert.False(flow.IsIncluded(c2));
        }

        [Fact]
        public void IsIncluded_WithPlaceholders()
        {
            // condition => "Branch == $TestBranch"
            var placeholderCondition = new PlaceholderCondition();
            var flow = new CascadingFlow<FlowContext>();
            flow.AddConditions(placeholderCondition);

            // Evaluate => aggregator => "Branch == $TestBranch"
            flow.Evaluate(new FlowContext { IsSpecialCase=false });

            var ctx = new ParameterContext();
            ctx.Set("$TestBranch", "Magic");

            var candidateMatch = new Recommender { Branch="Magic" };
            var candidateNoMatch = new Recommender { Branch="Nothing" };

            Assert.True(flow.IsIncluded(candidateMatch, ctx));
            Assert.False(flow.IsIncluded(candidateNoMatch, ctx));
        }

        private class PlaceholderCondition : IDynaCondition<FlowContext>
        {
            public bool IsMatch(FlowContext domainContext)
            {
                // let's say it's always relevant
                return true;
            }

            public ISearchCriteria GetSearchCriteria()
            {
                return new SearchCriteria(
                    new FilterDefinition("Branch", FilterOperator.Equal, "$TestBranch")
                );
            }

            public bool StopAfterMatch => false;
        }
    }
}