namespace ShopSmart.Core.Models;

/// <summary>
/// Representa una venta realizada a un cliente.
/// </summary>
public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; } = new Cliente();
    public List<DetalleVenta> Detalles { get; set; } = new();
    public decimal Total { get; set; }
}
