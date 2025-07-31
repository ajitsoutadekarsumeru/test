using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTreatmentAccounts : FlexiQueryBridgeAsync<GetTreatmentAccountsDto>
    {
        protected readonly ILogger<GetTreatmentAccounts> _logger;
        protected GetTreatmentAccountsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;

        private readonly IELKUtility _elasticUtility;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetTreatmentAccounts(ILogger<GetTreatmentAccounts> logger, IRepoFactory repoFactory, IOptions<FilePathSettings> fileSettings, IELKUtility elasticUtility, IFileSystem fileSystem)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _elasticUtility = elasticUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetTreatmentAccounts AssignParameters(GetTreatmentAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetTreatmentAccountsDto> Fetch()
        {
            _repoFactory.Init(_params);

            GetTreatmentAccountsDto output = new GetTreatmentAccountsDto();
            List<GetLoanAccountsOutputDto> outputModel = new List<GetLoanAccountsOutputDto>();
            string treatmenthistoryindex = string.Empty;

            var indexdata = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => string.Equals(a.Parameter, "treatmenthistoryindex")).FirstOrDefaultAsync();


            if (indexdata != null)
            {
                treatmenthistoryindex = indexdata.Value;
            }

            string elasticsearchapipath = treatmenthistoryindex + "/_search";
            var client = _elasticUtility.GetElasticConnection();

            string treatmentshistoryid = _elasticUtility.GetFilterTextForElasticSearch(_params.Id);
            string DSLQueryForAllDocs1 = @"

                {
                  ""track_total_hits"": true,
                  ""from"":0,
                  ""size"": 0,
                  ""query"": {
                  ""terms"": {
                        ""treatmenthistoryid"":[
                ";

            DSLQueryForAllDocs1 += $@"
                ""{treatmentshistoryid}""
                 ]
                }}
                }}
                }}
                ";

            var ElkResp = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs1)).GetAwaiter().GetResult();
            string response1 = ElkResp.Body;
            dynamic RootObj1 = JObject.Parse(response1);
            int recordCount = RootObj1.hits.total.value;

            if (recordCount > 0)
            {
                var pages = (recordCount / 5000) + 1;
                _logger.LogInformation("Pages count " + pages);
                for (long page = 0; page < pages; page++)
                {
                    int fromRecord = (int)(page * 5000);
                    _logger.LogInformation("fromRecord count " + fromRecord);
                    string DSLQueryForAllDocs = @"

                        {
                          ""track_total_hits"": true,

                          ""from"":";

                    DSLQueryForAllDocs += $@"
                        ""{fromRecord}"",
                        ""size"": 5000,
                          ""query"": {{
                          ""terms"": {{
                                ""treatmenthistoryid"":[
                        ";

                    DSLQueryForAllDocs += $@"
                        ""{treatmentshistoryid}""
                         ]
                        }}
                        }}
                        }}
                        ";
                    _logger.LogInformation("Inside record count Fetch loanaccounts GetTreatmentLoanAccountFFPlugin " + DSLQueryForAllDocs);
                    var ElkResp1 = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs)).GetAwaiter().GetResult();

                    if (ElkResp1 != null && ElkResp1.Body != null)
                    {
                        string response = ElkResp1.Body;
                        dynamic RootObj = JObject.Parse(response);

                        if (RootObj != null && RootObj.hits != null && RootObj.hits.hits != null)
                        {
                            foreach (var res1 in RootObj.hits.hits)
                            {
                                var res = res1._source;
                                GetLoanAccountsOutputDto model = new GetLoanAccountsOutputDto();
                                model.AccountNumber = res.agreementid;
                                model.CustomerNumber = res.customerid;
                                model.Branch = res.branch;
                                model.State = res.state;
                                model.BOM_POS = (res.bom_pos != null) ? res.bom_pos.ToString() : "";
                                model.CommunicationCityCode = res.city;
                                model.NPAFlag = res.npa_stageid;
                                model.Region = res.region;
                                model.SchemeCode = res.product;
                                model.SubProductName = res.subproduct;
                                model.TotalOverdue = res.tos;
                                model.Zone = res.zone;
                                model.TCallingAgencyName = res.telecallingagencyid;
                                model.AgencyName = res.agencyid;
                                model.TCallingAgentName = res.telecallerid;
                                model.AllocationOwnerName = res.allocationownerid;
                                model.AgentName = res.collectorid;
                                model.TreatmentName = res.treatmentid;
                                model.SegmentName = res.segmentationid;
                                outputModel.Add(model);
                            }
                        }
                    }
                }
            }

            var agencyids = outputModel.Select(a => a.AgencyName).ToList();
            agencyids.AddRange(outputModel.Select(a => a.TCallingAgencyName).ToList());

            var agencies = await _repoFactory.GetRepo().FindAll<Agency>().Where(a => agencyids.Contains(a.Id)).ToListAsync();

            if (agencies.Count > 0)
            {
                outputModel.ForEach(a =>
                {
                    var pagency = agencies.Where(b => b.Id == a.AgencyName).FirstOrDefault();
                    a.AgencyName = pagency != null ? pagency.FirstName : "";

                    var qagency = agencies.Where(b => b.Id == a.TCallingAgencyName).FirstOrDefault();
                    a.TCallingAgencyName = qagency != null ? qagency.FirstName : "";
                });
            }

            var agentids = outputModel.Select(a => a.AgentName).ToList();
            agentids.AddRange(outputModel.Select(a => a.TCallingAgentName).ToList());

            var agents = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => agentids.Contains(a.Id)).ToListAsync();

            if (agents.Count > 0)
            {
                outputModel.ForEach(a =>
                {
                    var pagent = agents.Where(b => b.Id == a.TCallingAgentName).FirstOrDefault();
                    a.TCallingAgentName = pagent != null ? pagent.FirstName + " " + pagent.LastName : "";

                    var qagent = agents.Where(b => b.Id == a.AgentName).FirstOrDefault();
                    a.AgentName = qagent != null ? qagent.FirstName + " " + qagent.LastName : "";
                });
            }

            var userids = outputModel.Select(a => a.AllocationOwnerName).ToList();

            var users = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(a => userids.Contains(a.Id)).ToListAsync();

            if (users.Count > 0)
            {
                outputModel.ForEach(a =>
                {
                    var puser = users.Where(b => b.Id == a.AllocationOwnerName).FirstOrDefault();
                    a.AllocationOwnerName = puser != null ? puser.FirstName + " " + puser.LastName : "";
                });
            }

            var segmentids = outputModel.Select(a => a.SegmentName).ToList();

            var segments = await _repoFactory.GetRepo().FindAll<Segmentation>().Where(a => segmentids.Contains(a.Id)).ToListAsync();

            if (segments.Count > 0)
            {
                outputModel.ForEach(a =>
                {
                    var p = segments.Where(b => b.Id == a.SegmentName).FirstOrDefault();
                    a.SegmentName = p != null ? p.Name : "";
                });
            }

            var treatmentids = outputModel.Select(a => a.TreatmentName).ToList();

            var treatments = await _repoFactory.GetRepo().FindAll<Treatment>().Where(a => treatmentids.Contains(a.Id)).ToListAsync();

            if (treatments.Count > 0)
            {
                outputModel.ForEach(a =>
                {

                    var p = treatments.Where(b => b.Id == a.TreatmentName).FirstOrDefault();
                    a.TreatmentName = p != null ? p.Name : "";

                });
            }

            string filedownloadpath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TrailGapReportPath);
            string filename = "TreatmentHistoryDetails_" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            string filepath = filedownloadpath + "\\" + filename + ".xlsx";

            _logger.LogInformation("After model build");

            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            SheetData sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);

            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                   AppendChild<Sheets>(new Sheets());

            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                   GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "mySheet"
            };
            Row row = new Row() { RowIndex = 1 };
            Cell headers = new Cell() { CellValue = new CellValue("AccountNumber"), DataType = CellValues.String };
            row.Append(headers);
            Cell headers2 = new Cell() { CellValue = new CellValue("CustomerNumber"), DataType = CellValues.String };
            row.Append(headers2);
            Cell headers3 = new Cell() { CellValue = new CellValue("Branch"), DataType = CellValues.String };
            row.Append(headers3);

            Cell headers5 = new Cell() { CellValue = new CellValue("State"), DataType = CellValues.String };
            row.Append(headers5);
            Cell headers6 = new Cell() { CellValue = new CellValue("BOM_POS"), DataType = CellValues.String };
            row.Append(headers6);
            Cell headers7 = new Cell() { CellValue = new CellValue("CommunicationCityCode"), DataType = CellValues.String };
            row.Append(headers7);
            Cell headers8 = new Cell() { CellValue = new CellValue("NPAFlag"), DataType = CellValues.String };
            row.Append(headers8);
            Cell headers9 = new Cell() { CellValue = new CellValue("Region"), DataType = CellValues.String };
            row.Append(headers9);
            Cell headers10 = new Cell() { CellValue = new CellValue("SchemeCode"), DataType = CellValues.String };
            row.Append(headers10);
            Cell headers11 = new Cell() { CellValue = new CellValue("SubProductName"), DataType = CellValues.String };
            row.Append(headers11);
            Cell headers12 = new Cell() { CellValue = new CellValue("TotalOverdue"), DataType = CellValues.String };
            row.Append(headers12);
            Cell headers13 = new Cell() { CellValue = new CellValue("Zone"), DataType = CellValues.String };
            row.Append(headers13);
            Cell headers14 = new Cell() { CellValue = new CellValue("TCallingAgencyName"), DataType = CellValues.String };
            row.Append(headers14);
            Cell headers15 = new Cell() { CellValue = new CellValue("AgencyName"), DataType = CellValues.String };
            row.Append(headers15);
            Cell headers16 = new Cell() { CellValue = new CellValue("TCallingAgentName"), DataType = CellValues.String };
            row.Append(headers16);
            Cell headers17 = new Cell() { CellValue = new CellValue("AllocationOwnerName"), DataType = CellValues.String };
            row.Append(headers17);
            Cell headers18 = new Cell() { CellValue = new CellValue("AgentName"), DataType = CellValues.String };
            row.Append(headers18);
            Cell headers19 = new Cell() { CellValue = new CellValue("TreatmentName"), DataType = CellValues.String };
            row.Append(headers19);
            Cell headers20 = new Cell() { CellValue = new CellValue("SegmentName"), DataType = CellValues.String };
            row.Append(headers20);

            sheetData.Append(row);

            foreach (var result in outputModel)
            {
                DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.AccountNumber); //
                newRow.AppendChild(cell);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell2 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.CustomerNumber); //
                newRow.AppendChild(cell2);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell3 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell3.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell3.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.Branch); //
                newRow.AppendChild(cell3);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell4 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell4.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell4.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.State); //
                newRow.AppendChild(cell4);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell5 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell5.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell5.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.BOM_POS); //
                newRow.AppendChild(cell5);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell6 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell6.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell6.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.CommunicationCityCode); //
                newRow.AppendChild(cell6);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell7 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell7.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell7.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.NPAFlag); //
                newRow.AppendChild(cell7);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell8 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell8.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell8.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.Region); //
                newRow.AppendChild(cell8);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell9 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell9.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell9.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.SchemeCode); //
                newRow.AppendChild(cell9);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell10 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell10.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell10.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.SubProductName); //
                newRow.AppendChild(cell10);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell11 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                string totaloverdue = result.TotalOverdue != null ? result.TotalOverdue.ToString() : "";
                cell11.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell11.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(totaloverdue); //
                newRow.AppendChild(cell11);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell12 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell12.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell12.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.Zone); //
                newRow.AppendChild(cell12);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell13 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell13.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell13.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.TCallingAgencyName); //
                newRow.AppendChild(cell13);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell14 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell14.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell14.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.AgencyName); //
                newRow.AppendChild(cell14);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell15 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell15.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell15.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.TCallingAgentName); //
                newRow.AppendChild(cell15);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell16 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell16.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell16.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.AllocationOwnerName); //
                newRow.AppendChild(cell16);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell17 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell17.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell17.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.AgentName); //
                newRow.AppendChild(cell17);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell18 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell18.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell18.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.TreatmentName); //
                newRow.AppendChild(cell18);

                DocumentFormat.OpenXml.Spreadsheet.Cell cell19 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell19.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell19.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(result.SegmentName); //
                newRow.AppendChild(cell19);

                sheetData.AppendChild(newRow);
            }

            sheets.Append(sheet);

            workbookpart.Workbook.Save();
            spreadsheetDocument.Dispose();

            output.FileName = filename + ".xlsx";

            return output;
        }
    }

    public class GetTreatmentAccountsParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string Id { get; set; }
    }
}