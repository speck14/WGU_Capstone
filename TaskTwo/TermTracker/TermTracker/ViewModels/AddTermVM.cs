using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SQLite;
using Xamarin.Forms;
using TermTracker.Models;

namespace TermTracker.ViewModels
{
   public class AddTermVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      public Command SaveNewTermCommand { get; set; }

      private string termTitle;
      public string TermTitle
      {
         get { return termTitle; }
         set
         {
            termTitle = value;
            OnPropertyChanged("TermName");
         }
      }

      private DateTime startDate;
      public string StartDate
      {
         get{ return startDate.ToString("MM-dd-yyyy"); }
         set
         {
            startDate = DateTime.Parse(value);
            OnPropertyChanged("StartDate");
         }
      }
      private DateTime endDate;
      public string EndDate
      {
         get { return endDate.ToString("MM-dd-yyyy"); }
         set
         {
            endDate = DateTime.Parse(value);
            OnPropertyChanged("EndDate");
         }
      }
      public AddTermVM()
      {
         SaveNewTermCommand = new Command(Save);
         startDate = DateTime.Now.Date;
         endDate = DateTime.Now.Date.AddDays(30);
      }
      private void OnPropertyChanged(string property)
      {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      public void Save()
      {
         if (String.IsNullOrEmpty(TermTitle))
         {
            Application.Current.MainPage.DisplayAlert("Error", "Term Title cannot be empty", "Ok");
         }
         else if (startDate > endDate)
         {
            Application.Current.MainPage.DisplayAlert("Error", "Term end date cannot be before start date", "Ok");
         }
         else
         {
            Term term = new Term()
            {
               Title = termTitle,
               StartDate = startDate.ToString("MM-dd-yyyy"),
               EndDate = endDate.ToString("MM-dd-yyyy")
            };
            int response = Term.AddTerm(term);
            if (response > 0)
            {
               Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               Application.Current.MainPage.DisplayAlert("Error", "Unable to add term", "Ok");
            }
         }
      }
   }
}
