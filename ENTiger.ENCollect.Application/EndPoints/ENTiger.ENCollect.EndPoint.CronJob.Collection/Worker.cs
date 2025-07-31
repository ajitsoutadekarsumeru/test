using DocumentFormat.OpenXml.Spreadsheet;
using ENTiger.ENCollect.CollectionsModule;
using Org.BouncyCastle.Ocsp;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        protected IFlexHost _flexHost;
        protected IPushNotificationProvider _serviceProvider;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private IRepoFactory _repoFactory;

        public Worker(ILogger<Worker> logger, IFlexHost flexHost, IPushNotificationProvider serviceProvider)
        {
            _logger = logger;
            _flexHost = flexHost;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            NotificationRequest req = new NotificationRequest
            {
                Recipient = "fU6HxPnwQLWP5y3QK_cK6J:APA91bHYgcCU3MmA4fqBccWs4UeRLz3al7B9_znZ8AzZXkxeFRJMCUGtdJRqSvneyMiFGbFk1cbHqBm6NcQbhNcZt_aK-D6bAA7Pmbky_GlXzHc4LOW4NJQ",
                Title = "test",
                Message = "hi welcome to entiger"
            };

            var utility = _flexHost.GetUtilityService<PushNotificationProviderFactory>("");

            var pushService = utility.GetPushNotificationProvider();

            await pushService.SendNotificationAsync(req);


            //FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            //{
            //    TenantId = "1",
            //    UserId = "1234566",
            //    HostName = "BOB"
            //};
            //string shortCode = "123456";

            //var messageTemplate = _messageTemplateFactory.AccountVerifyTemplate(shortCode, hostContextInfo.HostName);

            //var PaymentOtp = _flexHost.GetUtilityService<PaymentOtpNotification>(hostContextInfo.HostName);
            //PaymentOtp.ConstructData(shortCode);
            //await addCollections();
            //await Task.CompletedTask;
        }

        private async Task addCollections()
        {
            IFlexPrimaryKeyGeneratorBridge _pkGenerator = FlexContainer.ServiceProvider.GetRequiredService<IFlexPrimaryKeyGeneratorBridge>();
            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = "1",
                UserId = "1234566"
            };

            AddCollectionDto dto = new AddCollectionDto()
            {
                //"AmountInWords": "Thirty Three",
                Cheque = new ChequeAPIModel
                {
                    BankCity = "null",
                    BankName = "null",
                    BranchName = "null",
                    IFSCCode = "",
                    InstrumentDate = null,
                    InstrumentNo = "null",
                    MICRCode = "null"
                },
                TransactionNumber = "",
                AccountNo = "1935",
                Amount = 33,
                amountBreakUp1 = 33,
                Cash = new CashAPIModel { },
                CollectionDocs = [],
                CollectionMode = CollectionModeEnum.Cash.Value,
                CustomerName = "SOMNATH VISHNU MATRE",
                DepositAccountNumber = " ",
                DepositBankBranch = " ",
                DepositBankName = "",
                EMailId = "",
                IsDepositAccount = false,
                IsPoolAccount = false,
                Latitude = "37.4219983",
                Longitude = "-122.084",
                MobileNo = "9960075360s",
                othercharges = 0,
                PayerImageName = "null",
                ReceiptNo = "2000008615",
                ReceiptType = "Non-Settlement",
                Settlement = 0,
                yBounceCharges = 0,
                yForeClosureAmount = 0,
                yOverdueAmount = 33,
                yPANNo = "",
                yPenalInterest = 0,
                yRelationshipWithCustomer = "OTHER"
            };
            dto.SetAppContext(hostContextInfo);
            _repoFactory.Init(dto);

            //-----------------------------------------
            //Receipt newreceipt = new Receipt();
            //newreceipt.CustomId = "test" + 2000;
            //newreceipt.CollectorId = "1234566";
            //newreceipt.MarkAsCollectionCollectedByCollector(newreceipt.CollectorId);
            //newreceipt.SetAdded();
            //_repoFactory.GetRepo().InsertOrUpdate(newreceipt);
            //int result = _repoFactory.GetRepo().SaveAsync().GetAwaiter().GetResult();
            //dto.SetGeneratedId(_pkGenerator.GenerateKey());
            //dto.ReceiptNo = newreceipt.CustomId;
            //AddCollectionCommand cmd = new AddCollectionCommand
            //{
            //    Dto = dto,
            //    ReceiptId = newreceipt.Id
            //};
            //---------------------------------------

            IFlexServiceBusBridge _bus = FlexContainer.ServiceProvider.GetRequiredService<IFlexServiceBusBridge>();
            //_bus.Send(cmd).GetAwaiter().GetResult();
            long value = Convert.ToInt32(DateTime.Now.ToString("ddhhmm") + "0000");
            // Start of the new code
            for (int i = 0; i < 1000; i++)
            {
                var tick = DateTime.Now.Ticks;
                _logger.LogInformation("Tick - " + tick);
                long customIdNumber = value + i; // This will increment by 1 with each iteration
                Receipt newreceipt = new Receipt();
                newreceipt.CustomId = "test" + customIdNumber;
                newreceipt.CollectorId = "1234566";
                newreceipt.SetAdded();
                newreceipt.MarkAsCollectionCollectedByCollector(newreceipt.CollectorId);

                _repoFactory.GetRepo().InsertOrUpdate(newreceipt);
                int result = await _repoFactory.GetRepo().SaveAsync();

                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                dto.ReceiptNo = newreceipt.CustomId;
                AddCollectionCommand cmd = new AddCollectionCommand
                {
                    Dto = dto,
                    ReceiptId = newreceipt.Id
                };
                _logger.LogInformation("Send Message : DateTime - " + DateTime.Now + " | CustomId - " + newreceipt.CustomId);
                await _bus.Send(cmd);
            }
        }
    }
}