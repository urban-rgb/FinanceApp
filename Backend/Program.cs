using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FinanceApp.Data;
using FinanceApp.Services;
using FinanceApp.Models;

var builder = WebApplication.CreateBuilder(args);

// To give proper information abt API
builder.Services.Configure<ApiInfoOptions>(
    builder.Configuration.GetSection(ApiInfoOptions.SectionName));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
}); // to specify transaction enum type not by number, but by string
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// To give proper information abt API
app.MapGet("/", (IOptions<ApiInfoOptions> apiInfo) => 
{
    var info = apiInfo.Value;
    return Results.Ok(new
    {
        info.Name,
        info.Version,
        info.Environment,
        Status = "Healthy",
        Docs = "/swagger"
    });
});

app.MapControllers();

app.Run();