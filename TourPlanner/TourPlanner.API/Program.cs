using TourPlanner.Data;
using Microsoft.EntityFrameworkCore;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var optionsBuilder = new DbContextOptionsBuilder<ToursDataContext>();
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("TourPlannerDb"));
EFTourRepository tourRepository = new EFTourRepository(new ToursDataContext(optionsBuilder.Options));
TourManager tourManager = new TourManager(tourRepository);
builder.Services.Add(new ServiceDescriptor(typeof(ITourManager), tourManager));

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
