namespace ShopSmart.Core.Models;

/// <summary>
/// Usuario simple para simulación de autenticación.
/// </summary>
public class Usuario
{
    public string Nombre { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}
