using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ILoanAccountProjectionService
    {
        Task<List<Dictionary<string, object?>>> GetAccountProjectionsAsync(string triggerId, string triggerRunId, List<string> fields, FlexAppContextBridge context);
    }
}
