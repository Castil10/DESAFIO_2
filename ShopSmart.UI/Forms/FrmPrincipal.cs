using System;
using System.Windows.Forms;

namespace ShopSmart.UI.Forms;

public class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        Text = "Heladería ShopSmart - Principal";
        WindowState = FormWindowState.Maximized;
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        var menu = new MenuStrip();
        var productosItem = new ToolStripMenuItem("Sabores y productos", null, (_, _) => new FrmProductos().ShowDialog());
        var clientesItem = new ToolStripMenuItem("Clientes de la heladería", null, (_, _) => new FrmClientes().ShowDialog());
        var proveedoresItem = new ToolStripMenuItem("Proveedores de insumos", null, (_, _) => new FrmProveedores().ShowDialog());
        var ventasItem = new ToolStripMenuItem("Ventas en mostrador", null, (_, _) => new FrmVentas().ShowDialog());
        var reportesItem = new ToolStripMenuItem("Reportes");
        reportesItem.DropDownItems.Add("Ventas diarias de helados (TODO)");
        reportesItem.DropDownItems.Add("Sabores con stock bajo (TODO)");
        reportesItem.DropDownItems.Add("Helados más vendidos (TODO)");

        menu.Items.Add(productosItem);
        menu.Items.Add(clientesItem);
        menu.Items.Add(proveedoresItem);
        menu.Items.Add(ventasItem);
        menu.Items.Add(reportesItem);
        MainMenuStrip = menu;
        Controls.Add(menu);
    }
}
