using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Books;

namespace BookStore.Domain.Entites.Orders;

public class OrderDetails : Auditable
{
    public int OrderId { get; set; }

    public int BookId { get; set; }
    [ForeignKey(nameof(BookId))]
    public Book? Book { get; set; }

    public int Count { get; set; }
}