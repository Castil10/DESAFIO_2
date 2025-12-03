using ShopSmart.Core.Models;

namespace ShopSmart.Data.Repositories;

/// <summary>
/// Acceso a datos para ventas y sus detalles.
/// </summary>
public class VentasRepository
{
    private readonly BDConexion _conexion;

    public VentasRepository(BDConexion conexion)
    {
        _conexion = conexion;
    }

    public IEnumerable<Venta> GetAll()
    {
        // TODO: Incluir JOIN con DetalleVenta para reportes.
        return Enumerable.Empty<Venta>();
    }

    public void Insert(Venta venta)
    {
        using var connection = _conexion.CrearConexion();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "INSERT INTO Ventas(Fecha, ClienteId, Total) OUTPUT INSERTED.Id VALUES(@Fecha, @ClienteId, @Total)";
            command.Parameters.AddWithValue("@Fecha", venta.Fecha);
            command.Parameters.AddWithValue("@ClienteId", venta.Cliente.Id);
            command.Parameters.AddWithValue("@Total", venta.Total);
            int ventaId = (int)command.ExecuteScalar()!;

            foreach (var detalle in venta.Detalles)
            {
                using var detalleCommand = connection.CreateCommand();
                detalleCommand.Transaction = transaction;
                detalleCommand.CommandText = "INSERT INTO DetalleVenta(VentaId, ProductoId, Cantidad, PrecioUnitario, Subtotal) VALUES(@VentaId, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal)";
                detalleCommand.Parameters.AddWithValue("@VentaId", ventaId);
                detalleCommand.Parameters.AddWithValue("@ProductoId", detalle.Producto.Id);
                detalleCommand.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                detalleCommand.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                detalleCommand.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                detalleCommand.ExecuteNonQuery();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
