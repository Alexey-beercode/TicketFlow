using AnalyticsNotificationService.DLL.Infrastructure.Database.Configuration;
using AnalyticsNotificationService.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AnalyticsNotificationService.DLL.Infrastructure.Database;

public class NotificationDbContext
{
    private readonly IMongoDatabase _database;

    public NotificationDbContext(IOptions<NotificationDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        CreateCollections();
    }

    private void CreateCollections()
    {
        var collections = _database.ListCollectionNames().ToList();

        if (!collections.Contains("Notifications"))
            _database.CreateCollection("Notifications");

        if (!collections.Contains("Analytics"))
            _database.CreateCollection("Analytics");

        if (!collections.Contains("MetricTypes"))
            _database.CreateCollection("MetricTypes");
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

    public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");
    public IMongoCollection<Analytics> Analytics => _database.GetCollection<Analytics>("Analytics");
    public IMongoCollection<MetricType> MetricTypes => _database.GetCollection<MetricType>("MetricTypes");
}