using OmniClinicAPIv1.Service;
using MongoDB.Driver;
using Microsoft.AspNetCore.Hosting;
using OmniClinicAPIv1.ContextDB;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MongoDBSettings>
    (builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<UserContext>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register UserService in the DI container
builder.Services.AddSingleton<UserService>();

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