using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for newContractWindow.xaml
    /// </summary>
    public partial class newContractWindow : Window
    {
        private IBL bl = BLSingleton.GetBL;
        private Contract CurrentContract;
        private Contract CopyContract;
        public newContractWindow(Contract contract)
        {
            InitializeComponent();
            CurrentContract = contract;
            CopyContract = contract.GetCopy();
            DataContext = contract;
            
        }


        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentContract.StartDate.CompareTo(CurrentContract.EndDate) > 0)
                {
                    throw new Exception("Start date must be before end date!");
                }
                var btnresult= MessageBox.Show("Add Contract?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (btnresult == MessageBoxResult.OK)
                {
                    CurrentContract.Signed = true;
                    bl.AddContract(CurrentContract);
                }
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error - {exception.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentContract = CopyContract;
            this.Close();
        }
    }
}
