using System.Windows;
using System.Windows.Input;

namespace dGPT
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Alert : Window
    {
        public bool Result { get; set; } = false;
        private int bSetS;

        public Alert(string title, string message, int bSet)
        {
            InitializeComponent();
            bSetS = bSet;
            titleB.Content = title;
            messageB.Text = message;
            if (bSet != 0)
            {
                ConfirmB.Visibility = Visibility.Visible;

                YesB.Visibility = Visibility.Hidden;
                NoB.Visibility = Visibility.Hidden;
            }
            this.Focus();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseB_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }

        private void ConfirmB_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void AlertF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (bSetS != 0)
                {
                    ConfirmB_Click(ConfirmB, e);
                }
                else
                {
                    Button_Click(sender, e);
                }
            }
        }
    }
}