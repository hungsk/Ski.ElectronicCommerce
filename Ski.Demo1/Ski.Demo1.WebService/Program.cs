using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ski.Base.Util.Models;
using Ski.Base.Util.Services;
using Ski.Demo1.Business;
using Ski.Demo1.Data;
using Ski.Demo1.Domain;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Verbose()
//    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
//    .Enrich.FromLogContext()
//    .WriteTo.Console()
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger();

try
{
    _Log.InfoAsync("開始啟動Ski.ElectronicCommerce應用程式");

    // Add services to the container.

    // appSettings "FunConfig" section -> _Fun.Config
    var config = new ConfigDto();
    builder.Configuration.GetSection("FunConfig").Bind(config);
    _Fun.Config = config;

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //EF Core
    builder.Services.AddDbContext<Demo1DbContext>(option =>
    {
        option.UseSqlServer(builder.Configuration.GetConnectionString("Demo1Database"));
        option.EnableSensitiveDataLogging();
        option.LogTo(Console.WriteLine);
    });

    //UnitOfWork
    builder.Services.AddScoped<IUnitOfWorks, UnitOfWork>();
    builder.Services.AddScoped<IProductLogic, ProductLogic>();
    builder.Services.AddScoped<ICartLogic, CartLogic>();
    builder.Services.AddScoped<IOrderLogic, OrderLogic>();

    //Cors
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.WithOrigins("http://localhost:8080",
                                    "https://localhost:8080",
                                    "http://it221/SkiDemo",
                                    "http://localhost/SkiDemo",
                                    "https://ectest.sk-ins.com.tw/SkiDemo")
                                    .AllowCredentials()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });
    });

    //jwt驗證
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,  //是否驗證超時  當設置exp和nbf時有效
                ValidateIssuerSigningKey = true,  //是否驗證密鑰
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))   //SecurityKey
                                                                                                                        //ValidAudience = "http://localhost:49999",//Audience
                                                                                                                        //ValidIssuer = "http://localhost:49998",//Issuer，這兩項和登入時頒發的一致
                                                                                                                        //緩衝過期時間，總的有效時間等於這個時間加上jwt的過期時間，預設為5分鐘
                                                                                                                        //注意這是緩衝過期時間，總的有效時間等於這個時間加上jwt的過期時間，如果不配置，默認是5分鐘
                                                                                                                        //ClockSkew = TimeSpan.FromMinutes(60)   //設置過期時間
            };
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/error-development");
    }
    else
    {
        //app.UseApiExceptionHandler(options => options.AddResponseDetails = UpdateApiErrorResponse);
        app.UseExceptionHandler("/error");
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors();

    app.UseAuthentication();    //認証
    app.UseAuthorization();     //授權

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    _Log.ErrorAsync("啟動失敗" + ex);
}
finally
{
    _Log.InfoAsync("finally");
}

//void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
//{
//    Log.Error("{ErrorMessage}--{ErrorId}.", ex.StackTrace, error.Id);
//    error.Detail = "發生錯誤" + ex.Message;
//}

