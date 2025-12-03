using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShopSmart.Core.Models;

namespace ShopSmart.UI.Forms;

public class FrmLogin : Form
{
    private readonly List<Usuario> _usuarios = new()
    {
        new Usuario { Nombre = "admin", Contrasena = "admin" },
        new Usuario { Nombre = "cajero", Contrasena = "1234" }
    };

    private readonly TextBox _txtUsuario = new() { PlaceholderText = "Usuario" };
    private readonly TextBox _txtContrasena = new() { PlaceholderText = "Contraseña", UseSystemPasswordChar = true };
    private readonly Button _btnIngresar = new() { Text = "Ingresar" };
    private readonly Label _lblEstado = new() { AutoSize = true };

    public FrmLogin()
    {
        Text = "ShopSmart - Login";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _btnIngresar.Click += ValidarIngreso;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(15),
            RowCount = 4,
            ColumnCount = 1
        };

        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        layout.Controls.Add(_txtUsuario, 0, 0);
        layout.Controls.Add(_txtContrasena, 0, 1);
        layout.Controls.Add(_btnIngresar, 0, 2);
        layout.Controls.Add(_lblEstado, 0, 3);
        Controls.Add(layout);
    }

    private void ValidarIngreso(object? sender, EventArgs e)
    {
        try
        {
            var usuario = _usuarios.FirstOrDefault(u =>
                u.Nombre.Equals(_txtUsuario.Text, StringComparison.OrdinalIgnoreCase) &&
                u.Contrasena == _txtContrasena.Text);

            if (usuario is null)
            {
                _lblEstado.Text = "Usuario o contraseña incorrectos";
                return;
            }

            Hide();
            using var principal = new FrmPrincipal();
            principal.ShowDialog();
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al intentar ingresar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
