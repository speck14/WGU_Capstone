using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using TermTracker.Models;
using System.Text.RegularExpressions;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   class EditCourseVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command UpdateCommand { get; set; }
      public Command UpdateNotesCommand { get; set; }

      public Course currentCourse;
      private static int courseId;
      private static int termId;
   
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

      public EditCourseVM(Course currentCourse)
      {
         this.currentCourse = currentCourse;
         courseId = currentCourse.CourseId;
         name = currentCourse.Name;
         startDate = DateTime.Parse(currentCourse.StartDate);
         endDate = DateTime.Parse(currentCourse.EndDate);
         dueDate = DateTime.Parse(currentCourse.DueDate);
         status = currentCourse.Status;
         instructorName = currentCourse.InstructorName;
         instructorEmail = currentCourse.InstructorEmail;
         instructorPhone = currentCourse.InstructorPhone;
         notes = currentCourse.Notes;
         startNotification = currentCourse.StartNotification;
         endNotification = currentCourse.EndNotification;
         termId = currentCourse.TermId;

         UpdateCommand = new Command(Update);
         UpdateNotesCommand = new Command(UpdateNotes);

      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      private void UpdateNotes()
      {
         Course course = new Course()
         {
            CourseId = courseId,
            Name = name,
            StartDate = startDate.ToString("MM-dd-yyyy"),
            EndDate = endDate.ToString("MM-dd-yyyy"),
            DueDate = dueDate.ToString("MM-dd-yyyy"),
            Status = status,
            InstructorName = instructorName,
            InstructorEmail = instructorEmail,
            InstructorPhone = instructorPhone,
            Notes = notes,
            StartNotification = startNotification,
            EndNotification = endNotification,
            TermId = termId
         };
         int updated = Course.UpdateCourse(course);
         if (updated > 0)
         {
            currentCourse = course;
            MessagingCenter.Send(this, "EditNotes");
            Application.Current.MainPage.Navigation.PopAsync();
         }
         else
         {
            Application.Current.MainPage.DisplayAlert("Error", "Unable to update notes", "Ok");
         }
      }
      private void Update()
      {
         if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(instructorName) || string.IsNullOrEmpty(instructorEmail) || string.IsNullOrEmpty(instructorPhone))
         {
            Application.Current.MainPage.DisplayAlert("Error", "No fields except Notes can be empty.", "Ok");
         }
         else if (!Validation.IsValidEmail(instructorEmail))
         {
            Application.Current.MainPage.DisplayAlert("Error", "Instructor email invalid.", "Ok");
         }
         else if (!Validation.IsValidPhone(instructorPhone))
         {
            Application.Current.MainPage.DisplayAlert("Error", "Please use this phone format: XXX-XXX-XXXX", "Ok");
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
            Course course = new Course()
            {
               CourseId = courseId,
               Name = name,
               StartDate = startDate.ToString("MM-dd-yyyy"),
               EndDate = endDate.ToString("MM-dd-yyyy"),
               DueDate = dueDate.ToString("MM-dd-yyyy"),
               Status = status,
               InstructorName = instructorName,
               InstructorEmail = instructorEmail,
               InstructorPhone = instructorPhone,
               Notes = notes,
               StartNotification = startNotification,
               EndNotification = endNotification,
               TermId = termId
            };
            int updated = Course.UpdateCourse(course);
            if (updated > 0)
            {
               currentCourse = course;
               MessagingCenter.Send(this, "EditCourse");
               Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Unable to update course", "Ok");
            }
         }
      }
   }
}
