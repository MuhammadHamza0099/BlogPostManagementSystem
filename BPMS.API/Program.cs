using BPMS.API.Data;
using BPMS.API.Data.Models;
using BPMS.API.Extensions;
using BPMS.API.Interfaces;
using BPMS.API.Services;
using Microsoft.EntityFrameworkCore;
using Sqids;

var builder = WebApplication.CreateBuilder(args);



var sqidssSettingsSection = builder.Configuration.GetSection(nameof(SqidsSettings));
var sqidsSettings = sqidssSettingsSection.Get<SqidsSettings>();

// Register Sqids with the loaded settings
builder.Services.AddSingleton(new SqidsEncoder<int>(new SqidsOptions
{
    MinLength = sqidsSettings.MinHashLength,
    Alphabet = sqidsSettings.Alphabet
}));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<IBlogPostService, BlogPostService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSqids();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
