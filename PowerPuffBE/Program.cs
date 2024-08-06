using Microsoft.EntityFrameworkCore;
using PowerPuffBE;
using PowerPuffBE.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Policy");
app.MapControllers();

app.Run();
