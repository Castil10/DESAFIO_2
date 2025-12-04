using ShopSmart.Core.Gestores;
using ShopSmart.Core.Modelos;
using ShopSmart.Data.Repositorios;
using System.Windows.Forms;

namespace ShopSmart.UI;

public class FrmProveedores : Form
{
    private readonly TextBox _txtNombre = new() { Left = 120, Top = 20, Width = 200 };
    private readonly TextBox _txtTelefono = new() { Left = 120, Top = 50, Width = 200 };
    private readonly TextBox _txtCorreo = new() { Left = 120, Top = 80, Width = 200 };
    private readonly TextBox _txtDireccion = new() { Left = 120, Top = 110, Width = 200 };
    private readonly CheckBox _chkActivo = new() { Left = 120, Top = 140, Text = "Activo" };
    private readonly DataGridView _dgvProveedores = new() { Left = 20, Top = 180, Width = 500, Height = 200, ReadOnly = true, AutoGenerateColumns = true };
    private readonly Button _btnGuardar = new() { Left = 340, Top = 20, Width = 100, Text = "Guardar" };
    private readonly Button _btnBuscar = new() { Left = 340, Top = 60, Width = 100, Text = "Buscar" };

    private readonly GestorProveedores _gestor = new();
    private readonly ProveedoresRepository _repo = new();

    public FrmProveedores()
    {
        Text = "Proveedores";
        Width = 560;
        Height = 460;

        Controls.AddRange(new Control[]
        {
            new Label { Left = 20, Top = 20, Text = "Nombre" }, _txtNombre,
            new Label { Left = 20, Top = 50, Text = "Teléfono" }, _txtTelefono,
            new Label { Left = 20, Top = 80, Text = "Correo" }, _txtCorreo,
            new Label { Left = 20, Top = 110, Text = "Dirección" }, _txtDireccion,
            _chkActivo,
            _btnGuardar,
            _btnBuscar,
            _dgvProveedores
        });

        _btnGuardar.Click += (_, _) => Guardar();
        _btnBuscar.Click += (_, _) => Buscar();
    }

    private Proveedor ObtenerProveedor() => new()
    {
        Nombre = _txtNombre.Text.Trim(),
        Telefono = _txtTelefono.Text.Trim(),
        Correo = _txtCorreo.Text.Trim(),
        Direccion = _txtDireccion.Text.Trim(),
        Activo = _chkActivo.Checked
    };

    private void Guardar()
    {
        var proveedor = ObtenerProveedor();
        if (string.IsNullOrWhiteSpace(proveedor.Nombre))
        {
            MessageBox.Show("Nombre requerido");
            return;
        }

        _gestor.Agregar(proveedor);
        // TODO: guardar en base de datos con _repo.Insert
        RefrescarGrid();
    }

    private void Buscar()
    {
        var proveedor = _gestor.Obtener(_txtNombre.Text.Trim());
        if (proveedor is null)
        {
            MessageBox.Show("Proveedor no encontrado en memoria");
            return;
        }

        _txtTelefono.Text = proveedor.Telefono;
        _txtCorreo.Text = proveedor.Correo;
        _txtDireccion.Text = proveedor.Direccion;
        _chkActivo.Checked = proveedor.Activo;
    }

    private void RefrescarGrid()
    {
        _dgvProveedores.DataSource = _gestor.ObtenerTodos().ToList();
    }
}
