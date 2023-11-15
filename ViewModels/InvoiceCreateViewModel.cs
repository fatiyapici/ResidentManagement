namespace ResidentManagement;

public class InvoiceCreateViewModel
{
    public int ID { get; set; }
    public int ApartmentId { get; set; }
    public DateTime Session { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
