using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class LoginModel
{
  [Required(AllowEmptyStrings = false)]
  public string Email { get; set; }
  [Required(AllowEmptyStrings = false)]
  public string Password { get; set; }
}