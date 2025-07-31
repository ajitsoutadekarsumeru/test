using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect;
using ENTiger.ENCollect.EndPoint.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Polly.Extensions.Http;
using Polly;
using Serilog;
using Serilog.Events;
using Sumeru.Flex;
using System.Text;   

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
builder.Services.AddControllers(config =>
{
    IHttpRequestStreamReaderFactory readerFactory = builder.Services.BuildServiceProvider().GetRequiredService<IHttpRequestStreamReaderFactory>();
    config.ModelBinderProviders.Insert(0, new FlexOpusBodyModelBinderProvider(config.InputFormatters));
    config.Filters.Add<SpecialCharacterValidationFilter>();
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

//Another approach : TODO - IAuthorizaionPolicyProvider/CustomAuthorizaionPolicy
// Add authorization services
builder.Services.AddAuthorization(options =>
{
    var permissionService = builder.Services.BuildServiceProvider().GetRequiredService<IPermissionService>();

    // Load permissions from the database dynamically
    var permissions = permissionService.GetPermissions();
    foreach (var perm in permissions)
    {
        // Dynamically create the policy using requirements
        var policyName = perm.Name + "Policy";
        options.AddPolicy(policyName, policy => policy.RequireClaim("Permissions", perm.Name));
    }
});


builder.Services.AddExtensions();
builder.Services.ConfigureAPIs();

builder.Services.AddHttpClient<IApiHelper, ApiHelper>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
})
                 .AddPolicyHandler(GetRetryPolicy())
                 .AddPolicyHandler(GetCircuitBreakerPolicy());

var app = builder.Build();

//Enforce license check before any endpoint : (Place it early in the pipeline)
app.UseLicenseValidation();

app.UseCors(MyAllowSpecificOrigins);

#region "Register ServiceProvider to Flex Host"

var serviceScope = app.Services.CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);

#endregion "Register ServiceProvider to Flex Host"

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

//// Global exception handling
//app.UseExceptionHandler("/error/404");

//// Handle invalid URLs and status codes
//app.UseStatusCodePagesWithReExecute("/error/{0}");

app.Run();

#region Polly Policies Configuration

/// <summary>
/// Defines a retry policy that retries 3 times with exponential backoff for transient HTTP errors.
/// </summary>
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
    HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

/// <summary>
/// Defines a circuit breaker policy that breaks the circuit after 5 consecutive errors for 30 seconds.
/// </summary>
static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
    HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

#endregion