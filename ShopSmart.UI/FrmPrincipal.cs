using System.Windows.Forms;

namespace ShopSmart.UI;

public class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        Text = "ShopSmart";
        Width = 800;
        Height = 600;

        var menu = new MenuStrip();
        var productos = new ToolStripMenuItem("Productos");
        var clientes = new ToolStripMenuItem("Clientes");
        var proveedores = new ToolStripMenuItem("Proveedores");
        var ventas = new ToolStripMenuItem("Ventas");
        var reportes = new ToolStripMenuItem("Reportes");

        productos.Click += (_, _) => new FrmProductos().ShowDialog(this);
        clientes.Click += (_, _) => new FrmClientes().ShowDialog(this);
        proveedores.Click += (_, _) => new FrmProveedores().ShowDialog(this);
        ventas.Click += (_, _) => new FrmVentas().ShowDialog(this);
        reportes.Click += (_, _) => MessageBox.Show("// TODO: implementar reportes", "Pendiente", MessageBoxButtons.OK, MessageBoxIcon.Information);

        menu.Items.Add(productos);
        menu.Items.Add(clientes);
        menu.Items.Add(proveedores);
        menu.Items.Add(ventas);
        menu.Items.Add(reportes);

        MainMenuStrip = menu;
        Controls.Add(menu);
    }
}
