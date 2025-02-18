using Presentation;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);



var app = builder.Build();

var logger = app.Services.GetService<ILogger<Startup>>();

var env = app.Services.GetService<IWebHostEnvironment>();

startup.Configure(app, env!, logger!);

app.Run();
