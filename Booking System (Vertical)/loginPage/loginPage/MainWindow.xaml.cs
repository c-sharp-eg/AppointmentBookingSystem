using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace loginPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string USERNAME = "";
        private string PASSWORD = "";

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        public MainWindow()
        {
            InitializeComponent();
            //enter key check
            this.KeyUp += new KeyEventHandler(loginButton_KeyUp);

            //default pointer click position to the username login
            usernameField.Focus();
            usernameField.Select(0, 0);
        }

        // this is for letting the enter key to be used to login
        void loginButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkLogin();
            }
        }

        // exit button will close the program
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //login button will call the function to check fields
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            checkLogin();
        }

        // used for checking the user entered fields, also will return a little information status on the top (changes colour of te)
        public void checkLogin()
        {
            //hardcoded username/password
            //probably want this blank when we're testing
            if ((usernameField.Text == USERNAME) && (passwordField.Password == PASSWORD))
            {
                statusText.Foreground = Brushes.Green;
                statusText.Text = "Logging in...";

                //opens the main booking window and closes the login screen 

                mainCalendarDisplayWindow cal = new mainCalendarDisplayWindow();
                cal.Show();
                this.Hide();
            }

            else
            {
                statusText.Foreground = Brushes.Red;
                statusText.Text = "Incorrect username/password";
                passwordField.Password = "";
            }
        }


    }
}
