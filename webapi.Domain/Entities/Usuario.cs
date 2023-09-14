namespace Domain.Entities;

public sealed class Usuario
{
  public Guid Id { get; set; }

  public List<string>? Rol { get; set; }

  public string? Nombre { get; set; }

  public string? Email { get; set; }

  public string? Password { get; set; }

  public bool Activo { get; set; }

  public string? IdEmpresa { get; set; }
}