using System.Windows.Forms;

namespace ShopSmart.UI;

public class FrmLogin : Form
{
    private readonly TextBox _txtUsuario = new() { Left = 120, Top = 20, Width = 150 };
    private readonly TextBox _txtContrasena = new() { Left = 120, Top = 60, Width = 150, UseSystemPasswordChar = true };
    private readonly Button _btnIngresar = new() { Left = 120, Top = 100, Width = 100, Text = "Ingresar" };

    public FrmLogin()
    {
        Text = "Login";
        Width = 320;
        Height = 200;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        Controls.Add(new Label { Left = 20, Top = 20, Text = "Usuario" });
        Controls.Add(new Label { Left = 20, Top = 60, Text = "Contraseña" });
        Controls.Add(_txtUsuario);
        Controls.Add(_txtContrasena);
        Controls.Add(_btnIngresar);

        _btnIngresar.Click += BtnIngresar_Click;
    }

    private void BtnIngresar_Click(object? sender, EventArgs e)
    {
        if (_txtUsuario.Text == "admin" && _txtContrasena.Text == "admin")
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
