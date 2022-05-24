using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   class ViewAssessmentVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command DeleteAssessmentCommand { get; set; }
      public Command EditAssessmentCommand { get; set; }

      public Assessment currentAssessment;

      private int assessmentId;
      public int AssessmentId
      {
         get { return assessmentId; }
         set
         {
            assessmentId = value;
            OnPropertyChanged("assessmentId");
         }
      }
      private string name;
      public string Name
      {
         get { return name; }
         set
         {
            name = value;
            OnPropertyChanged("Name");
         }
      }
      private string type;
      public string Type
      {
         get { return type; }
         set
         {
            name = value;
            OnPropertyChanged("Name");
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
            startDate = value;
            OnPropertyChanged("EndDate");
         }
      }
      private string dueDate;
      public string DueDate
      {
         get { return dueDate; }
         set
         {
            dueDate = value;
            OnPropertyChanged("DueDate");
         }
      }
      private bool dueDateNotification;
      public bool DueDateNotification
      {
         get { return dueDateNotification; }
         set
         {
            dueDateNotification = value;
            OnPropertyChanged("DueDateNotificaton");
         }
      }
      public ViewAssessmentVM(Assessment currentAssessment)
      {
         this.currentAssessment = currentAssessment;
         this.assessmentId = currentAssessment.AssessmentId;
         this.name = currentAssessment.Name;
         this.type = currentAssessment.Type;
         this.startDate = currentAssessment.StartDate;
         this.endDate = currentAssessment.EndDate;
         this.dueDate = currentAssessment.DueDate;
         this.dueDateNotification = currentAssessment.DueDateNotification;

         DeleteAssessmentCommand = new Command(DeleteAssessment);
         EditAssessmentCommand = new Command(EditAssessment);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      public void EditAssessment()
      {
         MessagingCenter.Subscribe<EditAssessmentVM>(this, "EditAssessment", (sender) =>
         {
            currentAssessment = sender.currentAssessment;
            name = sender.Name;
            type = sender.Type;
            startDate = sender.StartDate.ToString("MM-dd-yyyy");
            endDate = sender.EndDate.ToString("MM-dd-yyyy");
            dueDate = sender.DueDate.ToString("MM-dd-yyyy");
            dueDateNotification = sender.DueDateNotification;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
         });
         Application.Current.MainPage.Navigation.PushAsync(new EditAssessment(currentAssessment));
      }
      public async void DeleteAssessment()
      {
         bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Confirm Deletion", $"Do you really want to delete {currentAssessment.Name}?", "Yes", "No");
         if (confirmDelete)
         {
            int del_assessment = Assessment.DeleteAssessment(currentAssessment);
            if (del_assessment > 0)
            {
               MessagingCenter.Send(this, "DeleteAssessment");
               await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               await Application.Current.MainPage.DisplayAlert("Error", "Unable to delete assessment", "Ok");
            }
         }
      }
   }
}
