using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentManagement;

public class Bill
{
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime Session { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
