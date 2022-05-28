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
      public ReportResults(List<ObjectiveAssessment> ReportResults)
      {
         InitializeComponent();
         assessments = ReportResults;
         ViewModel = Resources["vm"] as ReportsVM;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         if (assessments != null && assessments.Any())
         {
            //ViewModel.GetCourses(courses);
         }
         // If there are no search results
         else
         {
            var results_stack = this.FindByName<StackLayout>("report_stack");
            results_stack.Children.Add(new Label { Text = "Report generated 0 records", FontSize = 24 });
            var results_list = this.FindByName<ListView>("Assessments_ListView");
            results_list.IsVisible = false;
         }
      }
   }
}