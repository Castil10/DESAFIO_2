using System.Data;
using System.Data.SqlClient;

namespace ShopSmart.Data;

/// <summary>
/// Maneja la creación de conexiones hacia SQL Server.
/// </summary>
public class BDConexion
{
    private readonly string _connectionString;

    public BDConexion(string connectionString)
    {
        _connectionString = connectionString;
    }

    public BDConexion() : this("Server=localhost;Database=ShopSmart;User Id=usuario;Password=clave;TrustServerCertificate=True;")
    {
    }

    public SqlConnection CrearConexion()
    {
        return new SqlConnection(_connectionString);
    }
}
