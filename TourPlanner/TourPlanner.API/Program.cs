using TourPlanner.Data;
using Microsoft.EntityFrameworkCore;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;
using Microsoft.Extensions.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net("log4net.config");
// Add services to the container.
var optionsBuilder = new DbContextOptionsBuilder<ToursDataContext>();
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("TourPlannerDb"));
var tourRepository = new EFTourRepository(new ToursDataContext(optionsBuilder.Options));
var tourLogRepository = new EFTourLogRepository(new ToursDataContext(optionsBuilder.Options));
var mapQuestRepository = new FileMapRepository(System.IO.Directory.GetCurrentDirectory()+"/maps/");
var pdfTemplateRepository = new PdfTemplateRepository(System.IO.Directory.GetCurrentDirectory());
var tourManager = new TourManager(tourRepository, pdfTemplateRepository);
var tourLogManager = new TourLogManager(tourLogRepository);
var mapQuestManager = new MapQuestManager(mapQuestRepository, builder.Configuration.GetConnectionString("MapQuestApiKey"));

var weatherRepository = new WeatherRepository(builder.Configuration.GetConnectionString("OpenWeatherMapApiKey"));
var coordinatesRepository = new CoordinatesRepository(builder.Configuration.GetConnectionString("OpenWeatherMapApiKey"));

var weatherManager = new WeatherManager(weatherRepository, coordinatesRepository);

builder.Services.Add(new ServiceDescriptor(typeof(IWeatherManager), weatherManager));


builder.Services.Add(new ServiceDescriptor(typeof(ITourManager), tourManager));
builder.Services.Add(new ServiceDescriptor(typeof(ITourLogManager), tourLogManager));
builder.Services.Add(new ServiceDescriptor(typeof(IMapQuestManager), mapQuestManager));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
