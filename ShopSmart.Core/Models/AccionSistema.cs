namespace ShopSmart.Core.Models;

/// <summary>
/// Acción registrada en la pila para permitir deshacer operaciones recientes.
/// </summary>
public class AccionSistema
{
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public Action? Revertir { get; set; }
}
