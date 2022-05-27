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
         var objAssessment = ObjectiveAssessment.GetObjAssessments(currentCourse.CourseId);
         Assessments.Clear();
         foreach(var ob_assessment in objAssessment)
         {
            Assessments.Add(ob_assessment);
         }
         var perfAssessment = PerformanceAssessment.GetPerfAssessments(currentCourse.CourseId);
         foreach(var per_assessment in perfAssessment)
         {
            Assessments.Add(per_assessment);
         }
      }
      public static void OnAppearing(int courseId, ListView objAssessmentView, ListView perfAssessmentView)
      {
         var objAssessments = ObjectiveAssessment.GetObjAssessments(courseId);
         objAssessmentView.ItemsSource = objAssessments;
         perfAssessmentView.ItemsSource = PerformanceAssessment.GetPerfAssessments(courseId);
      }
   }
}