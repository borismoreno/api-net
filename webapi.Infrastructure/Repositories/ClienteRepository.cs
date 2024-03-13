using Application.Abstractions;
using Domain.Entities;
using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private const string collectionName = "clientes";
    private readonly IMongoCollection<Cliente> dbCollection;
    private readonly FilterDefinitionBuilder<Cliente> filterDefinitionBuilder = Builders<Cliente>.Filter;
    public ClienteRepository(IMongoDatabase database)
    {
        dbCollection = database.GetCollection<Cliente>(collectionName);
    }
    public async Task<List<Cliente>> GetClientes(string userId)
    {
        FilterDefinition<Cliente> filter = filterDefinitionBuilder.Eq(cliente => cliente.Usuario, userId);
        return await dbCollection.Find(filter).ToListAsync();
    }

    public async Task<Cliente> GetCliente(Guid clienteId)
    {
        FilterDefinition<Cliente> filter = filterDefinitionBuilder.Eq(cliente => cliente.Id, clienteId);
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Cliente> GetClienteIdentificacion(string numeroIdentificacion)
    {
        FilterDefinition<Cliente> filter = filterDefinitionBuilder.Eq(cliente => cliente.NumeroIdentificacion, numeroIdentificacion);
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public GenericResponse AddCliente(Cliente cliente)
    {
        dbCollection.InsertOne(cliente);
        return new GenericResponse { Success = true };
    }

    public GenericResponse DeleteCliente(Guid clienteId)
    {
        FilterDefinition<Cliente> filter = filterDefinitionBuilder.Eq(cliente => cliente.Id, clienteId);
        dbCollection.DeleteOne(filter);
        return new GenericResponse { Success = true };
    }
}