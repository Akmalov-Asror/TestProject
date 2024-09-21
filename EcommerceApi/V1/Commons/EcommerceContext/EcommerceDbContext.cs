using Microsoft .AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.V1.Roles.Entities;

namespace EcommerceApi.V1.Commons.EcommerceContext;

public class EcommerceDbContext : IdentityDbContext<EcommerceApi.V1.Users.Entities.Users, Role, int>
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

    public DbSet<EcommerceApi.V1.Users.Entities.Users> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

}
