using ShopSmart.Core.Models;

namespace ShopSmart.Data.Repositories;

/// <summary>
/// Acceso a datos para clientes.
/// </summary>
public class ClientesRepository
{
    private readonly BDConexion _conexion;

    public ClientesRepository(BDConexion conexion)
    {
        _conexion = conexion;
    }

    public IEnumerable<Cliente> GetAll()
    {
        // TODO: Implementar lectura completa desde la tabla Clientes.
        return Enumerable.Empty<Cliente>();
    }

    public Cliente? GetById(int id)
    {
        // TODO: Implementar consulta por Id.
        return null;
    }

    public void Insert(Cliente cliente)
    {
        // TODO: Implementar inserción en base de datos.
    }

    public void Update(Cliente cliente)
    {
        // TODO: Implementar actualización en base de datos.
    }

    public void Delete(int id)
    {
        // TODO: Implementar eliminación en base de datos.
    }
}
