using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ResidentManagement.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Bill> Bills { get; set; }
}
