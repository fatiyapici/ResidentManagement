using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentManagement;

public class Invoice
{
    public int ID { get; set; }
    public int ApartmentId { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Session { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public string? Description { get; set; }

    public Apartment Apartment { get; set; }

}
