using BPMS.API.Data;
using BPMS.API.Data.Models;
using BPMS.API.Extensions;
using BPMS.API.Interfaces;
using BPMS.API.Middlewares;
using BPMS.API.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Sqids;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Host.UseSerilog();

// Load Sqids settings
var sqidsSettingsSection = builder.Configuration.GetSection(nameof(SqidsSettings));
var sqidsSettings = sqidsSettingsSection.Get<SqidsSettings>();

// Register Sqids with the loaded settings
builder.Services.AddSingleton(new SqidsEncoder<int>(new SqidsOptions
{
    MinLength = sqidsSettings.MinHashLength,
    Alphabet = sqidsSettings.Alphabet
}));

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSqids();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
