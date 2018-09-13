using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Deployment.Application;
using Dapper;

//V1.1.0.0- Added search function, sql db from app.config, UI and function improvements. 
//V1.1.0.15- Added multi-practice results view for Dr npi search.
//V1.1.1.0- TID and NPI search moved to same box. Dr Name search added. 



namespace TaxIDandNPI
{

    public partial class MainWindow : Window
    {
        List<Practice> output = new List<Practice>();
        string sqlqry = "select * FROM [Practice Table]";
        



        public MainWindow()
        {
            InitializeComponent();
            try
            {
                GetPracticeList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void GetPracticeList()
        {
            try
            {
                PListBox.Items.Clear();
                using (IDbConnection connection = new SqlConnection(Helper.ConnVal("C1user")))
                {
                    output = connection.Query<Practice>(sqlqry).ToList();

                    foreach (var op in output)
                    {
                        PListBox.Items.Add(op.PracticeId + " - " + op.PracticeName);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void CopyTidBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TidBox.Text);
        }

        private void CopyNpiBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(NpiBox.Text);
        }

        private void PListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var text = PListBox.SelectedValue.ToString();
            text = text.Substring(0, 3);
            if (text != String.Empty)
                foreach (var p in output)
                {
                    if (text == p.PracticeId.ToString())
                    {
                        TidBox.Text = p.PracticeTaxId.ToString();
                        NpiBox.Text = p.PracticeNpi.ToString();
                    }
                }
        }

        void DrEnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                NSearchBox.Clear();
                try
                {
                    string name = DrSearchBox.Text;
                    DataAccess qda = new DataAccess();
                    string results = "";


                    var sqlResults = qda.SearchDrByName(name);

                    if (sqlResults.Count==0) { ResultBox.Text = "----- No results found -----"; }
                    else
                    {
                        foreach (var doctor in sqlResults)
                        {
                            results += doctor.DrFullInfo +"\n";
                        }
                        ResultBox.Text = results;
                    }
                    
                    e.Handled = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        void NEnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DrSearchBox.Clear();
                try
                {
                    string qry = NSearchBox.Text;
                    string npis = "";
                    DataAccess qda = new DataAccess();
                    string results = "";

                    if (qry.Length == 9)
                    {
                        var sqlResults = qda.SearchPracticesByTid(qry);
                        if (sqlResults.Count == 0) { ResultBox.Text = "----- No results found -----"; }
                        else
                        {
                            foreach (var p in sqlResults)
                            {
                                results = p.PracticeFullInfo;
                            }
                            ResultBox.Text = results;
                        }
                    }
                    else if (qry.Length == 10)
                    {
                        var drResults = qda.SearchDrByNpi(qry);

                        if (drResults.Count == 1)
                        {
                            foreach (var d in drResults)
                            {
                                results = d.DrFullInfo;
                            }
                        }
                        else if (drResults.Count > 1)
                        {
                            foreach (var d in drResults)
                            {
                                npis += d.PracticeId.ToString() + ", ";
                            }
                            var drname = drResults[0].DrName;
                            results = drname + " - " + npis;
                        }
                        else
                        {
                            var prResults = qda.SearchPracticesByNpi(qry);
                            if (prResults.Count != 0)
                            {
                                foreach (var p in prResults)
                                {
                                    results = p.PracticeFullInfo;
                                }
                            }
                            else
                            {
                                results = "----- No results found -----";
                            }
                        }
                    }
                    else { results = "----- No results found -----"; }
                    
                    ResultBox.Text = results;
                    e.Handled = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        #region Autotext clearers
        private void TidSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.Text = String.Empty;
            box.GotFocus -= TidSearchBox_GotFocus;
        }

        private void NpiSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.Text = String.Empty;
            box.GotFocus -= NpiSearchBox_GotFocus;
        }
        
        #endregion

        //V1.1.0.14
        public Version AssemblyVersion
        {
            get
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
        }

        //V1.1.0.14
        private void VersionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Version: " + AssemblyVersion.Major + "." + AssemblyVersion.Minor + "." + AssemblyVersion.Build + "." + AssemblyVersion.Revision);
                }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
    
    

