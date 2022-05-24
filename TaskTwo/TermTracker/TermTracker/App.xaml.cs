using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/* NOTES TO REVIEWER:
      - Mobile application, ran in Pixel 2 Pie 9.0- API 28 (Android 9.0)
      - Encapsulation: occurs in classes containg 'private' variables and methods, in various places throughout the program
      - Inheritance: ObjectiveAssessment and PerformanceAssessment are both derived classes of the Assessment parent class
      - Polymorphism: occurs in: TermsView, AddAssessment view, AddCourse view, CourseView, EditAssessment, ViewAssessments, and ViewwTerm, when the OnAppearing() method is overridden to redefine the functionality of the parent class 
         - in the case of the above classes, the parent is the ContentPage class
 */

namespace TermTracker
{
   public partial class App : Application
   {
      public static string FilePath;
      public App()
      {
         InitializeComponent();

         MainPage = new NavigationPage(new TermsView());
      }
      public App(string filePath)
      {
         InitializeComponent();

         FilePath = filePath;

         MainPage = new NavigationPage(new TermsView());

      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}
