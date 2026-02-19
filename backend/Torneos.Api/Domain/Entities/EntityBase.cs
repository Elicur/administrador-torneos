namespace Torneos.Api.Domain.Entities;

public abstract class EntityBase
{
    public long Id { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}