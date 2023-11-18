using LogicLayer;
using LogicLayerInterfaces;
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
using DataObjects;

namespace PresentationLayer.Admin
{
    /// <summary>
    /// Interaction logic for FrmNewUser.xaml
    /// </summary>
    public partial class FrmNewUser : Window
    {
        private AdminManagerInterface adminManager;
        private User user;
        public FrmNewUser()
        {
            adminManager = new AdminManager();
            user = new User();
            InitializeComponent();
            fillRoleComboBox();
        }

        private void fillRoleComboBox()
        {
            List<string> roles = new List<string>();
            roles = adminManager.getRoles();
            combRole.ItemsSource = roles;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validateData())
            {
                return;
            }
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.Role = combRole.SelectedItem.ToString();
            int result = adminManager.addUser(user);
            if (result == 0)
            {
                lblFormMessage.Content = "user did not added!";
                return;
            }
            lblFormMessage.Content = "user added";
        }

        private bool validateData()
        {
            if (txtUserName.Text.Length == 0)
            {
                lblFormMessage.Content = "user name is require";
                return false;
            }
            if (txtPassword.Text.Length == 0)
            {
                lblFormMessage.Content = "password is require";
                return false;
            }
            if (combRole.SelectedItem == null)
            {
                lblFormMessage.Content = "Role is require";
                return false;
            }
            lblFormMessage.Content = "";
            return true;
        }
    }
}
