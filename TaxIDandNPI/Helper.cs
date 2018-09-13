using System.Configuration;


namespace TaxIDandNPI
{

    public partial class MainWindow
    {
        public static class Helper
        {
            public static string ConnVal(string name)
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
        }

    }
}
