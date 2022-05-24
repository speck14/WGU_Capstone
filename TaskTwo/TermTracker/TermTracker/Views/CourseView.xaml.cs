using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using TermTracker.Models;
using TermTracker.ViewModels;

namespace TermTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class CourseView : ContentPage
   {
      Course currentCourse;
      public CourseView(Course currentCourse)
      {
         InitializeComponent();

         this.currentCourse = currentCourse;
         BindingContext = new CourseVM(currentCourse);
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         recipientPhone.IsVisible = false;
      }
      private void Button_Clicked(object sender, EventArgs e)
      {
         recipientPhone.IsVisible = true;
      }
   }
}