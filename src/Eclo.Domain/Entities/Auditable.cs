﻿namespace Eclo.Domain.Entities;

public abstract class Auditable : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
