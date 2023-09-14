using Application.Abstractions;
using Domain.Entities;
using MongoDB.Driver;

public class UsuarioRepository : IUsuarioRepository
{
  private const string collectionName = "usuarios";
  private readonly IMongoCollection<Usuario> dbCollection;
  private readonly FilterDefinitionBuilder<Usuario> filterDefinitionBuilder = Builders<Usuario>.Filter;

  public UsuarioRepository(IMongoDatabase database)
  {
    dbCollection = database.GetCollection<Usuario>(collectionName);
  }
  public Task<Usuario> AddUsuario(Usuario toCreate)
  {
    throw new NotImplementedException();
  }

  public Task<ICollection<Usuario>> GetAll()
  {
    throw new NotImplementedException();
  }

  public Task<Usuario> GetUsuarioById(Guid Id)
  {
    FilterDefinition<Usuario> filter = filterDefinitionBuilder.Eq(usuario => usuario.Id, Id);
    return dbCollection.Find(filter).FirstOrDefaultAsync();
  }

  public Task<Usuario> GetUsuarioByLogin(string login)
  {
    FilterDefinition<Usuario> filter = filterDefinitionBuilder.Eq(usuario => usuario.Email, login);
    return dbCollection.Find(filter).FirstOrDefaultAsync();
  }
}