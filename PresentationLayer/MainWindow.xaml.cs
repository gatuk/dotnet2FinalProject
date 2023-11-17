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
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginManagerInterface loginManager;
        public MainWindow()
        {
            InitializeComponent();
            loginManager = new LoginManager();
            gridMain.Visibility = Visibility.Collapsed;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!isDataValid())
            {
                return;
            }
            string role = "";
            role = loginManager.verifyUser(txtUserName.Text, txtPassword.Password);
            lblLoginMessage.Content = role;
            displayGridByRole(role);
        }

        private void displayGridByRole(string role)
        {
            if (role == "not verify")
            {
                lblLoginMessage.Content = "this user is not verify";
                gridMain.Visibility = Visibility.Collapsed;
            }
            else if (role == "Admin")
            {
                gridMain.Visibility = Visibility.Visible;
                gridAdmin.Visibility = Visibility.Visible;
                gridAirlineStaff.Visibility = Visibility.Collapsed;
                gridCustomer.Visibility = Visibility.Collapsed;
            }
            else if (role == "AirlineStaff")
            {
                gridMain.Visibility = Visibility.Visible;
                gridAdmin.Visibility = Visibility.Collapsed;
                gridAirlineStaff.Visibility = Visibility.Visible;
                gridCustomer.Visibility = Visibility.Collapsed;
            }
            else if (role == "Customer")
            {
                gridMain.Visibility = Visibility.Visible;
                gridAdmin.Visibility = Visibility.Collapsed;
                gridAirlineStaff.Visibility = Visibility.Collapsed;
                gridCustomer.Visibility = Visibility.Visible;
            }
        }

        //below method make sure the input of login is correct
        private bool isDataValid()
        {
            if (txtUserName.Text.Length == 0)
            {
                lblLoginMessage.Content = "username is require";
                return false;
            }
            if (txtPassword.Password.Length == 0)
            {
                lblLoginMessage.Content = "password require";
                return false;
            }
            lblLoginMessage.Content = "";
            return true;
        }
    }
}
