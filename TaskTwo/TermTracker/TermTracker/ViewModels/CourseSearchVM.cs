using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Models;
using TermTracker.Views;
using System.ComponentModel;

namespace TermTracker.ViewModels
{
   class CourseSearchVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command SearchCoursesCommand { get; set; }
      public ObservableCollection<Course> Courses
      {
         get;
         set;
      }
      private string searchTerms;
      public string SearchTerms
      {
         get { return searchTerms; }
         set
         {
            searchTerms = value;
            OnPropertyChanged("SearchTerms");
         }
      }
      public CourseSearchVM()
      {
         Courses = new ObservableCollection<Course>();
         SearchCoursesCommand = new Command(SearchCourses);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      private void SearchCourses()
      {
         var searchResults = Course.SearchCourses(searchTerms);
         Application.Current.MainPage.Navigation.PushAsync(new SearchResults(searchResults));
      }
      public void GetCourses(List<Course> courses)
      {
         Courses.Clear();
         foreach(var course in courses)
         {
            Courses.Add(course);
         }
      }
   }
}
