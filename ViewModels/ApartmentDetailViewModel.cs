namespace ResidentManagement.ViewModels;

public class ApartmentDetailViewModel
{
    public int ID { get; set; }
    public string? IdentityNo { get; set; }
    public string ApartmentType { get; set; }
    public string Status { get; set; }
    public string OwnerOrTenant { get; set; }
}
