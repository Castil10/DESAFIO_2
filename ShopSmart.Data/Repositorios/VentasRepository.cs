using ShopSmart.Core.Modelos;

namespace ShopSmart.Data.Repositorios;

public class VentasRepository
{
    public IEnumerable<Venta> GetAll()
    {
        // TODO: implementar lectura real desde BD
        throw new NotImplementedException();
    }

    public Venta? GetById(int id)
    {
        // TODO: implementar consulta por id
        throw new NotImplementedException();
    }

    public void Insert(Venta venta)
    {
        // TODO: implementar inserción de venta con detalles
        throw new NotImplementedException();
    }

    public void Update(Venta venta)
    {
        // TODO: implementar actualización
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        // TODO: implementar eliminación
        throw new NotImplementedException();
    }

    public void GenerarReporteVentasDiarias(DateTime fecha)
    {
        // TODO: implementar consulta a BD para ventas diarias.
        throw new NotImplementedException();
    }

    public void GenerarReporteStockBajo()
    {
        // TODO: implementar reporte de productos con stock bajo.
        throw new NotImplementedException();
    }

    public void GenerarReporteProductosMasVendidos()
    {
        // TODO: implementar cálculo de productos más vendidos.
        throw new NotImplementedException();
    }
}
