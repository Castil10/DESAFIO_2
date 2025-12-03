using System.Windows.Forms;

namespace ShopSmart.UI;

internal static class ApplicationConfiguration
{
    public static void Initialize()
    {
        ApplicationConfigurationSection.Apply();
    }

    private static class ApplicationConfigurationSection
    {
        public static void Apply()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}
