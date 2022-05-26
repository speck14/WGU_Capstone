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
   public class AssessmentsVM// : INotifyPropertyChanged
   { 
     // public event PropertyChangedEventHandler PropertyChanged;
      public Command ViewAssessmentCommand { get; set; }
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
      }
      public void GetAssessments()
      {
         var assessments = Assessment.GetAssessments(currentCourse.CourseId);
         Assessments.Clear();
         foreach(var assessment in assessments)
         {
            Assessments.Add(assessment);
         }
      }
      public static void OnAppearing(int courseId, ListView assessmentView)
      {
         assessmentView.ItemsSource = Assessment.GetAssessments(courseId);
      }
   }
}