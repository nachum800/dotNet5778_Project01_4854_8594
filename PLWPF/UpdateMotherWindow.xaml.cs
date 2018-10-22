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
    /// Interaction logic for UpdateMotherWindow.xaml
    /// </summary>
    public partial class UpdateMotherWindow : Window
    {
        public static Schedule[] Schedules { get; set; }
        public static Mother MotherGlobal;
        public static Mother MotherCancel;
        private BL.IBL bl;

        public UpdateMotherWindow(Mother Mother)
        {
            try
            {

                this.SizeChanged += OnSizeChanged;
                InitializeComponent();
                Schedules = new Schedule[6];
                MotherCancel = Mother.GetCopy();
                MotherGlobal = Mother;
                this.DataContext = Mother;
                this.monthlyOrHourlyComboBox.ItemsSource = Enum.GetValues(typeof(BE.MonthlyOrHourly));
                bl = BLSingleton.GetBL;
                List<Time> times = new List<Time>();
                for (int i = 8; i < 20; i++)
                {
                    times.Add(new Time(i - 1, 30));
                    times.Add(new Time(i, 0));
                }
                StartTimeCmbx1.ItemsSource = times;
                #region InitMother
                Cb1.IsChecked = Mother.Schedule[0].IsWorking;
                if (Mother.Schedule[0].IsWorking)
                {
                    StartTimeCmbx1.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[0].StartTime) == 0);
                    EndTimeCmbx1.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[0].EndTime) == 0);
                }
                Cb2.IsChecked = Mother.Schedule[1].IsWorking;
                if (Mother.Schedule[1].IsWorking)
                {
                    StartTimeCmbx2.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[1].StartTime) == 0);
                    EndTimeCmbx2.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[1].EndTime) == 0);
                }
                Cb3.IsChecked = Mother.Schedule[2].IsWorking;
                if (Mother.Schedule[2].IsWorking)
                {
                    StartTimeCmbx3.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[2].StartTime) == 0);
                    EndTimeCmbx3.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[2].EndTime) == 0);
                }
                Cb4.IsChecked = Mother.Schedule[3].IsWorking;
                if (Mother.Schedule[3].IsWorking)
                {
                    StartTimeCmbx4.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[3].StartTime) == 0);
                    EndTimeCmbx4.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[3].EndTime) == 0);

                }
                Cb5.IsChecked = Mother.Schedule[4].IsWorking;
                if (Mother.Schedule[4].IsWorking)
                {
                    StartTimeCmbx5.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[4].StartTime) == 0);
                    EndTimeCmbx5.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[4].EndTime) == 0);

                }
                Cb6.IsChecked = Mother.Schedule[5].IsWorking;
                if (Mother.Schedule[5].IsWorking)
                {
                    StartTimeCmbx6.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[5].StartTime) == 0);
                    EndTimeCmbx6.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(Mother.Schedule[5].EndTime) == 0);
                }


                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var changeRatio = (e.PreviousSize.Width == 0 || e.PreviousSize.Height == 0) ? 1 : (e.NewSize.Height + e.NewSize.Width) /
                                                                                              (e.PreviousSize.Height + e.PreviousSize.Width);
            ChangeTextSize.RecurseChangeRatio(InfoGrid, changeRatio); ;
        }


        private void OnUpdateMotherBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                List<CheckBox> cb = SchedualeGrid.Children.OfType<CheckBox>().ToList();
                List<ComboBox> Combo = SchedualeGrid.Children.OfType<ComboBox>().ToList();
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
                    MotherGlobal.Schedule = new Schedule[6];
                    MotherGlobal.Schedule = Schedules;
                }
                foreach (var schedule in MotherGlobal.Schedule)
                {
                    if ((schedule.IsWorking && schedule.StartTime == null) || (schedule.IsWorking && schedule.EndTime == null) || (schedule.IsWorking && schedule.StartTime.CompareTo(schedule.EndTime) >= 0))
                        throw new FormatException("Please check your times input and try again");
                }
                bl.UpdateMother(MotherGlobal);
                // MotherOptionsWindow.MotherOption = MotherGlobal;
                MessageBox.Show($"{MotherGlobal.FirstName} {MotherGlobal.LastName} was updated successfully", "info");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelBtnClk(object sender, RoutedEventArgs e)
        {
            MotherOptionsWindow.MotherOption = MotherCancel;
            this.Close();

        }
    }
}
