using BookStore.Domain.Enums;

namespace BookStore.Domain.Commons;

public abstract class Auditable
{
    public int Id { get; set; }
    
    public ItemState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}   