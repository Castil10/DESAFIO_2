using ShopSmart.Core.DataStructures;
using ShopSmart.Core.Models;

namespace ShopSmart.Core.Services;

/// <summary>
/// Gestiona pedidos pendientes mediante una cola personalizada.
/// </summary>
public class GestorPedidos
{
    private readonly QueueCustom<Pedido> _colaPedidos = new();

    public void Encolar(Pedido pedido)
    {
        _colaPedidos.Enqueue(pedido);
    }

    public Pedido? ProcesarSiguiente()
    {
        if (_colaPedidos.Count == 0)
        {
            return null;
        }

        return _colaPedidos.Dequeue();
    }

    public IEnumerable<Pedido> ListarPendientes()
    {
        return _colaPedidos.Recorrer();
    }
}
