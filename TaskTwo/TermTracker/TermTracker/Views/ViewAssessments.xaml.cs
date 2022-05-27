using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Models;
using TermTracker.ViewModels;

namespace TermTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ViewAssessments : ContentPage
   {
      private Course currentCourse;
      public ViewAssessments(Course currentCourse)
      {
         InitializeComponent();
         this.currentCourse = currentCourse;
         BindingContext = new AssessmentsVM(currentCourse);
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         AssessmentsVM.OnAppearing(currentCourse.CourseId, CourseAssessments_ObjListView, CourseAssessments_PerfListView);
      }

      private void CourseAssessments_PerfListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (CourseAssessments_PerfListView.SelectedItem is PerformanceAssessment currentAssessment)
         {
            Navigation.PushAsync(new AssessmentView(currentAssessment));
         }
      }
      private void CourseAssessments_ObjListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (CourseAssessments_ObjListView.SelectedItem is ObjectiveAssessment currentAssessment)
         {
            Navigation.PushAsync(new ObjectiveAssessmentView(currentAssessment));
         }
      }
   }
}