using System;
using System.Windows.Forms;
using ShopSmart.UI.Forms;

namespace ShopSmart.UI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmLogin());
    }
}
