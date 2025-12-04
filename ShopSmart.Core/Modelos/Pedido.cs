namespace ShopSmart.Core.Modelos;

public class Pedido
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = new Cliente();
    public List<Producto> Productos { get; set; } = new List<Producto>();
    public DateTime Fecha { get; set; }
}
