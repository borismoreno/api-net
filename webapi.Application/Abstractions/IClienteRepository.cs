using Domain.Entities;
using Domain.Models;

namespace Application.Abstractions;

public interface IClienteRepository
{
    Task<List<Cliente>> GetClientes(string userId);
    Task<Cliente> GetCliente(Guid clienteId);
    GenericResponse AddCliente(Cliente cliente);
    GenericResponse DeleteCliente(Guid clienteId);
}