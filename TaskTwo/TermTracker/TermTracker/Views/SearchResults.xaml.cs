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
   public partial class SearchResults : ContentPage
   {
      CourseSearchVM ViewModel;
      List<Course> courses;
      public SearchResults(List<Course> SearchResults)
      {
         InitializeComponent();
         courses = SearchResults;
         ViewModel = Resources["vm"] as CourseSearchVM;
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
         if(courses != null && courses.Any())
         {
            ViewModel.GetCourses(courses);
         }
         // If there are no search results
         else
         {
            var results_stack = this.FindByName<StackLayout>("search_stack");
            results_stack.Children.Add(new Label { Text = "No courses that matched your search were found.", FontSize = 24 });
            var results_list = this.FindByName<ListView>("Courses_ListView");
            results_list.IsVisible = false;
         }
      }

      private void Courses_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (Courses_ListView.SelectedItem is Course currentCourse)
         {
            Navigation.PushAsync(new CourseView(currentCourse));
         }
      }
   }
}