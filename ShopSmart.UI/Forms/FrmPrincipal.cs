using System;
using System.Windows.Forms;

namespace ShopSmart.UI.Forms;

public class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        Text = "ShopSmart - Principal";
        WindowState = FormWindowState.Maximized;
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        var menu = new MenuStrip();
        var productosItem = new ToolStripMenuItem("Productos", null, (_, _) => new FrmProductos().ShowDialog());
        var clientesItem = new ToolStripMenuItem("Clientes", null, (_, _) => new FrmClientes().ShowDialog());
        var proveedoresItem = new ToolStripMenuItem("Proveedores", null, (_, _) => new FrmProveedores().ShowDialog());
        var ventasItem = new ToolStripMenuItem("Ventas", null, (_, _) => new FrmVentas().ShowDialog());
        var reportesItem = new ToolStripMenuItem("Reportes");
        reportesItem.DropDownItems.Add("Ventas diarias (TODO)");
        reportesItem.DropDownItems.Add("Productos con stock bajo (TODO)");
        reportesItem.DropDownItems.Add("Productos más vendidos (TODO)");

        menu.Items.Add(productosItem);
        menu.Items.Add(clientesItem);
        menu.Items.Add(proveedoresItem);
        menu.Items.Add(ventasItem);
        menu.Items.Add(reportesItem);
        MainMenuStrip = menu;
        Controls.Add(menu);
    }
}
