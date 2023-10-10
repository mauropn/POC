var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

app.Run();
