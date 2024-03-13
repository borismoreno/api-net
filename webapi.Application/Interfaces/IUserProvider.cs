using Domain.Models;

namespace Application.Interfaces;

public interface IUserProvider
{
  Task<LoginModelView> LoginUser(LoginModel login);
}