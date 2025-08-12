using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

[Table("orders")]
public class Order
{
    [Key]
    [Column("id")]
    public int OrderId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [JsonIgnore] // JSON döngü hatasını engellemek için gerekli.
    public Product Product { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("order_date")]
    public DateTime OrderDate { get; set; }
}