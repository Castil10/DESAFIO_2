using ShopSmart.Core.EstructurasDatos;
using ShopSmart.Core.Modelos;

namespace ShopSmart.Core.Gestores;

public class GestorPedidos
{
    private readonly QueueCustom<Pedido> _colaPedidos = new();

    public void EncolarPedido(Pedido pedido)
    {
        _colaPedidos.Enqueue(pedido);
    }

    public Pedido? ObtenerSiguiente()
    {
        if (_colaPedidos.Count == 0)
        {
            return null;
        }

        return _colaPedidos.Dequeue();
    }

    public Pedido? VerSiguiente()
    {
        if (_colaPedidos.Count == 0)
        {
            return null;
        }

        return _colaPedidos.Peek();
    }

    public int CantidadPedidos => _colaPedidos.Count;
}
