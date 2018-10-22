using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    ///     Interaction logic for ChooseChildUpdateContract.xaml
    /// </summary>
    public partial class ChooseChildUpdateContract : Window
    {
        private Mother ContractMother = new Mother();
        private Contract GlobalContract;
        private Child GlobalChild;
        private readonly IBL bl = BLSingleton.GetBL;

        public ChooseChildUpdateContract(Mother mother)
        {
            InitializeComponent();
            ContractMother = mother;
            ChooseChildComboBox.ItemsSource = bl.GetChildrenByMother(mother.ID);
            UpdateBtn.IsEnabled = false;
        }

        private void ChooseChildComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                GlobalChild = new Child();
                GlobalChild = (Child) ChooseChildComboBox.SelectedItem;
                contractDataGrid.ItemsSource = bl.GetContracts(c => c.ChildId == GlobalChild.ID).ToList();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error - {exception.Message}","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var contract = (Contract) contractDataGrid.SelectedItem;
                var updateContractWindow = new UpdateContractWindow(contract);
                updateContractWindow.Closed += UpdateContractWindow_Closed;
                updateContractWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error - {exception.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateContractWindow_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void ContractDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBtn.IsEnabled = true;
        }
    }
}