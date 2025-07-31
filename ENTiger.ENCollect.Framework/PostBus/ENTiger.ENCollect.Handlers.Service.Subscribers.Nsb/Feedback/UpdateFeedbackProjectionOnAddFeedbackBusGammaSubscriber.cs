using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule;

/// <summary>
/// 
/// </summary>
public class UpdateFeedbackProjectionOnAddFeedbackBusGammaSubscriber : NsbSubscriberBridge<FeedbackAddedEvent>
{
    readonly ILogger<UpdateFeedbackProjectionOnAddFeedbackBusGammaSubscriber> _logger;
    readonly IUpdateFeedbackProjectionOnFeedbackAdded _subscriber;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public UpdateFeedbackProjectionOnAddFeedbackBusGammaSubscriber(ILogger<UpdateFeedbackProjectionOnAddFeedbackBusGammaSubscriber> logger, IUpdateFeedbackProjectionOnFeedbackAdded subscriber)
    {
        _logger = logger;
        _subscriber = subscriber;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task Handle(FeedbackAddedEvent message, IMessageHandlerContext context)
    {
        await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
    }
}
