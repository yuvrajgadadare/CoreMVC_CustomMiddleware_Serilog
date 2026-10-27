using CoreMVC_CustomMiddleware_Serilog.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/app-exceptions.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] (Thread:{ThreadId}) {Message:lj}{NewLine}{Exception}"
    ).CreateLogger();

builder.Host.UseSerilog();


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
app.UseMiddleware<ExceptionLogMiddleware>();
app.MapStaticAssets();

//app.Use(async (context, next) =>
//{
//    int a = 10, b = 2, c;
//    c = a / b;
//   await context.Response.WriteAsync($"Div={c}");
//    await next();
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
