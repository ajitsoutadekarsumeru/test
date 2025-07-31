using ENTiger.ENCollect.FeedbackModule;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.FeedbackModule
{
    public interface IAddTrails : IAmFlexSubscriber<FeedbackAddedEvent>
    {
    }
}
