using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   public class AddCourseVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      public Command AddCommand { get; set; }

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
      private string status;
      public string Status
      {
         get { return status; }
         set
         {
            status = value;
            OnPropertyChanged("Status");
         }
      }
      private string notes;
      public string Notes
      {
         get { return notes; }
         set
         {
            notes = value;
            OnPropertyChanged("Notes");
         }
      }
      private string instructorName;
      public string InstructorName
      {
         get { return instructorName; }
         set
         {
            instructorName = value;
            OnPropertyChanged("InstructorName");
         }
      }
      private string instructorPhone;
      public string InstructorPhone
      {
         get { return instructorPhone; }
         set
         {
            instructorPhone = value;
            OnPropertyChanged("InstructorPhone");
         }
      }
      private string instructorEmail;
      public string InstructorEmail
      {
         get { return instructorEmail; }
         set
         {
            instructorEmail = value;
            OnPropertyChanged("InstructorEmail");
         }
      }
      private bool startNotification;
      public bool StartNotification
      {
         get { return startNotification; }
         set
         {
            startNotification = value;
            OnPropertyChanged("StartNotification");
         }
      }
      private bool endNotification;
      public bool EndNotification
      {
         get { return endNotification; }
         set
         {
            endNotification = value;
            OnPropertyChanged("EndNotification");
         }
      }
      private static int termId;
      public int TermId
      {
         get { return termId; }
         set
         {
            termId = value;
            OnPropertyChanged("TermId");
         }
      }

      public AddCourseVM()
      {
         startDate = DateTime.Now.Date;
         endDate = DateTime.Now.Date.AddDays(30);
         dueDate= DateTime.Now.Date.AddDays(30);
         startNotification = true;
         endNotification = true;
         AddCommand = new Command(AddCourse);
      }
      public static void SetTermId(int id)
      {
         termId = id;
      }

      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }

      private async void AddCourse()
      {
         if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(instructorName) || string.IsNullOrEmpty(instructorEmail) || string.IsNullOrEmpty(instructorPhone))
         {
            await Application.Current.MainPage.DisplayAlert("Error", "No fields except Notes can be empty.", "Ok");
         }
         else if (!Validation.IsValidEmail(instructorEmail))
         {
            await Application.Current.MainPage.DisplayAlert("Error", "Instructor email invalid.", "Ok");
         }
         else if (!Validation.IsValidPhone(instructorPhone))
         {
            await Application.Current.MainPage.DisplayAlert("Error", "Please use this phone format: XXX-XXX-XXXX", "Ok");
         }
         else if (startDate > endDate || startDate > dueDate)
         {
            await Application.Current.MainPage.DisplayAlert("Error", "Start date can't be after end date or due date.", "Ok");
         }
         else if (endDate > dueDate)
         {
            await Application.Current.MainPage.DisplayAlert("Error", "Expected course end date can't be after due date.", "Ok");
         }
         else
         {
            Course course = new Course()
            {
               Name = name,
               StartDate = startDate.Date.ToString("MM-dd-yyyy"),
               EndDate = endDate.Date.ToString("MM-dd-yyyy"),
               DueDate = dueDate.Date.ToString("MM-dd-yyyy"),
               Status = status,
               Notes = notes,
               InstructorName = instructorName,
               InstructorPhone = instructorPhone,
               InstructorEmail = instructorEmail,
               StartNotification = startNotification,
               EndNotification = endNotification,
               TermId = termId
            };
            var response = Course.AddCourse(course);
            if (response > 0)
            {
               ObjectiveAssessment objectiveAssessment = new ObjectiveAssessment()
               {
                  Name = "Objective Assessment",
                  Type = "Objective",
                  DueDate = course.DueDate,
                  DueDateNotification = false,
                  CourseId = response,
                  PreAssessmentScore = "Not entered",
                  ScheduledDate = "",
                  ScheduledDateNotification = false
               };
               PerformanceAssessment performanceAssessment = new PerformanceAssessment()
               {
                  Name = "Performance Assessment",
                  Type = "Performance",
                  DueDate = course.DueDate,
                  DueDateNotification = false,
                  CourseId = response
               };
               var assessment_res = ObjectiveAssessment.AddObjAssessment(objectiveAssessment);
               assessment_res += PerformanceAssessment.AddPerfAssessment(performanceAssessment);
               if(assessment_res > 0)
               {
                  Console.WriteLine(assessment_res);
                  MessagingCenter.Send(this, "AddCourse");
                  await App.Current.MainPage.Navigation.PopAsync();
               }
            }
            else
            {
               await Application.Current.MainPage.DisplayAlert("Error", "Unable to add course", "Ok");
            }
         }
      }
   }
}
