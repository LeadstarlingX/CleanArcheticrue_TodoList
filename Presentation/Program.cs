using Presentation;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);


// Add services to the container.

var app = builder.Build();


var env = app.Services.GetService<IWebHostEnvironment>();

startup.Configure(app, env!);

app.Run();
