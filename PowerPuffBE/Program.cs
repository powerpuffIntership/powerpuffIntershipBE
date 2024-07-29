using Microsoft.EntityFrameworkCore;
using PowerPuffBE;
using PowerPuffBE.Data;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices();

builder.Services.AddDbContext<PowerPuffDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PowerPuffDatabase")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
        policy  =>
        {
            policy.AllowAnyHeader().AllowAnyOrigin();
        });
});

var app = builder.Build();

//Database Seed
ServicesContainer.SeedDatabase(app);

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger/ui";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Name of Your API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("Policy");
app.Run();
