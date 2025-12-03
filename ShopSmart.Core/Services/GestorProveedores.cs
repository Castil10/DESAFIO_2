using ShopSmart.Core.Models;

namespace ShopSmart.Core.Services;

/// <summary>
/// Gestiona proveedores utilizando un diccionario por nombre.
/// </summary>
public class GestorProveedores
{
    private readonly Dictionary<string, Proveedor> _proveedores = new(StringComparer.OrdinalIgnoreCase);

    public void AgregarOActualizar(Proveedor proveedor)
    {
        _proveedores[proveedor.Nombre] = proveedor;
    }

    public Proveedor? ObtenerPorNombre(string nombre)
    {
        return _proveedores.TryGetValue(nombre, out var proveedor) ? proveedor : null;
    }

    public bool Eliminar(string nombre)
    {
        return _proveedores.Remove(nombre);
    }

    public IEnumerable<Proveedor> Listar()
    {
        return _proveedores.Values;
    }
}
