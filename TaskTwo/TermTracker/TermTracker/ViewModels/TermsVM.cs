using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   public class TermsVM
   {
      public Command AddTermCommand { get; set; }
      public Command ViewTermCommand { get; set; }
      public Command SearchCourseCommand { get; set; }
      public Command ReportsCommand { get; set; }

      public ObservableCollection<Term> Terms
      {
         get;
         set;
      }
      public TermsVM()
      {
         Terms = new ObservableCollection<Term>();
         AddTermCommand = new Command(AddTerm);
         SearchCourseCommand = new Command(SearchCourseRedirect);
         ReportsCommand = new Command(ViewReportsRedirect);

         GetTerms();
      }
      public void GetTerms()
      {
         var terms = Term.GetTerms();

         Terms.Clear();

         foreach(var term in terms)
         {
            Terms.Add(term);
         }
         
      }
      public void AddTerm()
      {
         Application.Current.MainPage.Navigation.PushAsync(new AddTerm());
      }
      public void SearchCourseRedirect()
      {
         Application.Current.MainPage.Navigation.PushAsync(new CourseSearch());
      }
      public void ViewReportsRedirect()
      {
         Application.Current.MainPage.Navigation.PushAsync(new CreateReports());
      }
   }
}
