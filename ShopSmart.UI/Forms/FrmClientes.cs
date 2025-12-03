using System;
using System.Linq;
using System.Windows.Forms;
using ShopSmart.Core.Models;
using ShopSmart.Core.Services;

namespace ShopSmart.UI.Forms;

public class FrmClientes : Form
{
    private readonly GestorClientes _gestorClientes = new();
    private readonly BindingSource _binding = new();

    private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, AutoGenerateColumns = true };
    private readonly TextBox _txtDocumento = new() { PlaceholderText = "Documento" };
    private readonly TextBox _txtNombre = new() { PlaceholderText = "Nombre" };
    private readonly Button _btnGuardar = new() { Text = "Guardar" };
    private readonly Button _btnEliminar = new() { Text = "Eliminar" };

    public FrmClientes()
    {
        Text = "Clientes";
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

        topPanel.Controls.AddRange(new Control[] { _txtDocumento, _txtNombre, _btnGuardar, _btnEliminar });
        Controls.Add(_grid);
        Controls.Add(topPanel);
    }

    private void Guardar()
    {
        try
        {
            var cliente = new Cliente { Documento = _txtDocumento.Text, Nombre = _txtNombre.Text, Activo = true };
            _gestorClientes.AgregarOActualizar(cliente);
            RefrescarTabla();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Eliminar()
    {
        if (_grid.CurrentRow?.DataBoundItem is not Cliente cliente)
        {
            return;
        }

        _gestorClientes.Eliminar(cliente.Documento);
        RefrescarTabla();
    }

    private void RefrescarTabla()
    {
        _binding.DataSource = _gestorClientes.Listar().ToList();
        _grid.DataSource = _binding;
    }
}
