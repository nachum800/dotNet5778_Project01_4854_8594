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
    /// Interaction logic for ManagerMenuWindow.xaml
    /// </summary>
    public partial class ManagerMenuWindow : Window
    {
        private IBL bl = BLSingleton.GetBL;
        public ManagerMenuWindow()
        {
            InitializeComponent();
            List<string> menuList= new List<string>();
            menuList.Add("All mothers");
            menuList.Add("All nannies");
            menuList.Add("All contracts");
            menuList.Add("All children");
            MenuComboBox.ItemsSource = menuList;
        }


        private void MenuComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((string)MenuComboBox.SelectedItem))
            {
                case "All mothers":
                    motherDataGrid.Visibility = Visibility.Visible;
                    nannyDataGrid.Visibility = Visibility.Collapsed;
                    contractDataGrid.Visibility = Visibility.Collapsed;
                    childDataGrid.Visibility = Visibility.Collapsed;
                    motherDataGrid.ItemsSource=bl.GetMothers().ToList();
                    break;
                case "All nannies":
                    motherDataGrid.Visibility = Visibility.Collapsed;
                    nannyDataGrid.Visibility = Visibility.Visible;
                    contractDataGrid.Visibility = Visibility.Collapsed;
                    childDataGrid.Visibility = Visibility.Collapsed;
                    nannyDataGrid.ItemsSource = bl.GetNannies().ToList();
                    break;
                case "All contracts":
                    motherDataGrid.Visibility = Visibility.Collapsed;
                    nannyDataGrid.Visibility = Visibility.Collapsed;
                    contractDataGrid.Visibility = Visibility.Visible;
                    childDataGrid.Visibility = Visibility.Collapsed;
                    contractDataGrid.ItemsSource = bl.GetContracts().ToList();
                    break;
                case "All children":
                    motherDataGrid.Visibility = Visibility.Collapsed;
                    nannyDataGrid.Visibility = Visibility.Collapsed;
                    contractDataGrid.Visibility = Visibility.Collapsed;
                    childDataGrid.Visibility = Visibility.Visible;
                    List<Child> temp=new List<Child>();
                    foreach (var mother in bl.GetMothers())
                    {
                        temp.AddRange(bl.GetChildrenByMother(mother.ID));
                    }
                    childDataGrid.ItemsSource = temp;
                    break;


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
