using Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterLogServices(builder.Configuration);

builder.Services.RegisterApiServices(builder.Configuration);

builder.Services.RegisterControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfig();

app.InitializeDatabase();

app.Run();