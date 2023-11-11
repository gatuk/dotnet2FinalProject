using LogicLayerInterfaces;
using LogicLayer;
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
using static System.Net.Mime.MediaTypeNames;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeMangerInterface employeeManager = null;
        public MainWindow()
        {
            InitializeComponent();
            employeeManager = new EmployeeManger();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!validateUserName(txtUserName.Text))
            {
                return;
            }
            if (!validatePassword(txtPassword.Password))
            {
                return;
            }
            int id = 0;
            id = employeeManager.verifyUser(txtUserName.Text, txtPassword.Password);
            if (id == 0)
            {
                lblLoginMessage.Content = "user not verify";
                return;
            }
            lblLoginMessage.Content = "user is good it is Id is " + id;
        }

        private bool validatePassword(string password)
        {
            if (password.Length == 0)
            {
                lblLoginMessage.Content = "password is require";
                return false;
            }
            lblLoginMessage.Content = "";
            return true;
        }

        private bool validateUserName(string text)
        {
            if (text.Length == 0) {
                lblLoginMessage.Content = "user name is require";
                return false;
            }
            lblLoginMessage.Content = "";
            return true;
        }
    }
}
