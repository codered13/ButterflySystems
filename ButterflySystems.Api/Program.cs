using ButterflySystems.Api.Core.Extensions;
using ButterflySystems.Api.Core.Services;
using ButterflySystems.Security.SecurityHeaders;
using CorrelationId;
using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .Enrich.FromLogContext()
    .WriteTo.Console());

// For the future
#region Http Related
builder.Services.AddHttpContextAccessor()
    .AddHttpCacheHeaders()
    .AddDefaultCorrelationId(options =>
    {
        options.AddToLoggingScope = true;
        options.IncludeInResponse = true;
        options.UpdateTraceIdentifier = true;
    })
    .AddHttpClient(string.Empty)
    .AddCorrelationIdForwarding();
#endregion

builder.Services.TryAddTransient<CalculationService>();

builder.Services.AddControllers(opts =>
{
    opts.UseGeneralRoutePrefix("api/");

}).AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ButterflySystems.Api", Version = "v1" });
});

var app = builder.Build();

// We are using Swagger even though it's not in Development mode due to being able to use swagger no matter
// the environment.
#region Swagger Using
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ButterflySystems.Api v1"));
#endregion

app.Use(async (ctx, next) =>
{
    ctx.Response.Headers.TryAdd(HeaderKeyNames.AccessControlExposeHeaders, HeaderKeyNames.DefaultValues.AccessControlExposeHeadersAll);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.AccessControlAllowOrigin, HeaderKeyNames.DefaultValues.AccessControlAllowOriginAll);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.AccessControlAllowHeaders, HeaderKeyNames.DefaultValues.AccessControlAllowHeader);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.AccessControlAllowMethods, HeaderKeyNames.DefaultValues.AccessControlAllowMethod);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.XssProtection, HeaderKeyNames.DefaultValues.XssProtectionBlock);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.NoSniff, HeaderKeyNames.DefaultValues.NoSniffDisable);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.InstanceId, Environment.MachineName);
    ctx.Response.Headers.TryAdd(HeaderKeyNames.TraceIdentifier, ctx.TraceIdentifier);

    await next();
});

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
