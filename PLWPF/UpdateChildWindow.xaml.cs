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
    /// Interaction logic for UpdateChildWindow.xaml
    /// </summary>
    public partial class UpdateChildWindow : Window
    {
        private Child ChildGlobal;
        private Child ChildCopy;
        private BL.IBL bl = BLSingleton.GetBL;

        public UpdateChildWindow(Child child)
        {
            try
            {
                ChildCopy = child.GetCopy();
                ChildGlobal = child;
                DataContext = ChildCopy;
                InitializeComponent();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateChildCancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ChildCopy = ChildGlobal;
            this.Close();
        }

        private void UpdateChildOKBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateChild(ChildCopy);
                MessageBox.Show($"{ChildCopy.Name} was updated successfully", "Info");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
