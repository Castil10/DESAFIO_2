using ShopSmart.Core.EstructurasDatos;
using ShopSmart.Core.Modelos;

namespace ShopSmart.Core.Gestores;

public class Inventario
{
    private readonly BST<Producto> _productosPorCodigo = new();
    private readonly SimpleLinkedList<Producto> _recorridoSecuencial = new();

    public void AgregarProducto(Producto producto)
    {
        if (producto is null)
        {
            throw new ArgumentNullException(nameof(producto));
        }

        if (BuscarPorCodigo(producto.Codigo) is not null)
        {
            EliminarProducto(producto.Codigo);
        }

        _productosPorCodigo.Insertar(producto);
        _recorridoSecuencial.InsertarFinal(producto);
    }

    public void ActualizarProducto(Producto producto)
    {
        if (producto is null)
        {
            throw new ArgumentNullException(nameof(producto));
        }

        EliminarProducto(producto.Codigo);
        AgregarProducto(producto);
    }

    public void EliminarProducto(string codigo)
    {
        var existente = BuscarPorCodigo(codigo);
        if (existente is null)
        {
            return;
        }

        var nuevaLista = new SimpleLinkedList<Producto>();
        foreach (var item in _recorridoSecuencial)
        {
            if (!item.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
            {
                nuevaLista.InsertarFinal(item);
            }
        }

        _recorridoSecuencial.ClearAndCopyFrom(nuevaLista);
        _productosPorCodigo.Reconstruir(nuevaLista);
    }

    public Producto? BuscarPorCodigo(string codigo)
    {
        foreach (var producto in _productosPorCodigo)
        {
            if (producto.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
            {
                return producto;
            }
        }

        return null;
    }

    public IEnumerable<Producto> ObtenerTodos()
    {
        foreach (var producto in _recorridoSecuencial)
        {
            yield return producto;
        }
    }
}
