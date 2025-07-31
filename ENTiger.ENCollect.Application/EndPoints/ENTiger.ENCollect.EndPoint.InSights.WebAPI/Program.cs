using System.Text;
using ENTiger.ENCollect;
using ENTiger.ENCollect.DomainModels.Utilities.ELK_Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Sumeru.Flex;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Host.ConfigureCommonHost
(
    new ConfigureEndPointHostParams
    {
        HostName = "EndPoint.WebAPI",
        Routes = NsbRouteConfig.GetRoute(),
        SearchPattern = "ENTiger.ENCollect*.dll"
    }
);

builder.Services.AddFlexBaseAspNetCoreServices();


// Add services to the container.

//builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers(config =>
{
    IHttpRequestStreamReaderFactory readerFactory = builder.Services.BuildServiceProvider().GetRequiredService<IHttpRequestStreamReaderFactory>();
    config.ModelBinderProviders.Insert(0, new FlexOpusBodyModelBinderProvider(config.InputFormatters));
    config.Filters.Add<FlexStandardResponseFilter>();
    config.Filters.Add<FlexStandardExceptionFilter>();    
}).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();

//TODO: Configure your own CORS rule
builder.Services.AddCors(options =>
{
    // Get the list of allowed origins from configuration
    var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins(allowedOrigins).AllowAnyHeader()
                    .AllowAnyMethod().WithExposedHeaders("X-Pagination");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});


builder.Services.AddExtensions();

builder.Services.ConfigureAPIs();

builder.Configuration.AddJsonFile($"elasticindexnames.json", optional: false, reloadOnChange: true);


var app = builder.Build();

// Enforce license check before any endpoint : (Place it early in the pipeline)
app.UseLicenseValidation();

app.UseCors(MyAllowSpecificOrigins);

#region "Register ServiceProvider to Flex Host"
var serviceScope = app.Services.CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);
#endregion

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Logging with unique id
app.EnableFlexCorrelationWithSerilog();

//Configure serilog to log http requests - Route Logging
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "Handled {HttpRequest} {RequestMethod} {RequestPath} from {RequestHost} {UserIP} User {UserName} responded {StatusCode} in {Elapsed:0.0000} ms for CorrelationId {CorrelationId}";
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var userName = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? httpContextAccessor.HttpContext.User.Identity.Name : "Anonymous";

        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"]);
        diagnosticContext.Set("UserIP", httpContext.Connection.RemoteIpAddress?.ToString());
        diagnosticContext.Set("UserName", userName);
        diagnosticContext.Set("HttpRequest", "HttpRequest");
    };
});

app.UseHttpsRedirection();
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



