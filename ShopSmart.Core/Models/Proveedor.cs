namespace ShopSmart.Core.Models;

/// <summary>
/// Proveedor que abastece insumos para la heladería (bases, frutas, toppings).
/// </summary>
public class Proveedor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
