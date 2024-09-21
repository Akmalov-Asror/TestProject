using EcommerceApi.V1.Commons.AuditableModel;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApi.V1.Users.Entities;

public class Users : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
