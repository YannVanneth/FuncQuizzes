using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
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

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Page
    {
        public AboutUs()
        {
            InitializeComponent();
        }


        private void Avar_1_Email_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://mail.google.com/mail/?view=cm&fs=1&to=oeundavid235@gmail.com&su=Hello&body=Hi%20David";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void Avar_1_Facebook_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://facebook.com/mcdavid2u";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }
        }

        private void Avar_1_School_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://training.antkh.com/students/?s=5003";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }
        }



        private void Avar_2_Email_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Avar_2_Facebook_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.facebook.com/yannvanneth";
            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void Avar_2_School_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://training.antkh.com/students/?s=5216";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }
        }

        private void Avar_3_Email_Click(object sender, RoutedEventArgs e)
        {
            // Gmail URL for composing an email
            string gmailComposeUrl = "https://mail.google.com/mail/?view=cm&fs=1&to=taemmarina@gmail.com&su=Hello&body=Hi%20Marina";

            try
            {

                Process.Start(new ProcessStartInfo(gmailComposeUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open Gmail: {ex.Message}");
            }
        }

        private void Avar_3_Facebook_Click(object sender, RoutedEventArgs e)
        {

            string url = "https://www.facebook.com/taem.marina";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void Avar_3_School_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://training.antkh.com/students/?s=5219";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void Avar_4_Email_Click(object sender, RoutedEventArgs e)
        {

            string url = "https://mail.google.com/mail/?view=cm&fs=1&to=prach.boroeurn@gmail.com&su=Hello&body=Hi%20Boroeurn";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void Avar_4_Facebook_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.facebook.com/BoroeurnDev";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void Avar_4_School_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://training.antkh.com/students/?s=5216";

            try
            {
                // Open the link in the default web browser
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}");
            }

        }

        private void button_back(object sender, RoutedEventArgs e)
        {

            MainWindow? mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.Main.Content = new pages.HomeScreen();
            }


        }
    }
}
