namespace ShopSmart.Core.Models;

/// <summary>
/// Representa a un cliente frecuente o eventual de la heladería.
/// </summary>
public class Cliente
{
    public int Id { get; set; }
    public string Documento { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
