using ShopSmart.Core.Modelos;

namespace ShopSmart.Core.Gestores;

public class GestorProveedores
{
    private readonly Dictionary<string, Proveedor> _proveedores = new(StringComparer.OrdinalIgnoreCase);

    public void Agregar(Proveedor proveedor)
    {
        _proveedores[proveedor.Nombre] = proveedor;
    }

    public Proveedor? Obtener(string nombre)
    {
        return _proveedores.TryGetValue(nombre, out var proveedor) ? proveedor : null;
    }

    public IEnumerable<Proveedor> ObtenerTodos() => _proveedores.Values;

    public void Actualizar(Proveedor proveedor)
    {
        _proveedores[proveedor.Nombre] = proveedor;
    }

    public void Eliminar(string nombre)
    {
        _proveedores.Remove(nombre);
    }
}
