using ShopSmart.Core.Gestores;
using ShopSmart.Core.Modelos;
using ShopSmart.Data.Repositorios;
using System.Windows.Forms;

namespace ShopSmart.UI;

public class FrmProductos : Form
{
    private readonly TextBox _txtCodigo = new() { Left = 120, Top = 20, Width = 200 };
    private readonly TextBox _txtNombre = new() { Left = 120, Top = 50, Width = 200 };
    private readonly TextBox _txtDescripcion = new() { Left = 120, Top = 80, Width = 200 };
    private readonly NumericUpDown _numPrecio = new() { Left = 120, Top = 110, Width = 200, DecimalPlaces = 2, Maximum = 1000000 };
    private readonly NumericUpDown _numStockActual = new() { Left = 120, Top = 140, Width = 200, Maximum = 1000000 };
    private readonly NumericUpDown _numStockMinimo = new() { Left = 120, Top = 170, Width = 200, Maximum = 1000000 };
    private readonly CheckBox _chkActivo = new() { Left = 120, Top = 200, Text = "Activo" };
    private readonly DataGridView _dgvProductos = new() { Left = 20, Top = 240, Width = 500, Height = 200, ReadOnly = true, AutoGenerateColumns = true };
    private readonly Button _btnNuevo = new() { Left = 340, Top = 20, Width = 100, Text = "Nuevo" };
    private readonly Button _btnGuardar = new() { Left = 340, Top = 60, Width = 100, Text = "Guardar" };
    private readonly Button _btnActualizar = new() { Left = 340, Top = 100, Width = 100, Text = "Actualizar" };
    private readonly Button _btnEliminar = new() { Left = 340, Top = 140, Width = 100, Text = "Eliminar" };

    private readonly Inventario _inventario = new();
    private readonly ProductosRepository _productosRepository = new();

    public FrmProductos()
    {
        Text = "Productos";
        Width = 560;
        Height = 520;

        Controls.AddRange(new Control[]
        {
            new Label { Left = 20, Top = 20, Text = "Código" },
            _txtCodigo,
            new Label { Left = 20, Top = 50, Text = "Nombre" },
            _txtNombre,
            new Label { Left = 20, Top = 80, Text = "Descripción" },
            _txtDescripcion,
            new Label { Left = 20, Top = 110, Text = "Precio" },
            _numPrecio,
            new Label { Left = 20, Top = 140, Text = "Stock Actual" },
            _numStockActual,
            new Label { Left = 20, Top = 170, Text = "Stock Mínimo" },
            _numStockMinimo,
            _chkActivo,
            _btnNuevo,
            _btnGuardar,
            _btnActualizar,
            _btnEliminar,
            _dgvProductos
        });

        _btnNuevo.Click += (_, _) => LimpiarFormulario();
        _btnGuardar.Click += (_, _) => GuardarProducto();
        _btnActualizar.Click += (_, _) => ActualizarProducto();
        _btnEliminar.Click += (_, _) => EliminarProducto();

        Load += (_, _) => CargarProductos();
    }

    private void CargarProductos()
    {
        try
        {
            var productos = _productosRepository.GetAll().ToList();
            foreach (var producto in productos)
            {
                _inventario.AgregarProducto(producto);
            }

            _dgvProductos.DataSource = productos;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudieron cargar los productos: {ex.Message}");
        }
    }

    private Producto ObtenerDesdeFormulario()
    {
        return new Producto
        {
            Codigo = _txtCodigo.Text.Trim(),
            Nombre = _txtNombre.Text.Trim(),
            Descripcion = _txtDescripcion.Text.Trim(),
            Precio = _numPrecio.Value,
            StockActual = (int)_numStockActual.Value,
            StockMinimo = (int)_numStockMinimo.Value,
            Activo = _chkActivo.Checked
        };
    }

    private bool ValidarFormulario()
    {
        if (string.IsNullOrWhiteSpace(_txtCodigo.Text) || string.IsNullOrWhiteSpace(_txtNombre.Text))
        {
            MessageBox.Show("El código y el nombre son obligatorios");
            return false;
        }

        return true;
    }

    private void GuardarProducto()
    {
        if (!ValidarFormulario())
        {
            return;
        }

        try
        {
            var producto = ObtenerDesdeFormulario();
            _inventario.AgregarProducto(producto);
            _productosRepository.Insert(producto);
            RefrescarGrid();
            MessageBox.Show("Producto guardado correctamente");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo guardar el producto: {ex.Message}");
        }
    }

    private void ActualizarProducto()
    {
        if (!ValidarFormulario())
        {
            return;
        }

        try
        {
            var producto = ObtenerDesdeFormulario();
            _inventario.ActualizarProducto(producto);
            // TODO: llamar a _productosRepository.Update cuando se implemente
            RefrescarGrid();
            MessageBox.Show("Producto actualizado en memoria");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo actualizar el producto: {ex.Message}");
        }
    }

    private void EliminarProducto()
    {
        if (string.IsNullOrWhiteSpace(_txtCodigo.Text))
        {
            MessageBox.Show("Escriba el código a eliminar");
            return;
        }

        try
        {
            _inventario.EliminarProducto(_txtCodigo.Text.Trim());
            // TODO: llamar a _productosRepository.Delete cuando se implemente
            RefrescarGrid();
            MessageBox.Show("Producto eliminado en memoria");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo eliminar el producto: {ex.Message}");
        }
    }

    private void RefrescarGrid()
    {
        _dgvProductos.DataSource = _inventario.ObtenerTodos().ToList();
    }

    private void LimpiarFormulario()
    {
        _txtCodigo.Text = string.Empty;
        _txtNombre.Text = string.Empty;
        _txtDescripcion.Text = string.Empty;
        _numPrecio.Value = 0;
        _numStockActual.Value = 0;
        _numStockMinimo.Value = 0;
        _chkActivo.Checked = false;
    }
}
