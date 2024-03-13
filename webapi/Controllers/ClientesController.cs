using System.Security.Claims;
using Application.Interfaces;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/cliente")]
public class ClientesController : ControllerBase
{
    public readonly IClientesProvider _clientesProvider;
    public ClientesController(IClientesProvider clientesProvider)
    {
        _clientesProvider = clientesProvider;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Cliente>>> GetAsync()
    {
        try
        {
            var uid = ((ClaimsIdentity)User.Identity).FindFirst("uid").Value;
            var clientes = await _clientesProvider.GetClientes(uid);
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetCliente")]
    public async Task<ActionResult<Cliente>> GetClienteAsync(string clienteId)
    {
        try
        {
            var cliente = await _clientesProvider.GetCliente(clienteId);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GenericResponse>> AddCliente(ClienteCrearModel clienteCrearModel)
    {
        try
        {
            var uid = ((ClaimsIdentity)User.Identity).FindFirst("uid").Value;
            var res = await _clientesProvider.AddCliente(clienteCrearModel, uid);
            return Ok(res);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<GenericResponse>> DeleteCliente(string clienteId)
    {
        try
        {
            return await _clientesProvider.DeleteCliente(clienteId);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}