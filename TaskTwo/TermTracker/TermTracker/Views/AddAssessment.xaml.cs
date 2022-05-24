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
   public partial class AddAssessment : ContentPage
   {
      private int courseId;
      public AddAssessment(int courseId)
      {
         InitializeComponent();
         this.courseId = courseId;
         BindingContext = new AddAssessmentVM();
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         AddAssessmentVM.SetCourseId(courseId);
         TypePicker.ItemsSource = Assessment.GetTypes(courseId);
      }
   }
}