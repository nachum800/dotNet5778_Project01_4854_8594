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
    /// Interaction logic for GetAllNannyWindow.xaml
    /// </summary>
    public partial class GetAllNannyContractWindow : Window
    {
        private IBL bl = BLSingleton.GetBL;
        public GetAllNannyContractWindow(Nanny nanny)
        {
            InitializeComponent();
            GetContractDataGrid.ItemsSource = bl.GetContracts(c => c.NannyId == nanny.ID).ToList();
        }


        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetContractDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(((Contract) GetContractDataGrid.SelectedItem).ToString(), "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
