using NLog;
using NLog.Web;
using UserService.Domain.Extensions;
using LogLevel = NLog.LogLevel;

var builder = WebApplication.CreateBuilder(args);

builder.AddDatabase();
builder.AddServices();
builder.AddMapping();
builder.AddIdentity();
builder.AddValidation();
builder.AddSwaggerDocumentation();
builder.AddLogging();

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var app = builder.Build();

app.AddSwagger();
app.AddApplicationMiddleware();
logger.Log(LogLevel.Info,"Program initial");
app.Run();