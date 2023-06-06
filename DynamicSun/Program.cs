using DynamicSun.Application.Services.ExcelParserService;
using DynamicSun.Application.Services.WeatherService;
using DynamicSun.Context;
using DynamicSun.DataAccess;
using DynamicSun.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Add services to the container.
builder.Services.AddWeatherRepositories(connectionString);
builder.Services.AddPostgresSqlDataAccessSchemaMigrator(connectionString);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigins", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IWeatherArchiveRepository, WeatherArchiveRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IExcelParserService, ExcelParserService>();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "DynamicSun", Version = "v1" }));
builder.Services.AddDbContext<WeatherContext>(options
    => options.UseNpgsql(connectionString)
              .UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseCors("AnyOrigins");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();

app.MapControllers();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();