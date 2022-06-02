using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/* NOTES TO REVIEWER:
      - Mobile application, ran in Pixel 2 Pie 9.0- API 28 (Android 9.0)
      - Encapsulation: occurs in classes containg 'private' variables and methods, in various places throughout the program
      - Inheritance: ObjectiveAssessment and PerformanceAssessment are both derived classes of the Assessment parent class
      - Polymorphism: occurs in: TermsView, AddAssessment view, AddCourse view, CourseView, EditAssessment, ViewAssessments, and ViewwTerm, when the OnAppearing() method is overridden to redefine the functionality of the parent class 
         - in the case of the above classes, the parent is the ContentPage class
         - There is also Polymorphism present in the implementation of the ObjectiveAssessment and PerformanceAssessment derived classes
      - Search functionality: search for courses by name
      - Reports: User can generate reports that consist of objective assessments scheduled within a selected time period.
         - User can name their report
         - Reports are timestamped with date/time that it was generated.
      - Security: per industry best practices, app permissions set within the Android manifest are limited to the fewest permissions needed for the app to function
         - The app does not have access to device media, photos, contacts, location, or files.
         - There is logic present in the Models classes to prevent against SQL injection into the SQLite database
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
