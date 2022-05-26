using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Models
{
   public partial class PerformanceAssessment : Assessment
   {
      public PerformanceAssessment() { }

      public static int AddPerfAssessment(PerformanceAssessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Insert(assessment);
         }
      }
      public static List<PerformanceAssessment> GetPerfAssessments(int courseId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Table<PerformanceAssessment>().Where(performanceAssessment => performanceAssessment.CourseId == courseId).ToList();
         }
      }
      public static string CheckPerfNotifications()
      {
         var message = "";
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            List<PerformanceAssessment> notifications = conn.Query<PerformanceAssessment>($"SELECT * FROM PerformanceAssessment WHERE DueDateNotification=TRUE;");
            foreach (var notification in notifications)
            {
               double days = DateTime.Parse(notification.DueDate).Subtract(DateTime.Today).TotalDays;
               if (days <= 7 && days >= 0)
               {
                  message += $"{notification.Type} Assessment: {notification.Name} is due {notification.DueDate}\n\n";
               }
            }
         }
         return message;
      }

      public static int DeletePerfAssessment(int courseId)
      {
         var deleted = 0;
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            List<PerformanceAssessment> assessments = conn.Query<PerformanceAssessment>($"SELECT * FROM PerformanceAssessment WHERE CourseId=courseId");
            foreach (var assessment in assessments)
            {
               conn.CreateTable<PerformanceAssessment>();
               deleted += conn.Delete(assessment);
            }
            return deleted;
         }
      }
      public static int DeleteSinglePerfAssessment(PerformanceAssessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Delete(assessment);
         }
      }
   }
}
