
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("products")]
public class Product
{
    [Key] // Bu, EF Core'a 'id'nin primary key olduğunu söyler.
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [JsonIgnore] // JSON döngü hatasını engellemek için gerekli.
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}