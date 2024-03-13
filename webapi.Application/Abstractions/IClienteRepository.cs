using Domain.Entities;
using Domain.Models;

namespace Application.Abstractions;

public interface IClienteRepository
{
    Task<List<Cliente>> GetClientes(string userId);
    Task<Cliente> GetCliente(Guid clienteId);
    Task<Cliente> GetClienteIdentificacion(string numeroIdentificacion);
    GenericResponse AddCliente(Cliente cliente);
    GenericResponse DeleteCliente(Guid clienteId);
}