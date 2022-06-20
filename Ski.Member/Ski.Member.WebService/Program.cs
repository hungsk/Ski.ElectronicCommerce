using Ski.Base.Util.Enums;
using Ski.Base.Util.Models;
using Ski.Base.Util.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ski.Member.Data;
using System.Data.Common;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//3.http context
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//5.ado.net for mssql
builder.Services.AddTransient<DbConnection, SqlConnection>();
builder.Services.AddTransient<DbCommand, SqlCommand>();

//EF Core
builder.Services.AddDbContext<MemberDbContext>(option =>
{
    //option.UseSqlServer("data source=172.20.103.78;user id=shinkong;password=shinkong5678;database=shinkong");
    option.UseSqlite("Data Source=../ski.member.data/Sqlite/Member.db");
    option.EnableSensitiveDataLogging();
    option.LogTo(Console.WriteLine);
});


//Repository
builder.Services.AddTransient<IMemberRepository, MemberRepository>();

//6.appSettings "FunConfig" section -> _Fun.Config
var config = new ConfigDto();
builder.Configuration.GetSection("FunConfig").Bind(config);
_Fun.Config = config;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
//1.initial & set locale
_Fun.Init(app.Environment.IsDevelopment(), app.Services, DbTypeEnum.MSSql);
//_Locale.SetCultureAsync(_Fun.Config.Locale);

//2.global exception handler
app.UseExceptionHandler("/Home/Error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();    //�{��
app.UseAuthorization();     //���v

app.MapControllers();

app.Run();
