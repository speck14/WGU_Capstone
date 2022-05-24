using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using TermTracker.Models;

namespace TermTracker.ViewModels 
{
   public class EditTermVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command UpdateCommand { get; set; }

      public Term currentTerm;
      private int termId;

      private string termTitle;
      public string TermTitle
      {
         get { return termTitle; }
         set
         {
            termTitle = value;
            OnPropertyChanged("TermTitle");
         }
      }
      private DateTime startDate;
      public DateTime StartDate
      {
         get { return startDate; }
         set
         {
            startDate = value;
            OnPropertyChanged("StartDate");
         }
      }
      private DateTime endDate;
      public DateTime EndDate
      {
         get { return endDate; }
         set
         {
            endDate = value;
            OnPropertyChanged("EndDate");
         }
      }
      public EditTermVM(Term currentTerm)
      {
         this.currentTerm = currentTerm;
         termId = currentTerm.TermId;
         termTitle = currentTerm.Title;
         startDate = DateTime.Parse(currentTerm.StartDate);
         endDate = DateTime.Parse(currentTerm.EndDate);

         UpdateCommand = new Command(Update);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      public void Update()
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
               TermId = termId,
               Title = termTitle,
               StartDate = startDate.Date.ToString("MM-dd-yyyy"),
               EndDate = endDate.Date.ToString("MM-dd-yyyy")
            };
            int updated = Term.UpdateTerm(term);
            if (updated > 0)
            {
               currentTerm = term;
               MessagingCenter.Send(this, "EditTerm");
               Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               Application.Current.MainPage.DisplayAlert("Error", "Unable to update term", "Ok");
            }
         }
      }

   }
}
