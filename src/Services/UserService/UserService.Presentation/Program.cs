using UserService.Domain.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDatabase();
builder.AddServices();
builder.AddMapping();
builder.AddIdentity();
builder.AddValidation();
builder.AddSwaggerDocumentation();
builder.AddLogging();

var app = builder.Build();

app.AddSwagger();
app.AddApplicationMiddleware();

app.Run();