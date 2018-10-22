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
    /// Interaction logic for AddNannyWindow.xaml
    /// </summary>
    public partial class AddNannyWindow : Window
    {
        public static Schedule[] Schedules { get; set; }
        public static Nanny nanny;
        private BL.IBL bl;
        private CheckBox cb;
        private ComboBox comboStart;
        private ComboBox comboEnd;
        public AddNannyWindow()
        {
            try
            {
                InitializeComponent();
                //Schedules=new Schedule[6];
                nanny = new Nanny();
                nanny.Birthday=new DateTime(DateTime.Now.Year-50,1,1);
                this.DataContext = nanny;
                bl = BLSingleton.GetBL;
                List<Time> times = new List<Time>();
                for (int i = 8; i < 20; i++)
                {
                    times.Add(new Time(i - 1, 30));
                    times.Add(new Time(i, 0));
                }
                StartTimeCmbx1.ItemsSource = times;
                
                
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OnAddNannyBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
               
                nanny.Schedule=new Schedule[6];
                nanny.Schedule[0] = new Schedule(Cb1.IsChecked.Value, (Time)StartTimeCmbx1.SelectedItem, (Time)EndTimeCmbx1.SelectedItem);
                nanny.Schedule[1] = new Schedule(Cb2.IsChecked.Value, (Time)StartTimeCmbx2.SelectedItem, (Time)EndTimeCmbx2.SelectedItem);
                nanny.Schedule[2] = new Schedule(Cb3.IsChecked.Value, (Time)StartTimeCmbx3.SelectedItem, (Time)EndTimeCmbx3.SelectedItem);
                nanny.Schedule[3] = new Schedule(Cb4.IsChecked.Value, (Time)StartTimeCmbx4.SelectedItem, (Time)EndTimeCmbx4.SelectedItem);
                nanny.Schedule[4] = new Schedule(Cb5.IsChecked.Value, (Time)StartTimeCmbx5.SelectedItem, (Time)EndTimeCmbx5.SelectedItem);
                nanny.Schedule[5] = new Schedule(Cb6.IsChecked.Value, (Time)StartTimeCmbx6.SelectedItem, (Time)EndTimeCmbx6.SelectedItem);
                foreach (var schedule in nanny.Schedule)
                {
                    if ((schedule.IsWorking && schedule.StartTime == null) || (schedule.IsWorking && schedule.EndTime == null) || (schedule.IsWorking && schedule.StartTime.CompareTo(schedule.EndTime) >= 0))
                        throw new FormatException("Please check your times input and try again");
                }
                bl.AddNanny(nanny);
                MessageBox.Show($"{nanny.FirstName} {nanny.LastName} was added successfully", "info");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        

        private void CancelAddNannyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
