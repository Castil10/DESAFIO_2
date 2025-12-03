using ShopSmart.Core.DataStructures;
using ShopSmart.Core.Models;

namespace ShopSmart.Core.Services;

/// <summary>
/// Inventario que utiliza un BST para búsquedas rápidas y una lista enlazada para recorridos completos.
/// </summary>
public class Inventario
{
    private readonly BinarySearchTree<Producto> _indiceProductos = new();
    private readonly SimpleLinkedList<Producto> _productos = new();

    public IEnumerable<Producto> Productos => _productos.Recorrer();

    public void AgregarProducto(Producto producto)
    {
        _indiceProductos.Insertar(producto);
        _productos.InsertarFinal(producto);
    }

    public Producto? BuscarPorCodigo(string codigo)
    {
        var buscado = new Producto { Codigo = codigo };
        return _indiceProductos.Buscar(buscado);
    }

    public bool EliminarProducto(string codigo)
    {
        // Eliminación básica solo en la lista enlazada.
        return _productos.Eliminar(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
    }
}
