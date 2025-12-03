namespace ShopSmart.Core.Models;

/// <summary>
/// Línea de detalle dentro de una venta.
/// </summary>
public class DetalleVenta
{
    public int Id { get; set; }
    public Producto Producto { get; set; } = new Producto();
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
