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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for NewExistingCtr.xaml
    /// </summary>
    public partial class NewExistingCtr : UserControl
    {
        public NewExistingCtr()
        {
            InitializeComponent();
            List<Time> times = new List<Time>();
            for (int i = 8; i < 20; i++)
            {
                times.Add(new Time(i - 1, 30));
                times.Add(new Time(i, 0));
            }
            StartTimeCmbx.ItemsSource = times;
        }

    }
}
