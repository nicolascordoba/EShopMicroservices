using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    //Add a validator behavior al pipeline de MediatR enfocado en las reglas de negocio
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //Add a loggin behavior al pipeline de MediatR enfocado en las reglas de negocio
    config.AddOpenBehavior(typeof(LogginBehavoir<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//Initialize data only in dev environment
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

//Add custom exception handler as a service in the dependency injection
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Define the dependency injection of the health validation process
//NpgSql--> validate if the connection is healthy or not
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

//configure the application to use our custom exception handler
app.UseExceptionHandler(options => { });

//Define the endpoint to validate the application health
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
    );

app.Run();
