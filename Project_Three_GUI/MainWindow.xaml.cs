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

namespace Project_Three_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            invalidCredentials.Visibility = Visibility.Hidden;
        }
        private void OnMouseCapture(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
            else
            {
                var passBox = e.OriginalSource as PasswordBox;
                if (passBox != null)
                {
                    passBox.SelectAll();
                }
            }
        }
        private void OnKeyboardFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
            else
            {
                var passBox = e.OriginalSource as PasswordBox;
                if (passBox != null)
                {
                    passBox.SelectAll();
                }
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                Login();
            }
        }
        private void LoginButton_Click(Object sender, RoutedEventArgs e)
        {
            Login();
        }
        private void Login()
        {
            if (Username.Text == "home" && Password.Password == "1234")
            {
                SelectionWindow newWindow = new SelectionWindow();
                newWindow.Show();
                this.Close();
            }
            else
            {
                invalidCredentials.Visibility = Visibility.Visible;
            }
        }
    }
}
