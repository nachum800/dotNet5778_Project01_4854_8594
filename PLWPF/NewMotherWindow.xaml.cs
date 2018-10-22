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
    /// Interaction logic for NewMotherWindow.xaml
    /// </summary>
    public partial class NewMotherWindow : Window
    {
        public static Schedule[] Schedules { get; set; }
        public static Mother mother;
        private BL.IBL bl;
        public NewMotherWindow()
        {
            try
            {
                InitializeComponent();
                Schedules=new Schedule[6];
                mother = new Mother();
                this.DataContext = mother;
                bl = BLSingleton.GetBL;
                List<Time> times = new List<Time>();
                for (int i = 8; i < 20; i++)
                {
                    times.Add(new Time(i - 1, 30));
                    times.Add(new Time(i, 0));
                }
                StartTimeCmbx1.ItemsSource = times;
                this.monthlyOrHourlyComboBox.ItemsSource = Enum.GetValues(typeof(BE.MonthlyOrHourly));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddMotherBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                List<CheckBox> cb = MotherScheduleGrid.Children.OfType<CheckBox>().ToList();
                List<ComboBox> Combo = MotherScheduleGrid.Children.OfType<ComboBox>().ToList();
                for (int i = 0; i < 6; i++)
                {
                    Schedules[i] = new Schedule();
                    var TempCheckBox = cb.First(c => c.Name == $"Cb{i + 1}");
                    Schedules[i].IsWorking = (bool)TempCheckBox.IsChecked;
                    if (Schedules[i].IsWorking)
                    {
                        var StartTemp = Combo.First(c => c.Name == $"StartTimeCmbx{i + 1}");
                        var EndTemp = Combo.First(c => c.Name == $"EndTimeCmbx{i + 1}");
                        Schedules[i].StartTime = new Time(StartTemp.SelectionBoxItem as Time);
                        Schedules[i].EndTime = new Time(EndTemp.SelectionBoxItem as Time);
                    }
                    mother.Schedule = new Schedule[6];
                    mother.Schedule = Schedules;
                }
                foreach (var schedule in mother.Schedule)
                {
                    if ((schedule.IsWorking && schedule.StartTime == null) || (schedule.IsWorking && schedule.EndTime == null) || (schedule.IsWorking && schedule.StartTime.CompareTo(schedule.EndTime) >= 0))
                        throw new FormatException("Please check your times input and try again");
                }
                bl.AddMother(mother);
                MessageBox.Show($"{mother.FirstName} {mother.LastName} was added successfully", "info");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelMotherBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewMotherWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var changeRatio = (e.PreviousSize.Width == 0 || e.PreviousSize.Height == 0) ? 1 : (e.NewSize.Height + e.NewSize.Width) /
                                                                                              (e.PreviousSize.Height + e.PreviousSize.Width);
            ChangeTextSize.RecurseChangeRatio(NewMotherGrid, changeRatio*0.95); ;
        }
    }
}
