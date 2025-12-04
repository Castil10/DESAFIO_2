using ShopSmart.Core.EstructurasDatos;
using ShopSmart.Core.Modelos;

namespace ShopSmart.Core.Gestores;

public class GestorAcciones
{
    private readonly StackCustom<AccionSistema> _acciones = new();

    public void RegistrarAccion(AccionSistema accion)
    {
        _acciones.Push(accion);
    }

    public AccionSistema? DeshacerUltimaAccion()
    {
        if (_acciones.Count == 0)
        {
            return null;
        }

        return _acciones.Pop();
    }

    public AccionSistema? VerUltimaAccion()
    {
        if (_acciones.Count == 0)
        {
            return null;
        }

        return _acciones.Peek();
    }
}
