using TourPlanner.Data;
using Microsoft.EntityFrameworkCore;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;
using Microsoft.Extensions.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adding Logging
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.
var optionsBuilder = new DbContextOptionsBuilder<ToursDataContext>();
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("TourPlannerDb"));

// Tour Manager & Repositories
var tourRepository = new EFTourRepository(optionsBuilder.Options);
var pdfTemplateRepository = new PdfTemplateRepository(System.IO.Directory.GetCurrentDirectory());
var tourManager = new TourManager(tourRepository, pdfTemplateRepository);
builder.Services.Add(new ServiceDescriptor(typeof(ITourManager), tourManager));

// Log Manager & Repository
var tourLogRepository = new EFTourLogRepository(optionsBuilder.Options);
var tourLogManager = new TourLogManager(tourLogRepository);
builder.Services.Add(new ServiceDescriptor(typeof(ITourLogManager), tourLogManager));

// MapQuest Manager & Repositories
var fileMapRepository = new FileMapRepository(System.IO.Directory.GetCurrentDirectory()+"/maps/");
var mapQuestRepository = new ApiMapQuestRepository(builder.Configuration.GetConnectionString("MapQuestApiKey"));
var mapQuestManager = new MapQuestManager(fileMapRepository, mapQuestRepository);
builder.Services.Add(new ServiceDescriptor(typeof(IMapQuestManager), mapQuestManager));

// Weather API
var weatherRepository = new ApiWeatherRepository(builder.Configuration.GetConnectionString("OpenWeatherMapApiKey"));
var coordinatesRepository = new ApiCoordinatesRepository(builder.Configuration.GetConnectionString("OpenWeatherMapApiKey"));
var weatherManager = new WeatherManager(weatherRepository, coordinatesRepository);
builder.Services.Add(new ServiceDescriptor(typeof(IWeatherManager), weatherManager));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adds xml documentation to swagger
builder.Services.AddSwaggerGen(opts =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
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

app.Run();
