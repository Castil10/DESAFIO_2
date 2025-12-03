using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShopSmart.Core.Models;
using ShopSmart.Core.Services;
using ShopSmart.Data;
using ShopSmart.Data.Repositories;

namespace ShopSmart.UI.Forms;

public class FrmVentas : Form
{
    private readonly Inventario _inventario = new();
    private readonly VentasRepository _ventasRepository = new(new BDConexion());
    private readonly List<DetalleVenta> _carrito = new();

    private readonly TextBox _txtBusqueda = new() { PlaceholderText = "Código o nombre" };
    private readonly Button _btnBuscar = new() { Text = "Buscar" };
    private readonly ListBox _lstResultados = new() { Height = 100, Dock = DockStyle.Top };
    private readonly DataGridView _gridCarrito = new() { Dock = DockStyle.Fill, AutoGenerateColumns = true };
    private readonly Label _lblTotal = new() { Text = "Total: 0", AutoSize = true };
    private readonly Button _btnGuardar = new() { Text = "Guardar venta" };

    public FrmVentas()
    {
        Text = "Ventas de helados";
        Width = 800;
        Height = 600;
        CargarEjemplos();
        InitializeLayout();
    }

    private void InitializeLayout()
    {
        _btnBuscar.Click += (_, _) => BuscarProducto();
        _lstResultados.DoubleClick += (_, _) => AgregarSeleccionado();
        _btnGuardar.Click += (_, _) => GuardarVenta();

        var topPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true
        };

        topPanel.Controls.AddRange(new Control[] { _txtBusqueda, _btnBuscar, _lblTotal, _btnGuardar });
        Controls.Add(_gridCarrito);
        Controls.Add(_lstResultados);
        Controls.Add(topPanel);
    }

    private void CargarEjemplos()
    {
        _inventario.AgregarProducto(new Producto { Codigo = "H001", Nombre = "Helado de vainilla", Precio = 2.50m, StockActual = 30, StockMinimo = 8, Activo = true });
        _inventario.AgregarProducto(new Producto { Codigo = "H002", Nombre = "Helado de chocolate", Precio = 2.75m, StockActual = 25, StockMinimo = 8, Activo = true });
        _inventario.AgregarProducto(new Producto { Codigo = "H003", Nombre = "Paleta de frutilla", Precio = 1.80m, StockActual = 40, StockMinimo = 12, Activo = true });
    }

    private void BuscarProducto()
    {
        var termino = _txtBusqueda.Text;
        var resultados = _inventario.Productos
            .Where(p => p.Codigo.Contains(termino, StringComparison.OrdinalIgnoreCase) ||
                        p.Nombre.Contains(termino, StringComparison.OrdinalIgnoreCase))
            .ToList();

        _lstResultados.Items.Clear();
        foreach (var producto in resultados)
        {
            _lstResultados.Items.Add(producto);
        }

        _lstResultados.DisplayMember = nameof(Producto.Nombre);
    }

    private void AgregarSeleccionado()
    {
        if (_lstResultados.SelectedItem is not Producto producto)
        {
            return;
        }

        var detalle = new DetalleVenta
        {
            Producto = producto,
            Cantidad = 1,
            PrecioUnitario = producto.Precio,
            Subtotal = producto.Precio
        };

        _carrito.Add(detalle);
        producto.StockActual--;
        RefrescarCarrito();
    }

    private void RefrescarCarrito()
    {
        _gridCarrito.DataSource = null;
        _gridCarrito.DataSource = _carrito.Select(d => new
        {
            d.Producto.Codigo,
            d.Producto.Nombre,
            d.Cantidad,
            d.PrecioUnitario,
            d.Subtotal
        }).ToList();

        decimal total = _carrito.Sum(d => d.Subtotal);
        _lblTotal.Text = $"Total: {total:C}";
    }

    private void GuardarVenta()
    {
        try
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Cliente = new Cliente { Id = 1, Nombre = "Cliente genérico" },
                Detalles = _carrito.ToList(),
                Total = _carrito.Sum(c => c.Subtotal)
            };

            _ventasRepository.Insert(venta);
            MessageBox.Show("Venta registrada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _carrito.Clear();
            RefrescarCarrito();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo guardar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
