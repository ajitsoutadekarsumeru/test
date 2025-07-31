using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ISqlCommandService
    {
        Task ExecuteStoredProcedure(ExecuteSpRequestDto request);
        Task<DataTable> ExecuteStoredProcedure(GetDataRequestDto request);
    }
}
