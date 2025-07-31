using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class ControllerExtensionMethods
    {
        public static TDto EnrichWithAppContext<TDto>(this TDto dto) where TDto : FlexDto<FlexAppContextBridge>
        {
            IFlexHostHttpContextAccesorBridge _appHttpContextAccessor = FlexContainer.ServiceProvider.GetRequiredService<IFlexHostHttpContextAccesorBridge>();
            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge();
            hostContextInfo.Populate<IFlexHostHttpContextAccesorBridge>(_appHttpContextAccessor);
            dto.SetAppContext(hostContextInfo);
            return dto;
        }
    }
}