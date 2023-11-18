﻿using System;
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
        private AdminManagerInterface adminManager;
        private IFlightManager flightManager;
        public MainWindow()
        {
            InitializeComponent();
            loginManager = new LoginManager();
            adminManager = new AdminManager();
            flightManager = new FlightManager();
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
                showUsersData();
            }
            else if (role == "AirlineStaff")
            {
                gridMain.Visibility = Visibility.Visible;
                gridAdmin.Visibility = Visibility.Collapsed;
                gridAirlineStaff.Visibility = Visibility.Visible;
                gridCustomer.Visibility = Visibility.Collapsed;
                showFlightData();
            }
            else if (role == "Customer")
            {
                gridMain.Visibility = Visibility.Visible;
                gridAdmin.Visibility = Visibility.Collapsed;
                gridAirlineStaff.Visibility = Visibility.Collapsed;
                gridCustomer.Visibility = Visibility.Visible;
            }
        }

        private void showFlightData()
        {
            List<Flight> flights = new List<Flight>();
            flights = flightManager.getAllFlights();
            dgFlight.ItemsSource = flights;
        }

        private void showUsersData()
        {
            List<User> users = new List<User>();
            users = adminManager.getAllUsers();
            dataGridUsers.ItemsSource = users;
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

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Admin.FrmNewUser frmNewUser = new Admin.FrmNewUser();
            frmNewUser.ShowDialog();
            showUsersData();
        }

        private void dataGridUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User user = (User)dataGridUsers.SelectedItem;
            Admin.FrmNewUser frmNewUser = new Admin.FrmNewUser(user);
            frmNewUser.ShowDialog();
            showUsersData();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)dataGridUsers.SelectedItem;
            if (user == null)
            {
                lblAdminMessage.Content = "Please select a user!";
            }
            lblAdminMessage.Content = "";
            int result = adminManager.deleteUser(user);
            if (result == 0)
            {
                lblAdminMessage.Content = "user did not deleted!";
                return;
            }
            lblAdminMessage.Content = "";
            showUsersData();

        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            FrmFlights frmFlights = new FrmFlights();
            frmFlights.ShowDialog();
            showFlightData();
        }
    }
}
