﻿namespace UserService.Domain.Interfaces;

public interface IHasId
{
    Guid Id { get; set; }
}