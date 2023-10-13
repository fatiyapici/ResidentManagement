using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ResidentManagement;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }

    [Display(Name = "Identity No")]
    public string IdentityNo { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Vehicle Plate")]
    public string? VehiclePlate { get; set; }
}