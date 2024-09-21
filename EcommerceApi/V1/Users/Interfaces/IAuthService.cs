using EcommerceApi.V1.Users.Models;

namespace EcommerceApi.V1.Users.Interfaces;

public interface IAuthService
{
    ValueTask<UserModel> Registration(RegisterModel user);
    ValueTask<bool> Login(LoginModel model);
}
