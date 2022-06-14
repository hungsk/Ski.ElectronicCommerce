using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Ski.HealthChecks.WebService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"], name: "SQL Server HealthCheck")
    .AddRedis(builder.Configuration["ConnectionStrings:RedisConn"], name: "Redis HealthCheck")
    .AddCheck<MemoryHealthCheck>("Memory Health Check")
    .AddUrlGroup(new Uri(builder.Configuration["ConnectionStrings:EcTest"]), "EcTest HealthCheck", HealthStatus.Degraded)
    .AddUrlGroup(new Uri(builder.Configuration["ConnectionStrings:Sk858"]), "Sk858 HealthCheck-", HealthStatus.Degraded)
    .AddUrlGroup(new Uri(builder.Configuration["ConnectionStrings:Member"]), "Member HealthCheck", HealthStatus.Degraded)
    .AddUrlGroup(new Uri(builder.Configuration["ConnectionStrings:MisService"]), "MisService HealthCheck", HealthStatus.Degraded);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        //var json = JsonConvert.SerializeObject(new
        //        {
        //            status = report.Status.ToString(),
        //            errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
        //        });
        var json = JsonConvert.SerializeObject(report);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(json);
    }
});

//app
//    .UseRouting()
//    .UseEndpoints(config => config.MapHealthChecksUI());

app.UseHealthChecks("/hc", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/hc-ui";
});


app.Run();
