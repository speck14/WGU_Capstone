using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   class AddAssessmentVM : INotifyPropertyChanged
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

      public AddAssessmentVM()
      {
         startDate = DateTime.Now.Date;
         endDate = DateTime.Now.Date.AddDays(30);
         dueDate = DateTime.Now.Date.AddDays(30);

         AddCommand = new Command(AddAssessment);
      }
      public static void SetCourseId(int id)
      {
         courseId = id;
      }

      private void OnPropertyChanged(string property)
      {
         PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
      private async void AddAssessment()
      {
         if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
         {
            await Application.Current.MainPage.DisplayAlert("Error", "No fields can be empty.", "Ok");
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
            Console.WriteLine(dueDateNotification);
            Assessment assessment = new Assessment()
            {
               Name = name,
               Type = type,
               StartDate = startDate.Date.ToString("MM-dd-yyyy"),
               EndDate = endDate.Date.ToString("MM-dd-yyyy"),
               DueDate = dueDate.Date.ToString("MM-dd-yyyy"),
               DueDateNotification = dueDateNotification,
               CourseId = courseId
            };
            var response = Assessment.AddAssessment(assessment);
            if (response > 0)
            {
               MessagingCenter.Send(this, "AddAssessment");
               Console.WriteLine($"{assessment.Name} added!");
               await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               await Application.Current.MainPage.DisplayAlert("Error", "Unable to add assessment", "Ok");
            }
         }
      }
   }
}
