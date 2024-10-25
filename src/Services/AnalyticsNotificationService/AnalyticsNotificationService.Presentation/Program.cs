using AnalyticsNotificationService.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddIdentity();
builder.AddLogging();
builder.AddServices();
builder.AddMapping();
builder.AddValidation();
builder.AddMongoDatabase();
builder.AddSwaggerDocumentation();
var app = builder.Build();

app.AddSwagger();
app.AddApplicationMiddleware();

app.Run();
