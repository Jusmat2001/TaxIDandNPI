using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaxIDandNPI
{
    /// <summary>
    /// Interaction logic for DeletePracticeWindow.xaml
    /// </summary>
    public partial class DeletePracticeWindow : Window
    {
        private string SelectedPractice;
        
        public DeletePracticeWindow()
        {
            InitializeComponent();
            ComboBoxLoad();
        }

        public void ComboBoxLoad()
        {
            DeleteComboBox.Items.Clear();
            string[] names = ConfigurationManager.AppSettings.AllKeys;
            NameValueCollection appStgs = ConfigurationManager.AppSettings;

            for (int i = 0; i < appStgs.Count; i++)
            {
                DeleteComboBox.Items.Add(names[i]);
            }
        }

        public void RemoveAppSetting()
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to remove " + SelectedPractice, "Confirm", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var settings = ((AppSettingsSection)configFile.GetSection("appSettings")).Settings;
                    settings.Remove(SelectedPractice);
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    //this.Hide();
                }
                catch (ConfigurationErrorsException)
                {
                    Console.WriteLine("Error deleting app settings");
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedPractice = DeleteComboBox.Text;
            RemoveAppSetting();
            this.Close();
            //Application.Current.MainWindow.ShowDialog();
        }

        private void DeleteCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.ShowDialog();
        }

    }
}
