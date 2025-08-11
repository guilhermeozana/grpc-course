using System.IO.Compression;
using System.Security.Claims;
using Auth;
using FirstGrpc.Interceptors;
using FirstGrpc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(option =>
{
    option.Interceptors.Add<ServerLoggingInterceptor>();
    option.ResponseCompressionAlgorithm = "gzip";
    option.ResponseCompressionLevel = CompressionLevel.SmallestSize;
    //option.CompressionProviders = new List<ICompressionProvider>()
    //{
    //    new GzipCompressionProvider(CompressionLevel.SmallestSize)
    //};
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateActor = false,
        ValidateLifetime = false,
        IssuerSigningKey = JwtHelper.SecurityKey
    });

builder.Services.AddAuthorization(o => o.AddPolicy(JwtBearerDefaults.AuthenticationScheme,
    p =>
    {
        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        p.RequireClaim(ClaimTypes.Name);
    }));

builder.Services.AddGrpcReflection();
builder.Services.AddGrpcHealthChecks(o =>
{
}).AddCheck("my cool service", () => HealthCheckResult.Healthy(), new[] {"grpc", "live"});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<FirstService>();
app.MapGrpcHealthChecksService();
app.MapGrpcReflectionService();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

public partial class Program {}