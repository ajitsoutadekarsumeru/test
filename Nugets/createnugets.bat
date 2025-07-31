cls
@ECHO OFF
ECHO. ***********************************
ECHO. ** Creating Nugets **
ECHO. ***********************************

dotnet pack ..\ENTiger.ENCollect.Application\EndPoints\ENTiger.ENCollect.EndPoint.CommonConfigs\ENTiger.ENCollect.EndPoint.CommonConfigs.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011   

dotnet pack ..\ENTiger.ENCollect.Application\Domain\ENTiger.ENCollect.CommonLib\ENTiger.ENCollect.CommonLib.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Domain\ENTiger.ENCollect.DomainModels\ENTiger.ENCollect.DomainModels.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Domain\ENTiger.ENCollect.Mappers\ENTiger.ENCollect.Mappers.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Domain\ENTiger.ENCollect.Queries\ENTiger.ENCollect.Queries.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Application\Dto\ENTiger.ENCollect.DTOs\ENTiger.ENCollect.DTOs.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Dto\ENTiger.ENCollect.Messages\ENTiger.ENCollect.Messages.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Dto\ENTiger.ENCollect.SharedEvents\ENTiger.ENCollect.SharedEvents.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Application\Orchestration\ENTiger.ENCollect.CronJobs\ENTiger.ENCollect.CronJobs.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Orchestration\ENTiger.ENCollect.RESTClients\ENTiger.ENCollect.RESTClients.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Orchestration\ENTiger.ENCollect.WebControllers\ENTiger.ENCollect.WebControllers.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Orchestration\ENTiger.ENCollect.Extensions\ENTiger.ENCollect.Extensions.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Application\PostBus\ENTiger.ENCollect.Handlers.Plugins\ENTiger.ENCollect.Handlers.Plugins.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Application\PreBus\ENTiger.ENCollect.PreBus\ENTiger.ENCollect.PreBus.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.SubscriberInterfaces\ENTiger.ENCollect.SubscriberInterfaces.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Framework\Bridge\ENTiger.ENCollect.AspNetCoreBridge\ENTiger.ENCollect.AspNetCoreBridge.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\Bridge\ENTiger.ENCollect.CoreBridge\ENTiger.ENCollect.CoreBridge.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\Bridge\ENTiger.ENCollect.EFCoreBridge\ENTiger.ENCollect.EFCoreBridge.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Bus\ENTiger.ENCollect.Nsb\ENTiger.ENCollect.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Db\ENTiger.ENCollect.BaseEF\ENTiger.ENCollect.BaseEF.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Db\ENTiger.ENCollect.BaseEF.MySql\ENTiger.ENCollect.BaseEF.MySql.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Db\ENTiger.ENCollect.BaseEF.PostgreSql\ENTiger.ENCollect.BaseEF.PostgreSql.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Db\ENTiger.ENCollect.BaseEF.SqlServer\ENTiger.ENCollect.BaseEF.SqlServer.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Default.Nsb\ENTiger.ENCollect.Handlers.Default.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Account.Nsb\ENTiger.ENCollect.Handlers.Account.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Collection.Nsb\ENTiger.ENCollect.Handlers.Collection.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.CollectionProcess.Nsb\ENTiger.ENCollect.Handlers.CollectionProcess.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Common.Nsb\ENTiger.ENCollect.Handlers.Common.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.UM.Nsb\ENTiger.ENCollect.Handlers.UM.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Premium.Nsb\ENTiger.ENCollect.Handlers.Premium.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Default.Subscribers.Nsb\ENTiger.ENCollect.Handlers.Default.Subscribers.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Notification.Subscribers.Nsb\ENTiger.ENCollect.Handlers.Notification.Subscribers.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 
dotnet pack ..\ENTiger.ENCollect.Framework\PostBus\ENTiger.ENCollect.Handlers.Service.Subscribers.Nsb\ENTiger.ENCollect.Handlers.Service.Subscribers.Nsb.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 


dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Utilities\ENTiger.ENCollect.Utilities\ENTiger.ENCollect.Utilities.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011 

dotnet pack ..\ENCollect.APIManagement.RateLimiting\ENCollect.APIManagement.RateLimiting.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011
dotnet pack ..\ENCollect.Compliance.AuditTrail\ENCollect.Compliance.AuditTrail.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011
dotnet pack ..\ENCollect.Security\ENCollect.Security.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011
dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\Http\APIClient\ENTiger.ENCollect.ApiClient.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011
dotnet pack ..\ENTiger.ENCollect.Application\Domain\ENTiger.ENCollect.DomainRepository\ENTiger.ENCollect.DomainRepository.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011

dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\DynaComponent\CascadingFlow\Dyna.Cascading\ENTiger.ENCollect.Dyna.Cascading.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011
dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\DynaComponent\Filters\Dyna.Filters\ENTiger.ENCollect.Dyna.Filters.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011
dotnet pack ..\ENTiger.ENCollect.Application\Infrastructure\DynaComponent\Workflows\Dyna.Workflows\ENTiger.ENCollect.Dyna.Workflows.csproj --configuration Release --output . -p:PackageVersion=1.3.0-Dev-Build011

pause
Utilities