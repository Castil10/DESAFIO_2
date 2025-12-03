namespace ShopSmart.Core.Models;

/// <summary>
/// Producto de heladería administrado en el inventario (sabores, toppings, presentaciones).
/// </summary>
public class Producto : IComparable<Producto>
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public bool Activo { get; set; }

    public int CompareTo(Producto? other)
    {
        if (other is null)
        {
            return 1;
        }

        return string.Compare(Codigo, other.Codigo, StringComparison.OrdinalIgnoreCase);
    }
}
