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
   public partial class AddCourse : ContentPage
   {
      private int termId;
      public AddCourse(int termId)
      {
         InitializeComponent();
         this.termId = termId;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         AddCourseVM.SetTermId(termId);
      }
   }
}