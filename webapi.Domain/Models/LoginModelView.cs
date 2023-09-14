namespace Domain.Models;

public class LoginModelView
{
  public string UserName { get; set; }
  public string Token { get; set; }
  public int TokenExpiresInMinutes { get; set; }
  public DateTime TokenExpirationDate { get; set; }
}