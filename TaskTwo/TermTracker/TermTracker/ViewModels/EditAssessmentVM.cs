using TermTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   class EditAssessmentVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command UpdateAssessmentCommand { get; set; }

      public PerformanceAssessment currentPerfAssessment;
      public ObjectiveAssessment currentObjAssessment;
      private int assessmentId;

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
            type = value;
            OnPropertyChanged("Type");
         }
      }
      private DateTime dueDate;
      public DateTime DueDate
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
            OnPropertyChanged("DueDateNotification");
         }
      }
      private static int courseId;
      public int CourseId
      {
         get { return courseId; }
         set
         {
            courseId = value;
            OnPropertyChanged("TermId");
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
            scheduledTime = value;
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
      public EditAssessmentVM(PerformanceAssessment currentAssessment)
      {
         this.currentPerfAssessment = currentAssessment;
         courseId = currentAssessment.CourseId;
         assessmentId = currentAssessment.AssessmentId;
         name = currentAssessment.Name;
         type = currentAssessment.Type;
         dueDate = DateTime.Parse(currentAssessment.DueDate);
         dueDateNotification = currentAssessment.DueDateNotification;

         UpdateAssessmentCommand = new Command(Update);
      }

      public EditAssessmentVM(ObjectiveAssessment currentAssessment)
      {
         currentObjAssessment = currentAssessment;
         courseId = currentAssessment.CourseId;
         assessmentId = currentAssessment.AssessmentId;
         name = currentAssessment.Name;
         type = currentAssessment.Type;
         dueDate = DateTime.Parse(currentAssessment.DueDate);
         dueDateNotification = currentAssessment.DueDateNotification;
         preAssessmentScore = currentAssessment.PreAssessmentScore;
         scheduledDate = currentAssessment.ScheduledDate;
         scheduledTime = currentAssessment.ScheduledTime;
         scheduledDateNotification = currentAssessment.ScheduledDateNotification;

         UpdateAssessmentCommand = new Command(Update);
      }

      public static void SetCourseId(int id)
      {
         courseId = id;
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      public void Update()
      {
         if(currentPerfAssessment != null)
         {
            if (string.IsNullOrEmpty(name))
            {
               Application.Current.MainPage.DisplayAlert("Error", "No fields can be empty.", "Ok");
            }
            else
            {
               PerformanceAssessment assessment = new PerformanceAssessment()
               {
                  AssessmentId = assessmentId,
                  Name = name,
                  Type = type,
                  DueDate = dueDate.ToString("MM-dd-yyyy"),
                  DueDateNotification = dueDateNotification,
                  CourseId = courseId
               };
               currentPerfAssessment = assessment;
               int updated = Assessment.UpdateAssessment(assessment);
               if (updated > 0)
               {
                  MessagingCenter.Send(this, "EditAssessment");
                  Application.Current.MainPage.Navigation.PopAsync();
               }
               else
               {
                  Application.Current.MainPage.DisplayAlert("Error", "Unable to update assessment", "Ok");
               }
            }
         }
         if (currentObjAssessment != null)
         {
            if (string.IsNullOrEmpty(name))
            {
               Application.Current.MainPage.DisplayAlert("Error", "No fields except PreAssessment can be empty.", "Ok");
            }
            else
            {
               ObjectiveAssessment assessment = new ObjectiveAssessment()
               {
                  AssessmentId = assessmentId,
                  Name = name,
                  Type = type,
                  DueDate = dueDate.ToString("MM-dd-yyyy"),
                  DueDateNotification = dueDateNotification,
                  CourseId = courseId,
                  PreAssessmentScore = preAssessmentScore,
                  ScheduledDate = scheduledDate,
                  ScheduledTime = scheduledTime.ToString(),
                  ScheduledDateNotification = scheduledDateNotification
               };
               if (EditObjAssessment.scheduled_change == false)
               {
                  assessment.ScheduledDate = currentObjAssessment.ScheduledDate;
                  assessment.ScheduledTime = currentObjAssessment.ScheduledTime;
               }
               currentObjAssessment = assessment;
               int updated = Assessment.UpdateAssessment(assessment);
               if (updated > 0)
               {
                  MessagingCenter.Send(this, "EditAssessment");
                  Application.Current.MainPage.Navigation.PopAsync();
               }
               else
               {
                  Application.Current.MainPage.DisplayAlert("Error", "Unable to update assessment", "Ok");
               }
            }
         }

      }
   } 
}
