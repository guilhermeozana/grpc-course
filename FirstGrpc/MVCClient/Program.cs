using Auth;
using Basics;
using MVCClient.Interceptors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ClientLoggerInterceptor>();
builder.Services.AddGrpcClient<FirstServiceDefinition.FirstServiceDefinitionClient>(options =>
    {
        options.Address = new Uri("https://localhost:7099");
    })
    .AddCallCredentials((context, metadata) =>
    {
        var token = JwtHelper.GenerateJwtToken("MVC");

        if (!string.IsNullOrEmpty(token))
        {
            metadata.Add("Authorization", $"Bearer {token}");
        }

        return Task.CompletedTask;
    })
    .AddInterceptor<ClientLoggerInterceptor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
