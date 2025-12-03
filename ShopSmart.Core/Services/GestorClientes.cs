using ShopSmart.Core.DataStructures;
using ShopSmart.Core.Models;

namespace ShopSmart.Core.Services;

/// <summary>
/// Gestiona clientes con tabla hash para acceso rápido por documento.
/// </summary>
public class GestorClientes
{
    private readonly HashTableCustom<string, Cliente> _clientes = new();

    public void AgregarOActualizar(Cliente cliente)
    {
        _clientes.AddOrUpdate(cliente.Documento, cliente);
    }

    public Cliente? ObtenerPorDocumento(string documento)
    {
        return _clientes.TryGetValue(documento, out var cliente) ? cliente : null;
    }

    public bool Eliminar(string documento)
    {
        return _clientes.Remove(documento);
    }

    public IEnumerable<Cliente> Listar()
    {
        return _clientes.Items.Select(c => c.Value);
    }
}
