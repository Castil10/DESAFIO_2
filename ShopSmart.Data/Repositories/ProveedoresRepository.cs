using ShopSmart.Core.Models;

namespace ShopSmart.Data.Repositories;

/// <summary>
/// Acceso a datos para proveedores.
/// </summary>
public class ProveedoresRepository
{
    private readonly BDConexion _conexion;

    public ProveedoresRepository(BDConexion conexion)
    {
        _conexion = conexion;
    }

    public IEnumerable<Proveedor> GetAll()
    {
        // TODO: Implementar lectura completa desde la tabla Proveedores.
        return Enumerable.Empty<Proveedor>();
    }

    public Proveedor? GetById(int id)
    {
        // TODO: Implementar consulta por Id.
        return null;
    }

    public void Insert(Proveedor proveedor)
    {
        // TODO: Implementar inserción en base de datos.
    }

    public void Update(Proveedor proveedor)
    {
        // TODO: Implementar actualización en base de datos.
    }

    public void Delete(int id)
    {
        // TODO: Implementar eliminación en base de datos.
    }
}
