using ShopSmart.Core.Models;

namespace ShopSmart.Data.Repositories;

/// <summary>
/// Acceso a datos para productos.
/// </summary>
public class ProductosRepository
{
    private readonly BDConexion _conexion;

    public ProductosRepository(BDConexion conexion)
    {
        _conexion = conexion;
    }

    public IEnumerable<Producto> GetAll()
    {
        var productos = new List<Producto>();
        using var connection = _conexion.CrearConexion();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Codigo, Nombre, Descripcion, Precio, StockActual, StockMinimo, Activo FROM Productos";
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            productos.Add(new Producto
            {
                Id = reader.GetInt32(0),
                Codigo = reader.GetString(1),
                Nombre = reader.GetString(2),
                Descripcion = reader.GetString(3),
                Precio = reader.GetDecimal(4),
                StockActual = reader.GetInt32(5),
                StockMinimo = reader.GetInt32(6),
                Activo = reader.GetBoolean(7)
            });
        }

        return productos;
    }

    public Producto? GetById(int id)
    {
        using var connection = _conexion.CrearConexion();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Codigo, Nombre, Descripcion, Precio, StockActual, StockMinimo, Activo FROM Productos WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Producto
            {
                Id = reader.GetInt32(0),
                Codigo = reader.GetString(1),
                Nombre = reader.GetString(2),
                Descripcion = reader.GetString(3),
                Precio = reader.GetDecimal(4),
                StockActual = reader.GetInt32(5),
                StockMinimo = reader.GetInt32(6),
                Activo = reader.GetBoolean(7)
            };
        }

        return null;
    }

    public void Insert(Producto producto)
    {
        using var connection = _conexion.CrearConexion();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, StockActual, StockMinimo, Activo)
VALUES(@Codigo, @Nombre, @Descripcion, @Precio, @StockActual, @StockMinimo, @Activo)";
        command.Parameters.AddWithValue("@Codigo", producto.Codigo);
        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
        command.Parameters.AddWithValue("@Precio", producto.Precio);
        command.Parameters.AddWithValue("@StockActual", producto.StockActual);
        command.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
        command.Parameters.AddWithValue("@Activo", producto.Activo);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Producto producto)
    {
        using var connection = _conexion.CrearConexion();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Productos SET Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Descripcion, Precio=@Precio, StockActual=@StockActual, StockMinimo=@StockMinimo, Activo=@Activo WHERE Id=@Id";
        command.Parameters.AddWithValue("@Codigo", producto.Codigo);
        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
        command.Parameters.AddWithValue("@Precio", producto.Precio);
        command.Parameters.AddWithValue("@StockActual", producto.StockActual);
        command.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
        command.Parameters.AddWithValue("@Activo", producto.Activo);
        command.Parameters.AddWithValue("@Id", producto.Id);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = _conexion.CrearConexion();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Productos WHERE Id=@Id";
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        command.ExecuteNonQuery();
    }
}
