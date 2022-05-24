
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

      public ObservableCollection<Term> Terms
      {
         get;
         set;
      }
      public TermsVM()
      {
         Terms = new ObservableCollection<Term>();
         AddTermCommand = new Command(AddTerm);

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
   }
}
