using ENCollect.Dyna.Workflows;
using ENTiger.ENCollect.SettlementModule;


namespace ENTiger.ENCollect
{
    public static class SettlementWorkflowDefinitionFactory
    {
        public static DynaWorkflowDefinition<SettlementContext> Create()
        {
            var definition = new DynaWorkflowBuilder<SettlementContext>()
                .Version("1.0")
                .Named("SettlementWorkflow")

            #region RECOMMENDATIONS

                 .Step("Pending Recommendation1")
                    .WithTransitions("Pending Recommendation2",
                        "Pending Renegotiation",
                        "Denied",
                        "Cancelled")
                    .WithUIActionContext(UIActionContext.Recommendation)
                    .IsNeeded(_ => true)
                    .WithActorFlow(flow =>
                    {
                        // Single condition that does multiple sub-rules => L1 or L2
                        flow.AddConditions(new RequestorLevelCondition());
                    })
                .EndStep()

                .Step("Pending Recommendation2")
                    .WithTransitions("Pending Approval",
                        "Pending Renegotiation",
                        "Denied",
                        "Cancelled")
                    .WithUIActionContext(UIActionContext.Recommendation)
                    .IsNeeded(_ => true)
                    .WithActorFlow(flow =>
                    {
                        // Single condition that does multiple sub-rules => L1 or L2
                        flow.AddConditions(new RecommenderLevelCondition());
                    })
                .EndStep()

                .Step("Denied") // recommender denied → end
                .WithTransitions()
                    .IsNeeded(_ => true)
                .End()

            #endregion

            #region APPROVAL

                .Step("Pending Approval")
                    .WithTransitions("Pending Customer Acceptance",
                        "Pending Renegotiation",
                        "Rejected",
                        "Cancelled")
                    .WithUIActionContext(UIActionContext.Approval)
                    .IsNeeded(_ => true)
                    .WithActorFlow(flow =>
                    {                        
                        flow.AddConditions(new ApproverLevelCondition());
                    })
                .EndStep()

                .Step("Rejected") // approver rejected → end
                .WithTransitions()
                    .IsNeeded(_ => true)

                .End()

            #endregion

            #region RENEGOTIATION_LOOP

                .Step("Pending Renegotiation")
                    .WithTransitions(
                        "Pending Recommendation1", // success                       
                        "Cancelled") // failure
                    .WithUIActionContext(UIActionContext.Renegotiate)
                    .IsNeeded(_ => true)
                    .WithActorFlow(flow =>
                    {
                        flow.AddConditions(new RenegotiationCondition());
                    })
                .EndStep()

                .Step("Cancelled") // renegotiation failed → end
                .WithTransitions()
                    .IsNeeded(_ => true)
                .End()


            #endregion

            #region CUSTOMER_ACCEPTANCE

                .Step("Pending Customer Acceptance")
                    .WithTransitions("Closed", "Cancelled")
                    .WithUIActionContext(UIActionContext.Acceptance) // waiting for customer
                    .IsNeeded(_ => true)
                    .WithActorFlow(flow =>
                    {
                        flow.AddConditions(new CustomerAcceptanceCondition());
                    })
                .EndStep()

                .Step("Closed") // settlement accepted & closed
                .WithTransitions()
                    .IsNeeded(_ => true)
                .End()


            #endregion

                .Build();
            
            // Map ActionType => domain command type
            definition.ActionCommandMap[ActionType.Recommend] = typeof(ProcessSettlementRecommendationCommand);
            definition.ActionCommandMap[ActionType.Deny]      = typeof(ProcessSettlementDenyCommand);
            definition.ActionCommandMap[ActionType.Approve]   = typeof(ProcessSettlementApprovalCommand);
            definition.ActionCommandMap[ActionType.Reject]    = typeof(ProcessSettlementRejectCommand);
            definition.ActionCommandMap[ActionType.CustomerAcceptance] = typeof(ProcessSettlementCustomerAcceptanceCommand);
            definition.ActionCommandMap[ActionType.CustomerReject] = typeof(ProcessSettlementCustomerRejectCommand);
            definition.ActionCommandMap[ActionType.Renegotiate] = typeof(ProcessSettlementNegotiateCommand);
            definition.ActionCommandMap[ActionType.Update] = typeof(UpdateSettlementCommand);
            definition.ActionCommandMap[ActionType.Cancel] = typeof(CancelSettlementCommand);
            //Settlement -> Update
            return definition;
        }
    }
}