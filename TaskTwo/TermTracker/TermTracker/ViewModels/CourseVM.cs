using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   public class CourseVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command DeleteCourseCommand { get; set; }
      public Command EditCourseCommand { get; set; }
      public Command EditNotesCommand { get; set; }
      public Command ShareNotesCommand { get; set; }
      public Command ViewAssessmentsCommand { get; set; }

      private Course currentCourse;

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
      private int courseId;
      public int CourseId
      {
         get { return courseId; }
         set
         {
            courseId = value;
            OnPropertyChanged("CourseId");
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

      private string recipient;
      public string Recipient
      {
         get { return recipient; }
         set
         {
            recipient = value;
            OnPropertyChanged("Recipient");
         }
      }
      private bool startNotification;
      public bool StartNotification
      {
         get { return startNotification; }
         set
         {
            startNotification = value;

         }
      }
      private bool endNotification;
      public bool EndNotification
      {
         get { return endNotification; }
         set
         {
            endNotification = value;
         }
      }

      public CourseVM(Course currentCourse)
      {
         this.currentCourse = currentCourse;
         courseId = currentCourse.CourseId;
         name = currentCourse.Name;         
         startDate = currentCourse.StartDate;
         endDate = currentCourse.EndDate;
         dueDate = currentCourse.DueDate;
         status = currentCourse.Status;
         instructorName = currentCourse.InstructorName;
         instructorEmail = currentCourse.InstructorEmail;
         instructorPhone = currentCourse.InstructorPhone;
         notes = currentCourse.Notes;
         startNotification = currentCourse.StartNotification;
         endNotification = currentCourse.EndNotification;

         DeleteCourseCommand = new Command(DeleteCourse);
         EditCourseCommand = new Command(EditCourse);
         EditNotesCommand = new Command(EditNotes);
         ShareNotesCommand = new Command(ShareNotes);
         ViewAssessmentsCommand = new Command(ViewAssessments);
      }
      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      public async void DeleteCourse()
      {
         bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Confirm Deletion", $"Do you really want to delete {currentCourse.Name} and its notes and assessments?", "Yes", "No");
         if(confirmDelete)
         {
            int del_course = Course.DeleteCourse(currentCourse);
            if(del_course > 0)
            {
               MessagingCenter.Send(this, "DeleteCourse");
               await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               await Application.Current.MainPage.DisplayAlert("Error", "Unable to delete course", "Ok");
            }
         }
      }
      public void EditCourse()
      {
         MessagingCenter.Subscribe<EditCourseVM>(this, "EditCourse", (sender) =>
         {
            currentCourse = sender.currentCourse;
            name = sender.Name;
            startDate = sender.StartDate.ToString("MM-dd-yyyy");
            endDate = sender.EndDate.ToString("MM-dd-yyyy");
            dueDate = sender.DueDate.ToString("MM-dd-yyyy");
            status = sender.Status;
            instructorName = sender.InstructorName;
            instructorEmail = sender.InstructorEmail;
            instructorPhone = sender.InstructorPhone;
            notes = sender.Notes;
            startNotification = sender.StartNotification;
            endNotification = sender.EndNotification;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

            currentCourse.Name = sender.Name;
         });
         Application.Current.MainPage.Navigation.PushAsync(new EditCourse(currentCourse));
      }
      public void EditNotes()
      {
         MessagingCenter.Subscribe<EditCourseVM>(this, "EditNotes", (sender) =>
         {
            currentCourse = sender.currentCourse;
            name = sender.Name;
            startDate = sender.StartDate.ToString("MM-dd-yyyy");
            endDate = sender.EndDate.ToString("MM-dd-yyyy");
            dueDate = sender.DueDate.ToString("MM-dd-yyyy");
            status = sender.Status;
            instructorName = sender.InstructorName;
            instructorEmail = sender.InstructorEmail;
            instructorPhone = sender.InstructorPhone;
            notes = sender.Notes;
            startNotification = sender.StartNotification;
            endNotification = sender.EndNotification;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
         });
         Application.Current.MainPage.Navigation.PushAsync(new EditNotes(currentCourse));
      }
      private async void ShareNotes()
      {
         if(!string.IsNullOrEmpty(recipient) && Validation.IsValidPhone(recipient))
         {
            try
            {
               SmsMessage message = new SmsMessage(notes, recipient);
               await Sms.ComposeAsync(message);

            }
            catch (FeatureNotSupportedException err)
            {
               await Application.Current.MainPage.DisplayAlert("Error", "SMS messaging not supported on this device.", "Ok");
            }
            catch (Exception err)
            {
               Console.WriteLine(err);
               await Application.Current.MainPage.DisplayAlert("Error", "Unable to send your message.", "Ok");
            }
         }
         else
         {
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid phone number.", "Ok");
         }
      }
      public void ViewAssessments()
      {
         Application.Current.MainPage.Navigation.PushAsync(new ViewAssessments(currentCourse));
      }
   }
}
