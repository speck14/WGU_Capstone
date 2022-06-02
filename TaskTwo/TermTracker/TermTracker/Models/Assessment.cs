using System;
using System.Collections.Generic;
using System.Text;
using TermTracker.ViewModels;
using SQLite;

namespace TermTracker.Models
{
   public class Assessment
   {
      [PrimaryKey, AutoIncrement]
      public int AssessmentId { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public string DueDate { get; set; }
      public bool DueDateNotification { get; set; }
      public int CourseId { get; set; }

      public Assessment() {}

      public Assessment(int AssessmentId, string Name, string Type, string DueDate, bool DueDateNotification, int CourseId)
      {
         AssessmentId = this.AssessmentId;
         Name = this.Name;
         Type = this.Type;
         DueDate = this.DueDate;
         DueDateNotification = this.DueDateNotification;
         CourseId = this.CourseId;
      }
      public static int AddAssessment(ObjectiveAssessment assessment)
      {
            return ObjectiveAssessment.AddObjAssessment(assessment);
      }
      public static int AddAssessment(PerformanceAssessment assessment)
      {
         return PerformanceAssessment.AddPerfAssessment(assessment);
      }
      public static int UpdateAssessment(ObjectiveAssessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            return conn.Update(@assessment);
         }
      }
      public static int UpdateAssessment(PerformanceAssessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Update(@assessment);
         }
      }
      public static int DeleteAssessments(int courseId)
      {
         var deleted = 0;
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            // Delete from Assessment table in case there's anything in there
            List<Assessment> assessments = conn.Query<Assessment>($"SELECT * FROM Assessment WHERE CourseId=courseId");
            foreach(var assessment in assessments)
            {
               conn.CreateTable<Assessment>();
               deleted += conn.Delete(assessment);
            }
            deleted += ObjectiveAssessment.DeleteObjAssessment(courseId);
            deleted += PerformanceAssessment.DeletePerfAssessment(courseId);
            return deleted;
         }
      }
      public static string CheckNotifications()
      {
         string message = "";
         message += PerformanceAssessment.CheckPerfNotifications();
         message += ObjectiveAssessment.CheckObjNotifications();
         return message;
      }
   }
}
