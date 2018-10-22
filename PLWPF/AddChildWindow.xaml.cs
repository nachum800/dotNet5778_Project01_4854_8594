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
    /// Interaction logic for AddChildWindow.xaml
    /// </summary>
    public partial class AddChildWindow : Window
    {
        private Child ChlidGlobal;
        private Mother TempMother;
        private IBL bl = BLSingleton.GetBL;
        public AddChildWindow(Mother mother)
        {
            try
            {
                ChlidGlobal=new Child();
                TempMother = mother;
                InitializeComponent();
                ChlidGlobal.MotherID = mother.ID;
                ChlidGlobal.Birthday=DateTime.Today;
                this.DataContext = ChlidGlobal;
                motherIDTextBox.Text = TempMother.ID.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void AddChildCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddChildOKBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddChild(ChlidGlobal);
                MessageBox.Show($"{ChlidGlobal.Name} was added successfully!", "Info");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
