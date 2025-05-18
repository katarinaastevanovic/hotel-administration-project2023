﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HotelReservations.Model;
using HotelReservations.Service;

namespace HotelReservations.Windows
{
    public partial class Reservations : Window
    {
        private ReservationService reservationService;
        private ICollectionView view;

        public Reservations()
        {
            InitializeComponent();
            reservationService = new ReservationService();

            // Onemogući automatsko generisanje kolona
            ReservationsDG.AutoGenerateColumns = false;

            FillData();
        }

        public void FillData()
        {
            var reservations = reservationService.GetAllActiveReservations();

            view = CollectionViewSource.GetDefaultView(reservations);
            view.Filter = DoFilter;

            ReservationsDG.ItemsSource = null;
            ReservationsDG.ItemsSource = view;
            ReservationsDG.IsSynchronizedWithCurrentItem = true;
        }

      

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addReservationWindow = new AddEditReservation();

            Hide();
            if (addReservationWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = (Reservation)view.CurrentItem;

            if (selectedReservation != null)
            {
                var editReservationWindow = new AddEditReservation(selectedReservation);

                Hide();

                if (editReservationWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
        }

        private void ReservationSearchTB_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            view.Refresh();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedReservation = (Reservation)view.CurrentItem;

            if (selectedReservation != null)
            {
                MessageBoxResult res = MessageBox.Show($"Are you sure that you want to delete reservation {selectedReservation!.Id}?",
                "Confirmation", MessageBoxButton.YesNo);

                if(res == MessageBoxResult.Yes)
                {
                    selectedReservation.IsActive = false;
                    reservationService.SaveReservation(selectedReservation);
                    FillData();
                }
            }
            
            
        }

        private bool DoFilter(object reservationObject)
        {
            var reservation = reservationObject as Reservation;

            string idString = reservation.Id.ToString();

            var iDReservationSearchParam = ReservationSearchTB.Text;

            if (idString.Contains(iDReservationSearchParam))
            {
                return true;
            }
            return false;
        }
    }
}

