using Microsoft.EntityFrameworkCore;
using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Repositories.Interfaces;
using MedicineIdentificationAPI.Repositories.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IMedicineRepository, MedicineServices>();
builder.Services.AddScoped<IMedicineImageRepository, MedicineImageService>();
builder.Services.AddScoped<IPredictionRepository, PredictionService>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackService>();
// Ensure this is not null
builder.Services.AddDbContext<MedicineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MedicineDatabase")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
