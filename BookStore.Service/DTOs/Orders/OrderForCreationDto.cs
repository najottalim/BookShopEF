using System.ComponentModel.DataAnnotations;

namespace BookStore.Service.DTOs.Orders;

public class OrderForCreationDto
{
    [Required] public string Address { get; set; } = null!;

    [Required] public IEnumerable<OrderBookCount> Books { get; set; } = null!;
}

public class OrderBookCount
{
    [Required] public int BookId { get; set; }
    [Required] public int Count { get; set; }
}