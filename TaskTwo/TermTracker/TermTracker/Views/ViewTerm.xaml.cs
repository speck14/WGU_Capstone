using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Models;
using TermTracker.ViewModels;
using TermTracker.Views;

namespace TermTracker
{
   public partial class ViewTerm : ContentPage
   {
      Term currentTerm;
     
      public ViewTerm(Term currentTerm)
      {
         InitializeComponent();

         this.currentTerm = currentTerm;
         BindingContext = new ViewTermVM(currentTerm);
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         ViewTermVM.OnAppearing(currentTerm.TermId, TermCourses_ListView);
         
      }

      private void TermCourses_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (TermCourses_ListView.SelectedItem is Course currentCourse)
         {
            Navigation.PushAsync(new CourseView(currentCourse));
         }
      }
   }
}