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
            endDate = value;
            OnPropertyChanged("EndDate");
         }
      }
      private string reportDate;
      public string ReportDate
      {
         get { return reportDate; }
         set
         {
            reportDate = value;
            OnPropertyChanged("ReportDate");
         }
      }
      public ReportsVM ()
      {
         startDate = DateTime.Now.Date;
         endDate = DateTime.Now.Date;
         reportDate = DateTime.Now.ToString();
         Assessments = new ObservableCollection<Assessment>();
         GenerateReportsCommand = new Command(GenerateReport);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      private void GenerateReport()
      {
         var reportResults = Reports.GenerateReports(startDate, endDate);
         Application.Current.MainPage.Navigation.PushAsync(new ReportResults(reportResults, ReportDate));
      }
      public void GetAssessments(List<ObjectiveAssessment> assessments)
      {
         Assessments.Clear();
         foreach (var assessment in assessments)
         {
            Assessments.Add(assessment);
         }
      }
   }
}
