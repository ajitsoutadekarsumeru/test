using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(AddFeedbackCommand cmd)
        {
            await _bus.Send(cmd);

        }

    }
}
