using ShopSmart.Core.DataStructures;
using ShopSmart.Core.Models;

namespace ShopSmart.Core.Services;

/// <summary>
/// Gestiona acciones recientes para habilitar operaciones de deshacer básicas.
/// </summary>
public class GestorAcciones
{
    private readonly StackCustom<AccionSistema> _acciones = new();

    public void RegistrarAccion(string descripcion, Action? revertir = null)
    {
        _acciones.Push(new AccionSistema { Descripcion = descripcion, Revertir = revertir });
    }

    public AccionSistema? DeshacerUltima()
    {
        if (_acciones.Count == 0)
        {
            return null;
        }

        var accion = _acciones.Pop();
        accion.Revertir?.Invoke();
        return accion;
    }
}
