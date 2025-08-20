using AutoMapper;
using MillionLuxury.TechhicalTest.ApiRest.DbConnection;
using MillionLuxury.TechhicalTest.Application;
using MillionLuxury.TechhicalTest.Infraestructure.Data;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DbConnection;

var builder = WebApplication.CreateBuilder(args);

// Cors
var corsUrls = builder.Configuration.GetSection("UrlCors").Get<string[]>();
if (corsUrls != null)
{
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CORS", policy =>
        {
            policy
                .WithOrigins(corsUrls)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

// Add Automaper clases
builder.Services.AddSingleton(sp =>
{
    MapperConfigurationExpression currentMapperConfigurationExpression = new();
    currentMapperConfigurationExpression.AddMaps(InfrastructureDataConfiguration.GetProfiles());
    currentMapperConfigurationExpression.AddMaps(ApplicationConfiguration.GetProfiles());
    return new MapperConfiguration(currentMapperConfigurationExpression, sp.GetRequiredService<ILoggerFactory>()).CreateMapper();
});


// Add MongoDbConnectionProperties
var mongoDbConnectionProperties = builder.Configuration.GetSection("MongoDbConnectionProperties").Get<MongoDbConnectionProperties>() ??
    throw new InvalidOperationException("MongoDbConnectionProperties configuration section is missing or invalid.");
builder.Services.AddSingleton<IMongoDbConnectionProperties>(opt => mongoDbConnectionProperties);

// Add repositories
builder.Services.AddRepositories();

// Add use cases
builder.Services.AddUseCases();

// Add controllers.
builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Cors
if (corsUrls != null)
{
    app.UseCors("CORS");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
