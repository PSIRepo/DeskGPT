using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace dGPT
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    ///

    public partial class Settings : Window
    {
        private bool comChanged = false;

        public Settings()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception a) { }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Settings1.Default.APIKey = apiT.Text;
            Settings1.Default.temperature = tempS.Value;
            Settings1.Default.frequencyPenalty = freqS.Value;
            Settings1.Default.presencePenalty = preS.Value;
            Settings1.Default.bestOf = Convert.ToInt32(boS.Value);
            Settings1.Default.topProbability = Convert.ToInt32(probS.Value);
            Settings1.Default.Startup = (Startup.IsChecked == true ? true : false);
            Settings1.Default.startupVisible = (Max.IsChecked == true ? true : false);
            Settings1.Default.Model = (modelBox.SelectedIndex == 0 ? "gpt-3.5-turbo" : "text-davinci-003");
            Settings1.Default.Modifier = (modiBox.SelectedIndex == 0 ? "WIN" : (modiBox.SelectedIndex == 1 ? "ALT" : "CTRL"));
            Settings1.Default.Key = ((ComboBoxItem)keyBox.SelectedItem).Content.ToString();
            Settings1.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            apiT.Text = "";
            tempS.Value = 0.5;
            freqS.Value = 1;
            preS.Value = 2;
            boS.Value = 2;
            probS.Value = 50;
            modelBox.SelectedIndex = 0;
            modiBox.SelectedIndex = 0;
            keyBox.SelectedIndex = 2;
            Startup.IsChecked = true;
            Max.IsChecked = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void Slider_MouseEnter(object sender, MouseEventArgs e)
        {
            tempD.Visibility = Visibility.Visible;
        }

        private void Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            tempD.Visibility = Visibility.Hidden;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                tempD.Content = tempS.Value.ToString("#0.00");
            }
            catch (Exception f) { }
        }

        private void freqS_MouseEnter(object sender, MouseEventArgs e)
        {
            freqD.Visibility = Visibility.Visible;
        }

        private void freqS_MouseLeave(object sender, MouseEventArgs e)
        {
            freqD.Visibility = Visibility.Hidden;
        }

        private void freqS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                freqD.Content = freqS.Value.ToString("#0.00");
            }
            catch (Exception f) { }
        }

        private void preS_MouseEnter(object sender, MouseEventArgs e)
        {
            preD.Visibility = Visibility.Visible;
        }

        private void preS_MouseLeave(object sender, MouseEventArgs e)
        {
            preD.Visibility = Visibility.Hidden;
        }

        private void preS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                preD.Content = preS.Value.ToString("#0.00");
            }
            catch (Exception f) { }
        }

        private void boS_MouseEnter(object sender, MouseEventArgs e)
        {
            boD.Visibility = Visibility.Visible;
        }

        private void boS_MouseLeave(object sender, MouseEventArgs e)
        {
            boD.Visibility = Visibility.Hidden;
        }

        private void boS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                boD.Content = boS.Value.ToString("#0");
            }
            catch (Exception f) { }
        }

        private void probS_MouseEnter(object sender, MouseEventArgs e)
        {
            probD.Visibility = Visibility.Visible;
        }

        private void probS_MouseLeave(object sender, MouseEventArgs e)
        {
            probD.Visibility = Visibility.Hidden;
        }

        private void probS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                probD.Content = probS.Value.ToString("#0");
            }
            catch (Exception f) { }
        }

        private void SettingsF_Loaded(object sender, RoutedEventArgs e)
        {
            apiT.Text = Settings1.Default.APIKey;
            tempS.Value = Settings1.Default.temperature;
            freqS.Value = Settings1.Default.frequencyPenalty;
            preS.Value = Settings1.Default.presencePenalty;
            boS.Value = Settings1.Default.bestOf;
            probS.Value = Settings1.Default.topProbability;
            Startup.IsChecked = Settings1.Default.Startup;
            Max.IsChecked = Settings1.Default.startupVisible;
            modelBox.SelectedIndex = (Settings1.Default.Model == "gpt-3.5-turbo" ? 0 : 1);
            modiBox.SelectedIndex = (Settings1.Default.Modifier == "WIN" ? 0 : (Settings1.Default.Modifier == "ALT" ? 1 : 2));
            keyBox.SelectedIndex = ((int)char.ToUpper(Convert.ToChar(Settings1.Default.Key))) - 65;
            comChanged = false;
        }

        private void CloseB_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SettingsF_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (apiT.Text != Settings1.Default.APIKey || tempS.Value != Settings1.Default.temperature || freqS.Value != Settings1.Default.frequencyPenalty || preS.Value != Settings1.Default.presencePenalty || boS.Value != Settings1.Default.bestOf || probS.Value != Settings1.Default.topProbability || Startup.IsChecked != Settings1.Default.Startup || Max.IsChecked != Settings1.Default.startupVisible || ((modelBox.Text.Contains("3.5") ? "gpt-3.5-turbo" : "text-davinci-003") != Settings1.Default.Model) || !((ComboBoxItem)modiBox.SelectedItem).Content.ToString().Contains(Settings1.Default.Modifier) || ((ComboBoxItem)keyBox.SelectedItem).Content.ToString() != Settings1.Default.Key)
            {
                Alert alert = new Alert("Confirm", "Are you sure you want to exit without saving your changes?", 0);
                alert.ShowDialog();
                var resp = alert.Result;
                if (resp)
                {
                    if (comChanged)
                    {
                        Alert alert1 = new Alert("Alert", "You must restart the application for hotkey changes to take effect.", 1);
                        alert1.ShowDialog();
                        var resp2 = alert1.Result;
                        if (resp2)
                        {
                            comChanged = false;
                            return;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    return;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                RegistryKey su = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (Startup.IsChecked == true)
                {
                    string name = Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");
                    string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    string path = Path.Combine(dir, name);
                    path = "\"" + path + "\"";
                    su.SetValue("dGPT", path);
                }
                else
                {
                    try
                    {
                        su.DeleteValue("dGPT", false);
                    }
                    catch (Exception ex) { }
                }
                if (comChanged)
                {
                    Alert alert = new Alert("Alert", "You must restart the application for hotkey changes to take effect.", 1);
                    alert.ShowDialog();
                    var resp = alert.Result;
                    if (resp)
                    {
                        comChanged = false;
                        return;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void modiBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comChanged = true;
        }
    }
}