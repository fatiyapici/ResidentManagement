using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ResidentManagement;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }

    [Display(Name = "Identity Number")]
    public int IdentityNumber { get; set; }

    [Display(Name = "Vehicle Plate")]
    public string? VehiclePlate { get; set; }
    
    [Display(Name = "Manager")]
    public bool IsManager { get; set; }
}