using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentManagement;

public class Invoice
{
    public int ID { get; set; }
    public int ApartmentId { get; set; }
    public DateTime Session { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public string Description { get; set; }

    public Apartment Apartment { get; set; }

}
