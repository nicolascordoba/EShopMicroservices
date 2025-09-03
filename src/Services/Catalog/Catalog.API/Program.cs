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

//Add custom exception handler as a service in the dependency injection
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

//configure the application to use our custom exception handler
app.UseExceptionHandler(options => { });

app.Run();
