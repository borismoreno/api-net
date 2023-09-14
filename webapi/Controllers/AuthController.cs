using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
  private readonly IUserProvider _userProvider;
  public AuthController(IUserProvider userProvider)
  {
    _userProvider = userProvider;
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> LoginAsync(LoginModel login)
  {
    var usuario = await _userProvider.LoginUser(login);
    if (usuario != null)
    {
      return new JsonResult(usuario);
    }
    else
    {
      return Unauthorized(new ErrorResult() { Status = 401, Title = "Unauthorized", Description = "Usuario o password incorrectos." });
    }
  }
}