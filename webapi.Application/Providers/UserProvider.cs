using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Application.Providers;

public class UserProvider : IUserProvider
{
  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IConfiguration _configuration;
  public UserProvider(IUsuarioRepository usuarioRepository, IConfiguration configuration)
  {
    _usuarioRepository = usuarioRepository;
    _configuration = configuration;
  }
  public Task<LoginModelView> LoginUser(LoginModel login)
  {
    return ValidarCredenciales(login.Email, login.Password);
  }

  private async Task<LoginModelView> ValidarCredenciales(string userName, string password)
  {
    try
    {
      var usuario = await _usuarioRepository.GetUsuarioByLogin(userName);
      if (usuario != null)
      {
        var verificado = BCryptNet.Verify(password, usuario.Password);
        if (verificado)
        {
          var userModel = new LoginModelView
          {
            UserName = usuario.Nombre
          };
          (userModel.Token, userModel.TokenExpirationDate, userModel.TokenExpiresInMinutes) = GenerateToken(usuario);
          return userModel;
        }
      }
    }
    catch (Exception ex)
    {
      System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
      throw;
    }
    return null;
  }

  public (string token, DateTime tokenExpirationDate, int tokenExpirationInMinutes) GenerateToken(Usuario userModel)
  {
    try
    {
      int expirationMinutes = _configuration.GetValue<int>("Jwt:Expiration");
      var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
      var claims = new ClaimsIdentity();
      claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userModel.Nombre));
      claims.AddClaim(new Claim("uid", userModel.Id.ToString()));

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = claims,
        Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var createdToken = tokenHandler.CreateToken(tokenDescriptor);
      var token = tokenHandler.WriteToken(createdToken);
      return new(token, DateTime.Now.AddMinutes(expirationMinutes), expirationMinutes);
    }
    catch (Exception ex)
    {
      System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
      throw;
    }
  }
}