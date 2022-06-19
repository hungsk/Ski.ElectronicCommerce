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
    _Log.InfoAsync("Start Ski.ElectronicCommerce");

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
        //option.UseSqlServer(builder.Configuration.GetConnectionString("Demo1Database"));
        option.UseSqlite("Data Source=../ski.demo1.data/Sqlite/Demo1.db");
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

    //jwt����
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,  //�O�_���ҶW��  ���]�mexp�Mnbf�ɦ���
                ValidateIssuerSigningKey = true,  //�O�_���ұK�_
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))   //SecurityKey
                                                                                                                        //ValidAudience = "http://localhost:49999",//Audience
                                                                                                                        //ValidIssuer = "http://localhost:49998",//Issuer�A�o�ⶵ�M�n�J�ɹ{�o���@�P
                                                                                                                        //�w�ĹL���ɶ��A�`�����Įɶ�����o�Ӯɶ��[�Wjwt���L���ɶ��A�w�]��5����
                                                                                                                        //�`�N�o�O�w�ĹL���ɶ��A�`�����Įɶ�����o�Ӯɶ��[�Wjwt���L���ɶ��A�p�G���t�m�A�q�{�O5����
                                                                                                                        //ClockSkew = TimeSpan.FromMinutes(60)   //�]�m�L���ɶ�
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

    app.UseAuthentication();    //�{��
    app.UseAuthorization();     //���v

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    _Log.ErrorAsync("Error" + ex);
}
finally
{
    _Log.InfoAsync("Finally");
}

//void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
//{
//    Log.Error("{ErrorMessage}--{ErrorId}.", ex.StackTrace, error.Id);
//    error.Detail = "�o�Ϳ��~" + ex.Message;
//}

