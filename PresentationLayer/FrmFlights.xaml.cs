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
using DataObjects;
using System.Windows.Media.Media3D;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for FrmFlights.xaml
    /// </summary>
    public partial class FrmFlights : Window
    {
        private IFlightManager flightManager;
        public FrmFlights()
        {
            InitializeComponent();
            flightManager = new FlightManager();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                return;
            }
            int result = 0;
            Flight flight = new Flight();
            flight.FlightNumber = txtFlightNumber.Text;
            flight.Departure = txtDeparture.Text;
            flight.Destination = txtDestination.Text;
            flight.Departure = txtDeparture.Text;
            flight.DepartureTime = Convert.ToDateTime(txtDepartureTime.Text);
            flight.ArrivalTime = Convert.ToDateTime(txtArrivalTime.Text);
            flight.AvailableSeats = Convert.ToInt32(txtAvailableSeats.Text);
            flight.Price = Convert.ToDecimal(txtPrice.Text);
            flight.Airline = txtAirline.Text;
            result = flightManager.addNewFlight(flight);
            if (result == 0)
            {
                lblFlightFormMessage.Content = "Flight did not added!";
                return;
            }
            lblFlightFormMessage.Content = "Flight Added ";
            clearForm();
        }

        private void clearForm()
        {
            txtFlightNumber.Text = "";
            txtDeparture.Text = "";
            txtDestination.Text = "";
            txtDepartureTime.Text = "";
            txtArrivalTime.Text = "";
            txtAvailableSeats.Text = "";
            txtPrice.Text = "";
            txtAirline.Text = "";
        }

        private bool validateForm()
        {
            if (txtFlightNumber.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Flight number require";
                return false;
            }
            if (txtDeparture.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Departure require";
                return false;
            }
            if (txtDestination.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Destination require";
                return false;
            }
            if (txtDepartureTime.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Departure Time require";
                return false;
            }
            try
            {
                Convert.ToDateTime(txtDepartureTime);
            }
            catch (Exception)
            {

                lblFlightFormMessage.Content = "Departure time is not in format";
            }
            if (txtArrivalTime.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Arrival Time require";
                return false;
            }
            try
            {
                Convert.ToDateTime(txtArrivalTime.Text);
            }
            catch (Exception)
            {

                lblFlightFormMessage.Content = "Arrival time is not in format";
            }
            if (txtAvailableSeats.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Available seats require";
                return false;
            }
            try
            {
                Convert.ToInt32(txtAvailableSeats.Text);
            }
            catch (Exception)
            {

                lblFlightFormMessage.Content = "Available seats must be a number";
            }
            if (txtPrice.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Price require";
                return false;
            }
            try
            {
                Convert.ToDecimal(txtPrice.Text);
            }
            catch (Exception)
            {

                lblFlightFormMessage.Content = "Price must be a Decimal number";
            }
            if (txtAirline.Text.Length == 0)
            {
                lblFlightFormMessage.Content = "Airline require";
                return false;
            }
            lblFlightFormMessage.Content = "";
            return true;
        }
    }
}
