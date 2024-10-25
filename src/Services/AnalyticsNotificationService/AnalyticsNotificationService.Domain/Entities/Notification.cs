using AnalyticsNotificationService.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnalyticsNotificationService.Domain.Entities;

public class Notification : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}