using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Models;
using TermTracker.Views;
using System.ComponentModel;

namespace TermTracker.ViewModels
{
   class ReportsVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command GenerateReportsCommand { get; set; }
      public ObservableCollection<Assessment> Assessments
      {
         get;
         set;
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
            startDate = value;
            OnPropertyChanged("EndDate");
         }
      }
      public ReportsVM ()
      {
         Assessments = new ObservableCollection<Assessment>();
         GenerateReportsCommand = new Command(GenerateReport);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      private void GenerateReport()
      {
        // var reportResults = Reports.GenerateReports(startDate, endDate);
         Application.Current.MainPage.Navigation.PushAsync(new ReportResults(ObjectiveAssessment.GetObjAssessments(3)));
      }
   }
}
