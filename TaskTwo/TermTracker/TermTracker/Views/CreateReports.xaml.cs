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
   public partial class CreateReports : ContentPage
   {
      public CreateReports()
      {
         InitializeComponent();
         BindingContext = new ReportsVM();
      }
   }
}