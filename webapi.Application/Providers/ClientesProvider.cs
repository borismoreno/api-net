using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Models;

namespace Application.Providers;

public class ClientesProvider : IClientesProvider
{
    private readonly IClienteRepository _clienteRepository;
    public ClientesProvider(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }
    public async Task<List<Cliente>> GetClientes(string usuario)
    {
        try
        {
            return await _clienteRepository.GetClientes(usuario);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Cliente> GetCliente(string clienteId)
    {
        return await _clienteRepository.GetCliente(new Guid(clienteId));
    }

    public async Task<GenericResponse> AddCliente(ClienteCrearModel clienteCrearModel, string uuid)
    {
        var cliente = await _clienteRepository.GetClienteIdentificacion(clienteCrearModel.NumeroIdentificacion);
        if (cliente != null) return new GenericResponse() { Success = false, ErrorMessage = "Cliente ya existe" };
        var clienteInsertar = new Cliente()
        {
            Activo = true,
            RazonSocial = clienteCrearModel.RazonSocial,
            TipoIdentificacion = clienteCrearModel.TipoIdentificacion,
            NumeroIdentificacion = clienteCrearModel.NumeroIdentificacion,
            Telefono = clienteCrearModel.Telefono,
            Mail = clienteCrearModel.Mail,
            Direccion = clienteCrearModel.Direccion,
            Usuario = uuid
        };
        return _clienteRepository.AddCliente(clienteInsertar);
    }

    public async Task<GenericResponse> DeleteCliente(string clienteId)
    {
        var cliente = await _clienteRepository.GetCliente(new Guid(clienteId));
        if (cliente == null) return new() { Success = false, ErrorMessage = "Cliente no encontrado" };
        _clienteRepository.DeleteCliente(new Guid(clienteId));

        return new() { Success = true };
    }

    public async Task<GenericResponse> UpdateCliente(string clienteId, ClienteCrearModel clienteCrearModel)
    {
        var cliente = await _clienteRepository.GetCliente(new Guid(clienteId));
        if (cliente == null) return new() { Success = false, ErrorMessage = "Cliente no encontrado" };

        cliente.RazonSocial = clienteCrearModel.RazonSocial;
        cliente.Direccion = clienteCrearModel.Direccion;
        cliente.TipoIdentificacion = clienteCrearModel.TipoIdentificacion;
        cliente.NumeroIdentificacion = clienteCrearModel.NumeroIdentificacion;
        cliente.Telefono = clienteCrearModel.Telefono;
        cliente.Mail = clienteCrearModel.Mail;

        return _clienteRepository.UpdateCliente(cliente);

    }
}