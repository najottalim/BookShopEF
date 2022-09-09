using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Commons;

namespace BookStore.Domain.Entites.Publishers;

public class Publisher : Auditable
{
    [MaxLength(100), MinLength(8)]
    public string Name { get; set; }
}