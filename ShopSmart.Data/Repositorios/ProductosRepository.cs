using System.Data.SqlClient;
using ShopSmart.Core.Modelos;

namespace ShopSmart.Data.Repositorios;

public class ProductosRepository
{
    public IEnumerable<Producto> GetAll()
    {
        var lista = new List<Producto>();
        using var conexion = new SqlConnection(BDConexion.ConnectionString);
        using var comando = new SqlCommand("SELECT Id, Codigo, Nombre, Descripcion, Precio, StockActual, StockMinimo, Activo FROM Productos", conexion);
        conexion.Open();
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            var producto = new Producto
            {
                Id = lector.GetInt32(0),
                Codigo = lector.GetString(1),
                Nombre = lector.GetString(2),
                Descripcion = lector.GetString(3),
                Precio = lector.GetDecimal(4),
                StockActual = lector.GetInt32(5),
                StockMinimo = lector.GetInt32(6),
                Activo = lector.GetBoolean(7)
            };
            lista.Add(producto);
        }

        return lista;
    }

    public Producto? GetById(int id)
    {
        // TODO: implementar consulta por id
        throw new NotImplementedException();
    }

    public void Insert(Producto producto)
    {
        using var conexion = new SqlConnection(BDConexion.ConnectionString);
        using var comando = new SqlCommand("INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, StockActual, StockMinimo, Activo) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @StockActual, @StockMinimo, @Activo)", conexion);
        comando.Parameters.AddWithValue("@Codigo", producto.Codigo);
        comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
        comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
        comando.Parameters.AddWithValue("@Precio", producto.Precio);
        comando.Parameters.AddWithValue("@StockActual", producto.StockActual);
        comando.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
        comando.Parameters.AddWithValue("@Activo", producto.Activo);

        conexion.Open();
        comando.ExecuteNonQuery();
    }

    public void Update(Producto producto)
    {
        // TODO: implementar actualización de producto en base de datos
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        // TODO: implementar eliminación física o lógica
        throw new NotImplementedException();
    }
}
