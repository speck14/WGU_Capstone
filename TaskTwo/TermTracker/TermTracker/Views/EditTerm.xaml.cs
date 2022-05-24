using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.ViewModels;
using TermTracker.Models;

namespace TermTracker.Views
{
   public partial class EditTerm : ContentPage
   {
      public EditTerm(Term currentTerm)
      {
         InitializeComponent();

         this.BindingContext = new EditTermVM(currentTerm);
      }
   }
}