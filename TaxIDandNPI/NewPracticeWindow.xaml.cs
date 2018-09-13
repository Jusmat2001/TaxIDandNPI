using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewPracticeWindow.xaml
    /// </summary>
    public partial class NewPracticeWindow : Window
    {
        bool NameBoxHasBeenClicked = false;
        bool TidBoxHasBeenClicked = false;
        bool NpiBoxHasBeenClicked = false;

        
        public NewPracticeWindow()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void AddNewPracticeBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Sure?", "Confirm", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                string v1 = NewTidBox.Text + "," + NewNpiBox.Text;

                (Application.Current.MainWindow as MainWindow).AddUpdateAppSettings(NewPracticeBox.Text, v1);
                this.Close();
                //Application.Current.MainWindow.ShowDialog();
            }
        }

        private void NewPracticeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!NameBoxHasBeenClicked)
            {
                TextBox box = sender as TextBox;
                box.Text = String.Empty;
                NameBoxHasBeenClicked = true;
            }
        }

        private void NewTidBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!TidBoxHasBeenClicked)
            {
                TextBox box = sender as TextBox;
                box.Text = String.Empty;
                TidBoxHasBeenClicked = true;
            }
        }

        private void NewNpiBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!NpiBoxHasBeenClicked)
            {
                TextBox box = sender as TextBox;
                box.Text = String.Empty;
                NpiBoxHasBeenClicked = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.ShowDialog();
        }

        


    }
}
