using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
