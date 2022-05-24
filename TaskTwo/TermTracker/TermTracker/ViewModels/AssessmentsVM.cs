using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using TermTracker.Models;
using TermTracker.Views;

namespace TermTracker.ViewModels
{
   public class AssessmentsVM : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      public Command ViewAssessmentCommand { get; set; }
      public Command AddAssessmentCommand { get; set; }
      public Command SetNotificationCommand { get; set; }

      private Course currentCourse;
      public ObservableCollection<Assessment> Assessments
      {
         get;
         set;
      }
      public AssessmentsVM(Course currentCourse)
      {
         this.currentCourse = currentCourse;
         Assessments = new ObservableCollection<Assessment>();
         GetAssessments();
         AddAssessmentCommand = new Command(AddAssessment, AllowAdd);

         MessagingCenter.Subscribe<ViewAssessmentVM>(this, "DeleteAssessment", (sender) =>
         {
            AddAssessmentCommand?.ChangeCanExecute();
         });
      }
      public void GetAssessments()
      {
         var assessments = Assessment.GetAssessments(currentCourse.CourseId);
         Assessments.Clear();
         foreach(var assessment in assessments)
         {
            Assessments.Add(assessment);
         }

         MessagingCenter.Subscribe<EditAssessmentVM>(this, "DeleteAssessment", (sender) =>
         {
            AddAssessmentCommand?.ChangeCanExecute();
         });
      }
      private int NumAssessments()
      {
         return Assessment.GetAssessments(currentCourse.CourseId).Count;
      }
      private bool AllowAdd()
      {
         if (NumAssessments() >= 2)
         {
            return false;
         }
         else
         {
            return true;
         }
      }
      public void AddAssessment()
      {
         MessagingCenter.Subscribe<AddAssessmentVM>(this, "AddAssessment", (sender) =>
         {
            AddAssessmentCommand?.ChangeCanExecute();
         });
         Console.WriteLine("AddAssessment clicked");
         Application.Current.MainPage.Navigation.PushAsync(new AddAssessment(currentCourse.CourseId));
      }
      public static void OnAppearing(int courseId, ListView assessmentView)
      {
         assessmentView.ItemsSource = Assessment.GetAssessments(courseId);
      }
   }
}
