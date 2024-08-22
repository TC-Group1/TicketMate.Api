using TicketMate.Api.Middleware;
using TicketMate.Application.Implementation;
using TicketMate.Persistence.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("appsettings.json")
#if DEBUG
    .AddJsonFile("appsettings.Development.json");
#else
    .AddJsonFile("appsettings.Production.json");
#endif

// Inject Dependencies
builder.Services.InjectPersistenceDependencies(builder.Configuration.GetConnectionString("Default"));
builder.Services.InjectApplicationDependencies();

// Inject Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Use Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
