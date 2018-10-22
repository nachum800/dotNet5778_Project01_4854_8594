using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for RemoveContractWindow.xaml
    /// </summary>
    public partial class RemoveContractWindow : Window
    {
        private IBL bl = BLSingleton.GetBL;

        public RemoveContractWindow(Mother mother)
        {
            InitializeComponent();
            GetContractDataGrid.ItemsSource = bl.GetContracts(c => c.MotherId == mother.ID).ToList();
            RemoveBtn.IsEnabled = false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GetContractDataGrid.SelectedIndex==-1)
                {
                    throw new Exception("No contract has selected!");
                }
                var messege = MessageBox.Show($"Are you sure?", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (messege == MessageBoxResult.Yes)
                {
                    bl.RemoveContract(((Contract) GetContractDataGrid.SelectedItem).ContractNumber);
                    GetContractDataGrid.ItemsSource = bl.GetContracts(c => c.MotherId == MotherOptionsWindow.MotherOption.ID).ToList();


                }
                else
                    this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error - {exception.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetContractDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveBtn.IsEnabled = true;
        }
    }
}
