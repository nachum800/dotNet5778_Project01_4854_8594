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
    /// Interaction logic for MotherContractsWindow.xaml
    /// </summary>
    public partial class MotherContractsWindow : Window
    {
        private IBL bl = BLSingleton.GetBL;

        public MotherContractsWindow(Mother mother)
        {

            InitializeComponent();
            GetContractDataGrid.ItemsSource = bl.GetContracts(c => c.MotherId == mother.ID).ToList();

        }

        private void ViewContractBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

