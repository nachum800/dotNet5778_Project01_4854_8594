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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (PasswordBox.Password.ToString() == "1234")
                {
                    new ManagerMenuWindow().Show();
                    this.Close();
                }
                else
                {
                    throw new Exception("Password is incorrect!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Wrong Password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

      
    }
}
