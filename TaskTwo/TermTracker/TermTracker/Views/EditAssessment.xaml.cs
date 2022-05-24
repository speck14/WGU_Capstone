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
   public partial class EditAssessment : ContentPage
   {
      private int courseId;
      private string type;
      public EditAssessment(Assessment currentAssessment)
      {
         InitializeComponent();
         this.courseId = currentAssessment.CourseId;
         this.type = currentAssessment.Type;
         this.BindingContext = new EditAssessmentVM(currentAssessment);
      }
      private List<string> AvailableTypes()
      {
         var availableTypes = new List<string>();
         var assessments = Assessment.GetAssessments(courseId);
         if (assessments.Count == 2)
         {
            availableTypes.Add(type);
         }
         else
         {
            availableTypes.Add("Performance");
            availableTypes.Add("Objective");
         }
         return availableTypes;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         EditAssessmentVM.SetCourseId(courseId);
         TypePicker.ItemsSource = AvailableTypes();
         TypePicker.SelectedItem = type;
      }
   }
}