namespace ResidentManagement;

public class Apartment
{
    public int ID { get; set; }
    public string? UserId { get; set; }
    public int Number { get; set; }
    public int Floor { get; set; }
    public string Block { get; set; }
    public string ApartmentType { get; set; }
    public string Status { get; set; }
    public string OwnerOrTenant { get; set; }

    public User? User { get; set; }
}
