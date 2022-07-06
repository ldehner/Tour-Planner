using TourPlanner.Data;
using Microsoft.EntityFrameworkCore;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var optionsBuilder = new DbContextOptionsBuilder<ToursDataContext>();
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("TourPlannerDb"));
var tourRepository = new EFTourRepository(new ToursDataContext(optionsBuilder.Options));
var tourLogRepository = new EFTourLogRepository(new ToursDataContext(optionsBuilder.Options));
var mapQuestRepository = new MapQuestRepository(System.IO.Directory.GetCurrentDirectory()+"/maps/");
var pdfTemplateRepository = new PdfTemplateRepository(System.IO.Directory.GetCurrentDirectory());
var tourManager = new TourManager(tourRepository, pdfTemplateRepository);
var tourLogManager = new TourLogManager(tourLogRepository);
var mapQuestManager = new MapQuestManager(mapQuestRepository);
builder.Services.Add(new ServiceDescriptor(typeof(ITourManager), tourManager));
builder.Services.Add(new ServiceDescriptor(typeof(ITourLogManager), tourLogManager));
builder.Services.Add(new ServiceDescriptor(typeof(IMapQuestManager), mapQuestManager));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToursDataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("TourPlannerDb")));
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
