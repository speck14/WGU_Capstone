using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.ViewModels;
using TermTracker.Models;

namespace TermTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class EditObjAssessment : ContentPage
   {
      private int courseId;
      ObjectiveAssessment currentAssessment;
      public EditObjAssessment(ObjectiveAssessment currentAssessment)
      {
         InitializeComponent();
         this.courseId = currentAssessment.CourseId;
         this.BindingContext = new EditAssessmentVM(currentAssessment);
         this.currentAssessment = currentAssessment;
         CheckScheduled();
      }
      private void CheckScheduled()
      {
         // If the exam hasn't been scheduled yet
         if (currentAssessment.ScheduledTime == "00:00" && currentAssessment.ScheduledDate == currentAssessment.DueDate)
         {
            var scheduled_stack = this.FindByName<StackLayout>("schedule_stack");
            scheduled_stack.Children.Add(new Label { Text = "Exam not yet scheduled", TextColor = new Color(255, 0, 0) });
         }
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         EditAssessmentVM.SetCourseId(courseId);
      }
   }
}