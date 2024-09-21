using System.Security.Claims;

namespace EcommerceApi.V1.Commons.TokenGenerator.Interface;

public interface ITokenGenerator
{
    public string WriteToken(IEnumerable<Claim> claims);
}
