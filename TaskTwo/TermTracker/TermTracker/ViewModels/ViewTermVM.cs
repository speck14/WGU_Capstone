using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   public class ViewTermVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command DeleteTermCommand { get; set; }
      public Command EditTermCommand { get; set; }
      public Command AddCourseCommand { get; set; }

      private Term currentTerm;

      private string termTitle;
      public string TermTitle
      {
         get { return termTitle; }
      }
      private int termId;
      public int TermId
      {
         get {return termId; }
         set
         {
            termId = value;
            OnPropertyChanged("TermId");
         }
      }
      private string startDate;
      public string StartDate
      {
         get { return startDate; }
         set
         {
            startDate = value;
            OnPropertyChanged("StartDate");
         }
      }
      private string endDate;
      public string EndDate
      {
         get { return endDate; }
         set
         {
            endDate = value;
            OnPropertyChanged("EndDate");
         }
      }

      public ViewTermVM(Term currentTerm)
      {
         this.currentTerm = currentTerm;
         termId = currentTerm.TermId;
         termTitle = currentTerm.Title;
         startDate = currentTerm.StartDate;
         endDate = currentTerm.EndDate;

         DeleteTermCommand = new Command(DeleteTerm);
         EditTermCommand = new Command(EditTerm);
         AddCourseCommand = new Command(AddCourse, AllowAdd);

         MessagingCenter.Subscribe<CourseVM>(this, "DeleteCourse", (sender) =>
         {
            AddCourseCommand?.ChangeCanExecute();
         });
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }

      private async void DeleteTerm()
      {
         bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Confirm Deletion", $"Do you really want to delete {currentTerm.Title} and its courses and assessments?", "Yes", "No");
      
         if(confirmDelete)
         {
            int del_term = Term.DeleteTerm(currentTerm);
            if(del_term > 0)
            {
               await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               await Application.Current.MainPage.DisplayAlert("Error", "Unable to delete term", "Ok");
            }
         }
      }
      private void EditTerm()
      {
         MessagingCenter.Subscribe<EditTermVM>(this, "EditTerm", (sender) => {
            currentTerm = sender.currentTerm;
            termTitle = sender.TermTitle;
            startDate = sender.StartDate.ToString("MM-dd-yyyy");
            endDate = sender.EndDate.ToString("MM-dd-yyyy");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
         });
         Application.Current.MainPage.Navigation.PushAsync(new EditTerm(currentTerm));
      }
      private int NumCourses()
      {
         return  Course.GetCourses(termId).Count;
      }
      private bool AllowAdd()
      {
         if(NumCourses() >= 6)
         {
            return false;
         }
         else
         {
            return true;
         }
      }
      public void AddCourse()
      {
         MessagingCenter.Subscribe<AddCourseVM>(this, "AddCourse", (sender) =>
         {
            AddCourseCommand?.ChangeCanExecute();
         });
         Application.Current.MainPage.Navigation.PushAsync(new AddCourse(termId));
      }
      public static void OnAppearing(int termId, ListView courseView)
      {
         courseView.ItemsSource = Course.GetCourses(termId);
      }
   }
}
