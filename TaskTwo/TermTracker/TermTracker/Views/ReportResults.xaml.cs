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
      string reportTitle;
      public ReportResults(List<ObjectiveAssessment> ReportResults, string reportDate, string reportTitle)
      {
         InitializeComponent();
         assessments = ReportResults;
         ViewModel = Resources["vm"] as ReportsVM;
         this.reportDate = reportDate;
         this.reportTitle = reportTitle;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         var results_stack = this.FindByName<StackLayout>("report_stack");

         var time_label = this.FindByName<Label>("report_datetime");
         time_label.Text = reportDate;

         var title_label = this.FindByName<Label>("report_title");
         if(!String.IsNullOrEmpty(reportTitle))
         {
            title_label.Text = reportTitle;
         }        

         if (assessments != null && assessments.Any())
         {
            ViewModel.GetAssessments(assessments);
         }
         // If there are no search results
         else
         {
            var reports_label = new Label();
            reports_label.Text = reportTitle;
            results_stack.Children.Add(reports_label);
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