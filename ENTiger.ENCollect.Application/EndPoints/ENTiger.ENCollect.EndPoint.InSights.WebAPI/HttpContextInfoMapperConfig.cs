using AutoMapper.Configuration;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpContextInfoMapperConfig : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpContextInfoMapperConfig() : base()
        {
            #region Input
            CreateMap<FlexDefaultHttpContextAccessorBridge, FlexAppContextBridge>();
            #endregion
        }



    }
}
