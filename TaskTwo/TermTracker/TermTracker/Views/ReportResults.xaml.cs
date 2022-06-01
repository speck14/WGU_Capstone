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

namespace TermTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ReportResults : ContentPage
   {
      ReportsVM ViewModel;
      List<ObjectiveAssessment> assessments;
      string reportDate;
      public ReportResults(List<ObjectiveAssessment> ReportResults, string reportDate)
      {
         InitializeComponent();
         assessments = ReportResults;
         ViewModel = Resources["vm"] as ReportsVM;
         this.reportDate = reportDate;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         var results_stack = this.FindByName<StackLayout>("report_stack");
         var time_label = this.FindByName<Label>("report_datetime");
         time_label.Text = reportDate;
         if (assessments != null && assessments.Any())
         {
            ViewModel.GetAssessments(assessments);
         }
         // If there are no search results
         else
         {
            results_stack.Children.Add(new Label { Text = "Report generated 0 records", FontSize = 24 });
            results_stack.Children.Add(new Label { Text = "Report created: " });
            var timeLabel = new Label();
            timeLabel.Text = reportDate;
            results_stack.Children.Add(timeLabel);
            var results_list = this.FindByName<ListView>("Assessments_ListView");
            results_list.IsVisible = false;
         }
      }
   }
}