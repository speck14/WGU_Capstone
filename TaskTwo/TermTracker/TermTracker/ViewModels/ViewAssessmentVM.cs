using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using TermTracker.Models;
using TermTracker.Views;
using System.Runtime.InteropServices.ComTypes;

namespace TermTracker.ViewModels
{
   class ViewAssessmentVM : INotifyPropertyChanged
   { 
      public event PropertyChangedEventHandler PropertyChanged;
      public Command EditAssessmentCommand { get; set; }

      public ObjectiveAssessment currentObjAssessment;
      public PerformanceAssessment currentPerfAssessment;

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
            OnPropertyChanged("Type");
         }
      }
      private string preAssessmentScore;
      public string PreAssessmentScore
      {
         get { return preAssessmentScore; }
         set
         {
            preAssessmentScore = value;
            OnPropertyChanged("PreAssessmentScore");
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
      private string scheduledDate;
      public string ScheduledDate
      {
         get { return scheduledDate; }
         set
         {
            scheduledDate = value;
            OnPropertyChanged("ScheduledDate");
         }
      }
      private string scheduledTime;
      public string ScheduledTime
      {
         get { return scheduledTime; }
         set
         {
            scheduledDate = value;
            OnPropertyChanged("ScheduledTime");
         }
      }
      private bool scheduledDateNotification;
      public bool ScheduledDateNotification
      {
         get { return scheduledDateNotification; }
         set
         {
            dueDateNotification = value;
            OnPropertyChanged("ScheduledDateNotificaton");
         }
      }
      public ViewAssessmentVM(ObjectiveAssessment currentAssessment)
      {
         this.currentObjAssessment = currentAssessment;
         this.assessmentId = currentAssessment.AssessmentId;
         this.name = currentAssessment.Name;
         this.type = currentAssessment.Type;
         this.dueDate = currentAssessment.DueDate;
         this.dueDateNotification = currentAssessment.DueDateNotification;
         this.preAssessmentScore = currentAssessment.PreAssessmentScore;
         this.scheduledDate = currentAssessment.ScheduledDate;
         this.scheduledTime = currentAssessment.ScheduledTime;
         this.scheduledDateNotification = currentAssessment.ScheduledDateNotification;

         EditAssessmentCommand = new Command(EditAssessment);
      }
      public ViewAssessmentVM(PerformanceAssessment currentAssessment)
      {
         currentPerfAssessment = currentAssessment;
         this.assessmentId = currentAssessment.AssessmentId;
         this.name = currentAssessment.Name;
         this.type = currentAssessment.Type;
         this.dueDate = currentAssessment.DueDate;
         this.dueDateNotification = currentAssessment.DueDateNotification;

         EditAssessmentCommand = new Command(EditAssessment);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      public void EditAssessment()
      {
         if(currentObjAssessment != null && currentObjAssessment.Type == "Objective")
         {
            MessagingCenter.Subscribe<EditAssessmentVM>(this, "EditAssessment", (sender) =>
            {
               currentObjAssessment = sender.currentObjAssessment;
               name = sender.Name;
               type = sender.Type;
               dueDate = sender.DueDate.ToString("MM-dd-yyyy");
               dueDateNotification = sender.DueDateNotification;
               preAssessmentScore = sender.PreAssessmentScore;
               scheduledDate = sender.ScheduledDate.ToString("MM-dd-yyyy");
               scheduledTime = sender.ScheduledTime;
               scheduledDateNotification = sender.ScheduledDateNotification;
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            });
            Application.Current.MainPage.Navigation.PushAsync(new EditObjAssessment(currentObjAssessment));
         }
         else if(currentPerfAssessment != null && currentPerfAssessment.Type == "Performance")
         {
            MessagingCenter.Subscribe<EditAssessmentVM>(this, "EditAssessment", (sender) =>
            {
               currentPerfAssessment = sender.currentPerfAssessment;
               name = sender.Name;
               type = sender.Type;
               dueDate = sender.DueDate.ToString("MM-dd-yyyy");
               dueDateNotification = sender.DueDateNotification;
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            });
            Application.Current.MainPage.Navigation.PushAsync(new EditAssessment(currentPerfAssessment));
         }

      }
   }
}
