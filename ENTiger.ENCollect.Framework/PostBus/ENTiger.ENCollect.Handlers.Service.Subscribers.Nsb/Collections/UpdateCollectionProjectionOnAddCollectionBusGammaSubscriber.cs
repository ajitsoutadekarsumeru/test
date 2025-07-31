using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule;

/// <summary>
/// 
/// </summary>
public class UpdateCollectionProjectionOnAddCollectionBusGammaSubscriber : NsbSubscriberBridge<CollectionAddedEvent>
{
    readonly ILogger<UpdateCollectionProjectionOnAddCollectionBusGammaSubscriber> _logger;
    readonly IUpdateCollectionProjectionOnCollectionAdded _subscriber;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public UpdateCollectionProjectionOnAddCollectionBusGammaSubscriber(ILogger<UpdateCollectionProjectionOnAddCollectionBusGammaSubscriber> logger, IUpdateCollectionProjectionOnCollectionAdded subscriber)
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
    public override async Task Handle(CollectionAddedEvent message, IMessageHandlerContext context)
    {
        await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
    }
}
