using ShopSmart.Core.Gestores;
using ShopSmart.Core.Modelos;
using ShopSmart.Data.Repositorios;
using System.Windows.Forms;

namespace ShopSmart.UI;

public class FrmVentas : Form
{
    private readonly TextBox _txtBuscarCodigo = new() { Left = 20, Top = 20, Width = 150 };
    private readonly Button _btnBuscar = new() { Left = 180, Top = 18, Width = 80, Text = "Buscar" };
    private readonly Button _btnAgregar = new() { Left = 270, Top = 18, Width = 80, Text = "Agregar" };
    private readonly NumericUpDown _numCantidad = new() { Left = 360, Top = 20, Width = 80, Minimum = 1, Maximum = 1000, Value = 1 };
    private readonly DataGridView _dgvCarrito = new() { Left = 20, Top = 60, Width = 600, Height = 200, AutoGenerateColumns = true };
    private readonly Label _lblTotal = new() { Left = 20, Top = 270, Width = 200, Text = "Total: 0" };
    private readonly Button _btnGuardar = new() { Left = 20, Top = 300, Width = 100, Text = "Guardar Venta" };

    private readonly Inventario _inventario = new();
    private readonly VentasRepository _ventasRepository = new();
    private readonly ProductosRepository _productosRepository = new();
    private readonly List<DetalleVenta> _carrito = new();
    private readonly GestorClientes _gestorClientes = new();

    public FrmVentas()
    {
        Text = "Ventas";
        Width = 660;
        Height = 380;

        Controls.AddRange(new Control[]
        {
            _txtBuscarCodigo,
            _btnBuscar,
            _btnAgregar,
            _numCantidad,
            _dgvCarrito,
            _lblTotal,
            _btnGuardar
        });

        _btnBuscar.Click += (_, _) => BuscarProducto();
        _btnAgregar.Click += (_, _) => AgregarAlCarrito();
        _btnGuardar.Click += (_, _) => GuardarVenta();

        Load += (_, _) => CargarInventario();
    }

    private void CargarInventario()
    {
        try
        {
            foreach (var producto in _productosRepository.GetAll())
            {
                _inventario.AgregarProducto(producto);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo cargar el inventario: {ex.Message}");
        }
    }

    private void BuscarProducto()
    {
        var codigo = _txtBuscarCodigo.Text.Trim();
        var producto = _inventario.BuscarPorCodigo(codigo);
        if (producto is null)
        {
            MessageBox.Show("Producto no encontrado");
        }
        else
        {
            MessageBox.Show($"Producto: {producto.Nombre} - Precio: {producto.Precio:C}");
        }
    }

    private void AgregarAlCarrito()
    {
        var producto = _inventario.BuscarPorCodigo(_txtBuscarCodigo.Text.Trim());
        if (producto is null)
        {
            MessageBox.Show("Seleccione un producto válido");
            return;
        }

        var cantidad = (int)_numCantidad.Value;
        var detalle = new DetalleVenta
        {
            Producto = producto,
            Cantidad = cantidad,
            PrecioUnitario = producto.Precio,
            Subtotal = producto.Precio * cantidad
        };

        _carrito.Add(detalle);
        RefrescarCarrito();
    }

    private void RefrescarCarrito()
    {
        _dgvCarrito.DataSource = null;
        _dgvCarrito.DataSource = _carrito.ToList();
        _lblTotal.Text = $"Total: {_carrito.Sum(d => d.Subtotal):C}";
    }

    private void GuardarVenta()
    {
        if (_carrito.Count == 0)
        {
            MessageBox.Show("Agregue productos al carrito");
            return;
        }

        try
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Cliente = _gestorClientes.Obtener("") ?? new Cliente { Nombre = "Genérico" },
                Detalles = _carrito.ToList(),
                Total = _carrito.Sum(d => d.Subtotal)
            };

            _ventasRepository.Insert(venta);
            foreach (var detalle in _carrito)
            {
                detalle.Producto.StockActual -= detalle.Cantidad;
                _inventario.ActualizarProducto(detalle.Producto);
            }

            _carrito.Clear();
            RefrescarCarrito();
            MessageBox.Show("Venta guardada (pendiente de implementación de BD)");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo guardar la venta: {ex.Message}");
        }
    }
}
