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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateContractWindow.xaml
    /// </summary>
    public partial class UpdateContractWindow : Window
    {
        private IBL bl = BLSingleton.GetBL;
        private Contract CurrentContract=new Contract();
        private Contract CopyContract=new Contract();
        public UpdateContractWindow(Contract contract)
        {
            InitializeComponent();
            CurrentContract = contract;
            CopyContract = contract.GetCopy();
            ContractGrid.DataContext = CopyContract;

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentContract = CopyContract;
            if (CurrentContract.StartDate.CompareTo(CurrentContract.EndDate) > 0)
            {
                throw new Exception("Start date must be before end date!");
            }
            var btnresult = MessageBox.Show("Update Contract?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (btnresult == MessageBoxResult.OK)
            {
                bl.UpdateContract(CurrentContract);
                MessageBox.Show($"Contract #{CurrentContract.ContractNumber} was updated successfully", $"Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CopyContract = CurrentContract;
            this.Close();
        }
    }
}
