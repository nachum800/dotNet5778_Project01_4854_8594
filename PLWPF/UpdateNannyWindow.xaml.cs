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
    /// Interaction logic for UpdateNanny.xaml
    /// </summary>
    public partial class UpdateNanny : Window
    {
        public static Schedule[] Schedules { get; set; }
        public static Nanny NannyGlobal;
        public static Nanny NannyCancel;
        private BL.IBL bl;
        

        public UpdateNanny(Nanny nanny)
        {
            try
            {

                this.SizeChanged += OnSizeChanged;
                InitializeComponent();
                Schedules=new Schedule[6];
                NannyCancel = nanny.GetCopy();
                NannyGlobal = nanny;
                this.DataContext = nanny;
                bl = BLSingleton.GetBL;
                List<Time> times = new List<Time>();
                for (int i = 8; i < 20; i++)
                {
                    times.Add(new Time(i - 1, 30));
                    times.Add(new Time(i, 0));
                }
                StartTimeCmbx1.ItemsSource = times;
                #region InitNanny
                Cb1.IsChecked = nanny.Schedule[0].IsWorking;
                if (nanny.Schedule[0].IsWorking)
                {
                    StartTimeCmbx1.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[0].StartTime) == 0);
                    EndTimeCmbx1.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[0].EndTime) == 0);
                }
                Cb2.IsChecked = nanny.Schedule[1].IsWorking;
                if (nanny.Schedule[1].IsWorking)
                {
                    StartTimeCmbx2.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[1].StartTime) == 0);
                    EndTimeCmbx2.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[1].EndTime) == 0);
                }
                Cb3.IsChecked = nanny.Schedule[2].IsWorking;
                if (nanny.Schedule[2].IsWorking)
                {
                    StartTimeCmbx3.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[2].StartTime) == 0);
                    EndTimeCmbx3.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[2].EndTime) == 0);
                }
                Cb4.IsChecked = nanny.Schedule[3].IsWorking;
                if (nanny.Schedule[3].IsWorking)
                {
                    StartTimeCmbx4.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[3].StartTime) == 0);
                    EndTimeCmbx4.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[3].EndTime) == 0);
                    
                }
                Cb5.IsChecked = nanny.Schedule[4].IsWorking;
                if (nanny.Schedule[4].IsWorking)
                {
                    StartTimeCmbx5.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[4].StartTime) == 0);
                    EndTimeCmbx5.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[4].EndTime) == 0);
                    
                }
                Cb6.IsChecked = nanny.Schedule[5].IsWorking;
                if (nanny.Schedule[5].IsWorking)
                {
                    StartTimeCmbx6.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[5].StartTime) == 0);
                    EndTimeCmbx6.SelectedItem =
                        times.FirstOrDefault(t => t.CompareTo(nanny.Schedule[5].EndTime) == 0);
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
            ChangeTextSize.RecurseChangeRatio(MainUpdateNannyGrid, changeRatio); ;
        }


        private void OnAddNannyBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                List<CheckBox> cb = NannySceduleGrid.Children.OfType<CheckBox>().ToList();
                List<ComboBox> Combo = NannySceduleGrid.Children.OfType<ComboBox>().ToList();
                for (int i = 0; i < 6; i++)
                {
                    Schedules[i]=new Schedule();
                    var TempCheckBox = cb.First(c => c.Name == $"Cb{i+1}");
                    Schedules[i].IsWorking = (bool) TempCheckBox.IsChecked;
                    if (Schedules[i].IsWorking)
                    {
                        var StartTemp = Combo.First(c => c.Name == $"StartTimeCmbx{i + 1}");
                        var EndTemp = Combo.First(c => c.Name == $"EndTimeCmbx{i + 1}");
                        Schedules[i].StartTime = new Time(StartTemp.SelectionBoxItem as Time);
                        Schedules[i].EndTime = new Time(EndTemp.SelectionBoxItem as Time);
                    }
                    NannyGlobal.Schedule = new Schedule[6];
                    NannyGlobal.Schedule = Schedules;
                }
                foreach (var schedule in NannyGlobal.Schedule)
                {
                    if((schedule.IsWorking&&schedule.StartTime==null)||(schedule.IsWorking && schedule.EndTime==null)||(schedule.IsWorking && schedule.StartTime.CompareTo(schedule.EndTime)>=0))
                        throw new Exception("Please check your times input and try again");
                }
                bl.UpdateNanny(NannyGlobal);
                MessageBox.Show($"{NannyGlobal.FirstName} {NannyGlobal.LastName} was updated successfully", "info");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void CancelAddNannyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.nannyMain=NannyCancel;
        }
    }
}
