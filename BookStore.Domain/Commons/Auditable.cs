using System.Text.Json.Serialization;
using BookStore.Domain.Enums;

namespace BookStore.Domain.Commons;

public abstract class Auditable
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public ItemState State { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}   