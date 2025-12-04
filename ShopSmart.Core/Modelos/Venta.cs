namespace ShopSmart.Core.Modelos;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; } = new Cliente();
    public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    public decimal Total { get; set; }
}
