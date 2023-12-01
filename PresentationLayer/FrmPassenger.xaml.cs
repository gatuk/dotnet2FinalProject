using DataObjects;
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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for FrmPassenger.xaml
    /// </summary>
    public partial class FrmPassenger : Window
    {
        private IPassengerManager passengerManager;
        private Passenger passenger;
        private IFlightManager flightManger;
        private Flight flight;

        public FrmPassenger()
        {
            InitializeComponent();
            passengerManager = new PassengerManager();
            passenger = new Passenger();
            flightManger = new FlightManager();
            flight = new Flight();
            fillOutCombos();
        }

        private void fillOutCombos()
        {
           List<Flight> flights = new List<Flight>();
            flights = flightManger.getAllFlights();
            List<int> flightsIDs = new List<int>();
            foreach (Flight flight in flights) {
                flightsIDs.Add(flight.FlightId);
            }
            comboFlightID.ItemsSource = flightsIDs;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validateFormInput())
            {
                return;
            }
            int result = 0;
            passenger.FlightID = Convert.ToInt32(comboFlightID.SelectedItem);
            passenger.FirstName = txtFirstname.Text;
            passenger.LastName = txtLastname.Text;
            passenger.SeatNumber = txtSeatNumber.Text;
            passenger.Email = txtEmail.Text;
            passenger.PhoneNumber = txtPhoneNumber.Text;
            passenger.Address = txtAddress.Text;
            passenger.City = txtCity.Text;
            passenger.State = txtState.Text;
            passenger.ZipCode = Convert.ToInt32(txtZipcode.Text);
            passenger.IsCheckedIn = cbIsCheckIN.IsChecked == true;
            passenger.IsMinor = cbIsMinor.IsChecked == true;
            passenger.IsSpecialNeeds = cbSpecialNeed.IsChecked == true;
            passenger.Active = cbActive.IsChecked == true;
            result = passengerManager.addPassenger(passenger);

            if (result == 0)
            {
                lblFormMessage.Content = "Passenger did not added correctly";
                return;
            }
            lblFormMessage.Content = "Passenger added correctly";


        }

        private bool validateFormInput()
        {
            if (txtFirstname.Text.Length == 0)
            {
                lblFormMessage.Content = "first name require";
                return false;
            }
            if (txtLastname.Text.Length == 0)
            {
                lblFormMessage.Content = "Last name require";
                return false;
            }
            if (txtEmail.Text.Length == 0)
            {
                lblFormMessage.Content = "Email require";
                return false;
            }
            if (txtPhoneNumber.Text.Length == 0)
            {
                lblFormMessage.Content = "Phone number require";
                return false;
            }
            if (txtAddress.Text.Length == 0)
            {
                lblFormMessage.Content = "Address require";
                return false;
            }
            if (txtCity.Text.Length == 0)
            {
                lblFormMessage.Content = "City require";
                return false;
            }
            if (txtState.Text.Length == 0)
            {
                lblFormMessage.Content = "State require";
                return false;
            }
            if (txtZipcode.Text.Length == 0)
            {
                lblFormMessage.Content = "Zipcode require";
                return false;
            }
            try
            {
                Convert.ToInt32(txtZipcode.Text);
            }
            catch (Exception)
            {

                lblFormMessage.Content = "Zipcode must be a number";
                return false;
            }
            if (comboFlightID.SelectedItem == null)
            {
                lblFormMessage.Content = "Flight ID require";
                return false;
            }
            try
            {
                Convert.ToInt32(comboFlightID.SelectedItem);
            }
            catch (Exception)
            {

                lblFormMessage.Content = "Flight ID must be a number";
                return false;
            }
            if (txtSeatNumber.Text.Length == 0)
            {
                lblFormMessage.Content = "Seat Number require";
                return false;
            }
            lblFormMessage.Content = "";
            return true;
        }
    }
}
