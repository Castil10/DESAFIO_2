namespace ShopSmart.Core.Modelos;

public class Producto : IComparable<Producto>, IEquatable<Producto>
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

    public bool Equals(Producto? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(Codigo, other.Codigo, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Producto);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Codigo);
    }
}
