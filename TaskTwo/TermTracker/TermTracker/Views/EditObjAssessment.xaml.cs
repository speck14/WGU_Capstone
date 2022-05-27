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
      public static bool scheduled_change = false;
      public EditObjAssessment(ObjectiveAssessment currentAssessment)
      {
         InitializeComponent();
         this.courseId = currentAssessment.CourseId;
         this.BindingContext = new EditAssessmentVM(currentAssessment);
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         EditAssessmentVM.SetCourseId(courseId);
      }

      private void DatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
      {
         scheduled_change = true;
      }

      private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
      {
         scheduled_change = true;
      }
   }
}