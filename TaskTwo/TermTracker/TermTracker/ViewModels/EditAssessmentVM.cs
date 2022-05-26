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
   class EditAssessmentVM //: INotifyPropertyChanged
   {/*
      public event PropertyChangedEventHandler PropertyChanged;
      public Command UpdateAssessmentCommand { get; set; }

      public Assessment currentAssessment;
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
      private DateTime startDate;
      public DateTime StartDate
      {
         get { return startDate; }
         set
         {
            startDate = value;
            OnPropertyChanged("StartDate");
         }
      }
      private DateTime endDate;
      public DateTime EndDate
      {
         get { return endDate; }
         set
         {
            endDate = value;
            OnPropertyChanged("EndDate");
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

      public EditAssessmentVM(Assessment currentAssessment)
      {
         this.currentAssessment = currentAssessment;
         courseId = currentAssessment.CourseId;
         assessmentId = currentAssessment.AssessmentId;
         name = currentAssessment.Name;
         type = currentAssessment.Type;
         startDate = DateTime.Parse(currentAssessment.StartDate);
         endDate = DateTime.Parse(currentAssessment.EndDate);
         dueDate = DateTime.Parse(currentAssessment.DueDate);
         dueDateNotification = currentAssessment.DueDateNotification;

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
         if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
         {
            Application.Current.MainPage.DisplayAlert("Error", "No fields except Notes can be empty.", "Ok");
         }
         else if (startDate > endDate || startDate > dueDate)
         {
            Application.Current.MainPage.DisplayAlert("Error", "Start date can't be after end date or due date.", "Ok");
         }
         else if (endDate > dueDate)
         {
            Application.Current.MainPage.DisplayAlert("Error", "Expected course end date can't be after due date.", "Ok");
         }
         else
         {
            Assessment assessment = new Assessment()
            {
               AssessmentId = assessmentId,
               Name = name,
               Type = type,
               StartDate = startDate.ToString("MM-dd-yyyy"),
               EndDate = endDate.ToString("MM-dd-yyyy"),
               DueDate = dueDate.ToString("MM-dd-yyyy"),
               DueDateNotification = dueDateNotification,
               CourseId = courseId
            };
            currentAssessment = assessment;
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
      }*/
   } 
}
