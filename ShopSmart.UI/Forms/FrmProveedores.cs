using System;
using System.Linq;
using System.Windows.Forms;
using ShopSmart.Core.Models;
using ShopSmart.Core.Services;

namespace ShopSmart.UI.Forms;

public class FrmProveedores : Form
{
    private readonly GestorProveedores _gestorProveedores = new();
    private readonly BindingSource _binding = new();

    private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, AutoGenerateColumns = true };
    private readonly TextBox _txtNombre = new() { PlaceholderText = "Nombre" };
    private readonly TextBox _txtTelefono = new() { PlaceholderText = "Teléfono" };
    private readonly Button _btnGuardar = new() { Text = "Guardar" };
    private readonly Button _btnEliminar = new() { Text = "Eliminar" };

    public FrmProveedores()
    {
        Text = "Proveedores";
        Width = 600;
        Height = 400;
        InitializeLayout();
        RefrescarTabla();
    }

    private void InitializeLayout()
    {
        _btnGuardar.Click += (_, _) => Guardar();
        _btnEliminar.Click += (_, _) => Eliminar();

        var topPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true
        };

        topPanel.Controls.AddRange(new Control[] { _txtNombre, _txtTelefono, _btnGuardar, _btnEliminar });
        Controls.Add(_grid);
        Controls.Add(topPanel);
    }

    private void Guardar()
    {
        try
        {
            var proveedor = new Proveedor { Nombre = _txtNombre.Text, Telefono = _txtTelefono.Text, Activo = true };
            _gestorProveedores.AgregarOActualizar(proveedor);
            RefrescarTabla();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Eliminar()
    {
        if (_grid.CurrentRow?.DataBoundItem is not Proveedor proveedor)
        {
            return;
        }

        _gestorProveedores.Eliminar(proveedor.Nombre);
        RefrescarTabla();
    }

    private void RefrescarTabla()
    {
        _binding.DataSource = _gestorProveedores.Listar().ToList();
        _grid.DataSource = _binding;
    }
}
