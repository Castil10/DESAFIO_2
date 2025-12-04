using ShopSmart.Core.Gestores;
using ShopSmart.Core.Modelos;
using ShopSmart.Data.Repositorios;
using System.Windows.Forms;

namespace ShopSmart.UI;

public class FrmClientes : Form
{
    private readonly TextBox _txtDocumento = new() { Left = 120, Top = 20, Width = 200 };
    private readonly TextBox _txtNombre = new() { Left = 120, Top = 50, Width = 200 };
    private readonly TextBox _txtTelefono = new() { Left = 120, Top = 80, Width = 200 };
    private readonly TextBox _txtCorreo = new() { Left = 120, Top = 110, Width = 200 };
    private readonly TextBox _txtDireccion = new() { Left = 120, Top = 140, Width = 200 };
    private readonly CheckBox _chkActivo = new() { Left = 120, Top = 170, Text = "Activo" };
    private readonly DataGridView _dgvClientes = new() { Left = 20, Top = 210, Width = 500, Height = 200, ReadOnly = true, AutoGenerateColumns = true };
    private readonly Button _btnGuardar = new() { Left = 340, Top = 20, Width = 100, Text = "Guardar" };
    private readonly Button _btnBuscar = new() { Left = 340, Top = 60, Width = 100, Text = "Buscar" };

    private readonly GestorClientes _gestorClientes = new();
    private readonly ClientesRepository _repo = new();

    public FrmClientes()
    {
        Text = "Clientes";
        Width = 560;
        Height = 460;

        Controls.AddRange(new Control[]
        {
            new Label { Left = 20, Top = 20, Text = "Documento" }, _txtDocumento,
            new Label { Left = 20, Top = 50, Text = "Nombre" }, _txtNombre,
            new Label { Left = 20, Top = 80, Text = "Teléfono" }, _txtTelefono,
            new Label { Left = 20, Top = 110, Text = "Correo" }, _txtCorreo,
            new Label { Left = 20, Top = 140, Text = "Dirección" }, _txtDireccion,
            _chkActivo,
            _btnGuardar,
            _btnBuscar,
            _dgvClientes
        });

        _btnGuardar.Click += (_, _) => Guardar();
        _btnBuscar.Click += (_, _) => Buscar();
    }

    private Cliente ObtenerCliente() => new()
    {
        Documento = _txtDocumento.Text.Trim(),
        Nombre = _txtNombre.Text.Trim(),
        Telefono = _txtTelefono.Text.Trim(),
        Correo = _txtCorreo.Text.Trim(),
        Direccion = _txtDireccion.Text.Trim(),
        Activo = _chkActivo.Checked
    };

    private void Guardar()
    {
        var cliente = ObtenerCliente();
        if (string.IsNullOrWhiteSpace(cliente.Documento))
        {
            MessageBox.Show("Documento requerido");
            return;
        }

        _gestorClientes.Agregar(cliente);
        // TODO: guardar en base de datos a través de _repo.Insert
        RefrescarGrid();
    }

    private void Buscar()
    {
        var cliente = _gestorClientes.Obtener(_txtDocumento.Text.Trim());
        if (cliente is null)
        {
            MessageBox.Show("Cliente no encontrado en memoria");
            return;
        }

        _txtNombre.Text = cliente.Nombre;
        _txtTelefono.Text = cliente.Telefono;
        _txtCorreo.Text = cliente.Correo;
        _txtDireccion.Text = cliente.Direccion;
        _chkActivo.Checked = cliente.Activo;
    }

    private void RefrescarGrid()
    {
        _dgvClientes.DataSource = _gestorClientes.ObtenerTodos().ToList();
    }
}
