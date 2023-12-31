﻿using LogicLayer;
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

        private Flight Flight = null;
        private string newOrEdit = "";

        public FrmFlights()
        {
            InitializeComponent();
            flightManager = new FlightManager();
            Flight = new Flight();
            fillCombos();
            newOrEdit = "New";
        }

        public FrmFlights(Flight flight)
        {
            InitializeComponent();
            flightManager = new FlightManager();
            Flight = flight;
            fillCombos();
            fillForm();
            newOrEdit = "Edit";
        }

        private void fillForm()
        {
            txtFlightNumber.Text = Flight.FlightNumber;
            comboDeparture.SelectedItem = Flight.Departure;
            comboDestination.SelectedItem = Flight.Destination;
            txtDepartureTime.Text = Flight.DepartureTime.ToString();
            txtArrivalTime.Text = Flight.ArrivalTime.ToString();
            txtAvailableSeats.Text = Flight.AvailableSeats.ToString();
            txtPrice.Text = Flight.Price.ToString();
            txtAirline.Text = Flight.Airline;
        }

        private void fillCombos()
        {
            List<string> airportCodes = new List<string>();
            airportCodes = flightManager.getAllAirPortCodes();
            comboDestination.ItemsSource = airportCodes;
            comboDeparture.ItemsSource = airportCodes;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                return;
            }
            int result = 0;
           
            Flight.FlightNumber =txtFlightNumber.Text;
            Flight.Departure = comboDeparture.SelectedItem.ToString();
            Flight.Destination = comboDestination.SelectedItem.ToString();
            Flight.DepartureTime = Convert.ToDateTime(txtDepartureTime.Text);
            Flight.ArrivalTime = Convert.ToDateTime(txtArrivalTime.Text);
            Flight.AvailableSeats = Convert.ToInt32(txtAvailableSeats.Text);
            Flight.Price = Convert.ToDecimal(txtPrice.Text);
            Flight.Airline = txtAirline.Text;
            if (newOrEdit == "New")
            {
                result = flightManager.addNewFlight(Flight);
            }
            else
            {
                result = flightManager.editFlight(Flight);
            }
            
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
            comboDeparture.SelectedIndex = 0;
            comboDestination.SelectedIndex = 0;
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
            
            if (comboDeparture.SelectedItem == null)
            {
                lblFlightFormMessage.Content = "Departure require";
                return false;
            }
            if (comboDestination.SelectedItem == null)
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
