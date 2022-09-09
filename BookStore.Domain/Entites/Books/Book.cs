using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Enums;

namespace BookStore.Domain.Entites.Books;

public class Book : Auditable
{
    [MaxLength(50)]
    public string Title { get; set; }
    
    public int PublishYear { get; set; }
    public int NumberOfPages { get; set; }
    
    public int PublisherId { get; set; }
    [ForeignKey(nameof(PublisherId))] 
    public Publisher Publisher { get; set; }
    
    public Guid Isbn { get; set; }
    public int Price { get; set; }
    
    public Genre Genre { get; set; }
    public Language Language { get; set; }
}