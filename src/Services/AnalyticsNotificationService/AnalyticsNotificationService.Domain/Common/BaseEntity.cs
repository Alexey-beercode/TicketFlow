﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnalyticsNotificationService.Domain.Interfaces;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    [BsonIgnoreIfDefault]
    public Guid Id { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}