//using ENTiger.ENCollect;
//using NServiceBus;

//namespace ENCollect.Dyna.Workflows
//{
//    public class RecordPotentialActorsHandler 
//        : IHandleMessages<RecordPotentialActorsCommand>
//    {
//        private readonly IPotentialActorsReadModel _readModel;
//        // or IPotentialActorsWriter if you prefer separation of read vs. write

//        public RecordPotentialActorsHandler(IPotentialActorsReadModel readModel)
//        {
//            _readModel = readModel;
//        }

//        public Task Handle(RecordPotentialActorsCommand message, IMessageHandlerContext context)
//        {
//            _readModel.AddActors(
//                message.DomainId,
//                message.WorkflowName,
//                message.StepIndex,
//                message.EligibleUserIds
//            );

//            return Task.CompletedTask;
//        }
//    }
//}