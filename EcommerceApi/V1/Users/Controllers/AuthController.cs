using Asp.Versioning;
using EcommerceApi.V1.Users.Interfaces;
using EcommerceApi.V1.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceApi.V1.Users.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) 
        => _authService = authService;

    [HttpPost("Login")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Here Users able to login system", Description = "Login")]
    public async ValueTask<IActionResult> Login([FromBody] LoginModel model) 
        => Ok(await _authService.Login(model));

    [HttpPost("register")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Here Users able to register system", Description = "Register")]
    public async ValueTask<IActionResult> Register([FromBody] RegisterModel model) 
        => Ok(await _authService.Registration(model));
}
