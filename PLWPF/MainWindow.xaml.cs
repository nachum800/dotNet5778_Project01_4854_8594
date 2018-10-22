using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BL.IBL bl;
        public static Nanny nannyMain;
        public static Mother motherMain;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                bl = BLSingleton.GetBL;
                nannyMain=new Nanny();
                this.SizeChanged += OnWindowSizeChanged;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        

        private void MotherEnteryBtn_Click(object sender, RoutedEventArgs e)
        {
            MotherMenuGrid.Visibility = Visibility.Visible;
        }

        private void NannyMenuGrid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            NannyMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void NannyEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NannyMenuGrid.Visibility = Visibility.Visible;
        }

        private void MotherMenuGrid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            MotherMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void NannyOptionBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NannyIdCheckGrid.Visibility = Visibility.Collapsed;
        }

        private void NannyExistingBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            NannyIdCheckGrid.Visibility = Visibility.Visible;
            NannyIdTextBox.Text = "Please enter your ID here...";
            NannyIdTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
            NannyIdTextBox.Opacity = 50;
        }

        private void MotherExistingBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MotherIdCheckGrid.Visibility = Visibility.Visible;

            MotherIdTextBox.Text = "Please enter your ID here...";
            MotherIdTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void NannyIdOkBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var input = NannyIdTextBox.Text;
                nannyMain = bl.GetNanny(Convert.ToInt32(input));
                if (nannyMain == null)
                    MessageBox.Show("Nanny doesn't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    NannyWelcomeBanner.Content = $"Welcome: {nannyMain.FirstName} {nannyMain.LastName}!";
                    NannyOptionGrid.Visibility = Visibility.Visible;
                    NannyIdCheckGrid.Visibility = Visibility.Collapsed;
                }
               
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NannyIdTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (NannyIdTextBox.Text == "Please enter your ID here...")
            {
                NannyIdTextBox.Text = "";
                NannyIdTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void OnWindowSizeChanged(Object sender, SizeChangedEventArgs e)
        {
            var ChangeRatio = (e.PreviousSize.Width==0||e.PreviousSize.Height==0)?1:(e.NewSize.Height + e.NewSize.Width) /
                              (e.PreviousSize.Height + e.PreviousSize.Width);
            ChangeTextSize.RecurseChangeRatio(MainWindow_OuterGrid,ChangeRatio);
        }

        private void NannyIdTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (NannyIdTextBox.Text == "")
            {
                NannyIdTextBox.Text = "Please enter your ID here...";
                NannyIdTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void NannyOptionBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NannyOptionGrid.Visibility = Visibility.Collapsed;
        }

        private void NannyRemoveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show($"Are you sure you want to remove {nannyMain.FirstName} {nannyMain.LastName}?",
                    "Confirmation:",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show(
                        bl.RemoveNanny(nannyMain.ID)
                            ? $"{nannyMain.FirstName} {nannyMain.LastName} was removed!"
                            : $"{nannyMain.FirstName} {nannyMain.LastName} wasn't removed!", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    NannyOptionGrid.Visibility = Visibility.Collapsed;
                    nannyMain = new Nanny();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OnNannyUpdateWindowChanged(Object sender, EventArgs eventArgs)
        {
            NannyWelcomeBanner.Content = $"Welcome: {nannyMain.FirstName} {nannyMain.LastName}!";
        }

        private void NannyNewBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new AddNannyWindow().ShowDialog();
        }

        private void NannyUpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateNanny update = new UpdateNanny(nannyMain);
            update.Show();
            update.Closed += OnNannyUpdateWindowChanged;
        }

        private void NannyOptionGetContractBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var NannyContractWindow =new GetAllNannyContractWindow(nannyMain);
            NannyContractWindow.ShowDialog();
        }

        public void helloWorld()
        {
            MessageBox.Show("Hello World");
        }

        private void MotherNewBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var motherWindow=new NewMotherWindow();
            motherWindow.ShowDialog();
            motherWindow.Closed += OnMotherOptionsCloses;
        }

        private void MotherIdOkBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var input = MotherIdTextBox.Text;
                motherMain = bl.GetMother(Convert.ToInt32(input));
                if (motherMain == null)
                    MessageBox.Show("Mother doesn't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    new MotherOptionsWindow(motherMain).ShowDialog();
                    MotherIdCheckGrid.Visibility = Visibility.Collapsed;
                }
               
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void OnMotherOptionsCloses(Object sender, EventArgs e)
        {
            MotherIdTextBox.Text="";
        }

        private void MotherOptionBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MotherIdCheckGrid.Visibility = Visibility.Collapsed;
            
        }

        private void MotherIdTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (MotherIdTextBox.Text == "Please enter your ID here...")
            {
                MotherIdTextBox.Text = "";
                MotherIdTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void MotherIdTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (MotherIdTextBox.Text == "")
            {
                MotherIdTextBox.Text = "Please enter your ID here...";
                MotherIdTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void AppLogo_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            new PasswordWindow().ShowDialog();
        }
    }
}
