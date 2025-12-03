namespace ShopSmart.Core.Models;

/// <summary>
/// Pedido sencillo usado para la cola de gestores de pedidos.
/// </summary>
public class Pedido
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = new Cliente();
    public List<Producto> Productos { get; set; } = new();
    public DateTime Fecha { get; set; } = DateTime.Now;
}
