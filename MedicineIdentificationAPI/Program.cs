using Microsoft.EntityFrameworkCore;
using MedicineIdentificationAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
