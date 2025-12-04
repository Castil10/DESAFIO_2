using ShopSmart.Core.Modelos;

namespace ShopSmart.Core.Gestores;

public class GestorClientes
{
    private readonly Dictionary<string, Cliente> _clientes = new(StringComparer.OrdinalIgnoreCase);

    public void Agregar(Cliente cliente)
    {
        _clientes[cliente.Documento] = cliente;
    }

    public Cliente? Obtener(string documento)
    {
        return _clientes.TryGetValue(documento, out var cliente) ? cliente : null;
    }

    public IEnumerable<Cliente> ObtenerTodos() => _clientes.Values;

    public void Actualizar(Cliente cliente)
    {
        _clientes[cliente.Documento] = cliente;
    }

    public void Eliminar(string documento)
    {
        _clientes.Remove(documento);
    }
}
