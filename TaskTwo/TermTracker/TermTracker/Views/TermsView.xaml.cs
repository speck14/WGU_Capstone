using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using TermTracker.ViewModels;
using TermTracker.Models;

namespace TermTracker
{
   public partial class TermsView : ContentPage
   {
      TermsVM ViewModel;
      private bool displayedNotification = false;
      public TermsView()
      {
         InitializeComponent();

         ViewModel = Resources["vm"] as TermsVM;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();

         if (!displayedNotification)
         {
            Notify();
         }
         displayedNotification = true;

         ViewModel.GetTerms();
      }
      private void Notify()
      {
         string notificationText = "";
         notificationText += Assessment.CheckNotifications();
         notificationText += Course.CheckNotifications();

         if (!String.IsNullOrEmpty(notificationText))
         {
            Application.Current.MainPage.DisplayAlert("Upcoming:", notificationText, "OK");
         }
      }

      private void AllTerms_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (AllTerms_ListView.SelectedItem is Term currentTerm)
         {
            Navigation.PushAsync(new ViewTerm(currentTerm));
         }       
      }
   }
}