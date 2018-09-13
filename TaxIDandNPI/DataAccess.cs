using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;


namespace TaxIDandNPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class DataAccess
    {
        public List<Practice> SearchPracticesByNpi(string PracticeNpi)
        {
            using (IDbConnection connection = new SqlConnection(MainWindow.Helper.ConnVal("C1user")))
            {
                var output = connection.Query<Practice>("dbo.PracticeTable_GetByNpi @Npi", new { Npi = PracticeNpi }).ToList();
                return output;
            }
        }

        public List<Doctor> SearchDrByNpi(string DrNpi)
        {
            using (IDbConnection connection = new SqlConnection(MainWindow.Helper.ConnVal("C1user")))
            {
                var output = connection.Query<Doctor>("dbo.DrTable_GetByNpi @Npi", new { Npi = DrNpi }).ToList();
                return output;
            }
        }

        public List<Practice> SearchPracticesByTid(string PracticeTaxID)
        {
            using (IDbConnection connection = new SqlConnection(MainWindow.Helper.ConnVal("C1user")))
            {
                var output = connection.Query<Practice>("dbo.PracticeTable_GetByTid @Tid", new { Tid = PracticeTaxID }).ToList();
                return output;
            }
        }

        public List<Doctor> SearchDrByName(string name)
        {
            using (IDbConnection connection = new SqlConnection(MainWindow.Helper.ConnVal("C1user")))
            {
                var output = connection.Query<Doctor>("SELECT * FROM [dbo].[Dr Table] WHERE DrName LIKE '%"+name+"%';");
                return output as List<Doctor>;
            }
        }


    }
}