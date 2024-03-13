using Domain.Entities;
using Domain.Models;

namespace Application.Interfaces;

public interface IClientesProvider
{
    Task<List<Cliente>> GetClientes(string usuario);
    Task<Cliente> GetCliente(string clienteId);
    GenericResponse AddCliente(ClienteCrearModel clienteCrearModel, string uuid);
    Task<GenericResponse> DeleteCliente(string clienteId);
}