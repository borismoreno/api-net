using Domain.Entities;

namespace Application.Abstractions;

public interface IUsuarioRepository
{
  Task<ICollection<Usuario>> GetAll();
  Task<Usuario> GetUsuarioById(Guid Id);
  Task<Usuario> GetUsuarioByLogin(string login);
  Task<Usuario> AddUsuario(Usuario toCreate);
}