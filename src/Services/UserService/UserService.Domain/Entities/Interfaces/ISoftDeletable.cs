﻿namespace UserService.Domain.Entities.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}
