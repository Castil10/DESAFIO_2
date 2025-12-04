using System.Windows.Forms;

namespace ShopSmart.UI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var login = new FrmLogin();
        if (login.ShowDialog() == DialogResult.OK)
        {
            Application.Run(new FrmPrincipal());
        }
    }
}
