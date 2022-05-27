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


namespace TermTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ObjectiveAssessmentView : ContentPage
   {
      public ObjectiveAssessmentView(ObjectiveAssessment currentAssessment)
      {
         InitializeComponent();
         BindingContext = new ViewAssessmentVM(currentAssessment);
      }
   }
}