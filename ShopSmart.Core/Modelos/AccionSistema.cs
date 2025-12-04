namespace ShopSmart.Core.Modelos;

public class AccionSistema
{
    public string TipoAccion { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
}
