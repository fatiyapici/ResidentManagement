using System.ComponentModel.DataAnnotations;

namespace ResidentManagement;

public class InvoiceCreateViewModel
{
    public int ID { get; set; }
    public int ApartmentId { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = true)]
    public string Session { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
