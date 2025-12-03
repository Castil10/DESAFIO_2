using System;
using System.Linq;
using System.Windows.Forms;
using ShopSmart.Core.Models;
using ShopSmart.Core.Services;
using ShopSmart.Data;
using ShopSmart.Data.Repositories;

namespace ShopSmart.UI.Forms;

public class FrmProductos : Form
{
    private readonly Inventario _inventario = new();
    private readonly ProductosRepository _productosRepository = new(new BDConexion());
    private readonly BindingSource _binding = new();

    private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, AutoGenerateColumns = true };
    private readonly TextBox _txtCodigo = new() { PlaceholderText = "Código" };
    private readonly TextBox _txtNombre = new() { PlaceholderText = "Nombre" };
    private readonly NumericUpDown _numPrecio = new() { Maximum = 1000000, DecimalPlaces = 2 };
    private readonly NumericUpDown _numStock = new() { Maximum = 10000 };
    private readonly NumericUpDown _numStockMinimo = new() { Maximum = 10000 };
    private readonly Button _btnAgregar = new() { Text = "Agregar" };
    private readonly Button _btnEliminar = new() { Text = "Eliminar" };

    public FrmProductos()
    {
        Text = "Gestión de productos";
        Width = 800;
        Height = 600;
        InitializeLayout();
        CargarEjemplos();
        RefrescarTabla();
    }

    private void InitializeLayout()
    {
        _btnAgregar.Click += (_, _) => AgregarProducto();
        _btnEliminar.Click += (_, _) => EliminarProductoSeleccionado();

        var formularioPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true
        };

        formularioPanel.Controls.AddRange(new Control[]
        {
            _txtCodigo,
            _txtNombre,
            _numPrecio,
            _numStock,
            _numStockMinimo,
            _btnAgregar,
            _btnEliminar
        });

        Controls.Add(_grid);
        Controls.Add(formularioPanel);
    }

    private void CargarEjemplos()
    {
        _inventario.AgregarProducto(new Producto
        {
            Codigo = "P001",
            Nombre = "Teclado",
            Descripcion = "Teclado de oficina",
            Precio = 25,
            StockActual = 10,
            StockMinimo = 2,
            Activo = true
        });
        _inventario.AgregarProducto(new Producto
        {
            Codigo = "P002",
            Nombre = "Mouse",
            Descripcion = "Mouse óptico",
            Precio = 15,
            StockActual = 20,
            StockMinimo = 5,
            Activo = true
        });
    }

    private void AgregarProducto()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_txtCodigo.Text) || string.IsNullOrWhiteSpace(_txtNombre.Text))
            {
                MessageBox.Show("Código y nombre son obligatorios", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var producto = new Producto
            {
                Codigo = _txtCodigo.Text,
                Nombre = _txtNombre.Text,
                Precio = _numPrecio.Value,
                StockActual = (int)_numStock.Value,
                StockMinimo = (int)_numStockMinimo.Value,
                Activo = true
            };

            _inventario.AgregarProducto(producto);
            // Guardar en base de datos.
            _productosRepository.Insert(producto);
            RefrescarTabla();
            LimpiarFormulario();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al agregar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void EliminarProductoSeleccionado()
    {
        if (_grid.CurrentRow?.DataBoundItem is not Producto producto)
        {
            return;
        }

        try
        {
            _inventario.EliminarProducto(producto.Codigo);
            _productosRepository.Delete(producto.Id);
            RefrescarTabla();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefrescarTabla()
    {
        _binding.DataSource = _inventario.Productos.ToList();
        _grid.DataSource = _binding;
    }

    private void LimpiarFormulario()
    {
        _txtCodigo.Clear();
        _txtNombre.Clear();
        _numPrecio.Value = 0;
        _numStock.Value = 0;
        _numStockMinimo.Value = 0;
    }
}
