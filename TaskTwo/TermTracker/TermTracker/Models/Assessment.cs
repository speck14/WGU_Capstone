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
      //public string EndDate { get; set; }
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
      public static List<Assessment> GetAssessments(int courseId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Assessment>();
            return conn.Table<Assessment>().Where(assessment => assessment.CourseId == courseId).ToList();
         }
      }
      public static int AddAssessment(Assessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Assessment>();
            return conn.Insert(assessment);
         }
      }
      public static int UpdateAssessment(Assessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Assessment>();
            return conn.Update(assessment);
         }
      }
      public static int DeleteAssessment(Assessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Assessment>();
            return conn.Delete(assessment);
         }
      }
      public static List<string> GetTypes(int courseId)
      {
         var availableTypes = new List<string>()
         {
            "Performance",
            "Objective"
         };
         var assessments = GetAssessments(courseId);
         if (assessments.Count == 0)
         {
            return availableTypes;
         }
         else
         {
            foreach (var assessment in assessments)
            {
               if (assessment.Type == "Performance")
               {
                  availableTypes.Remove("Performance");
               }
               else if (assessment.Type == "Objective")
               {
                  availableTypes.Remove("Objective");
               }
            }
            return availableTypes;
         }
      }
      public static string CheckNotifications()
      {
         string message = "";
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Assessment>();
            List<Assessment> notifications = conn.Query<Assessment>($"SELECT * FROM Assessment WHERE DueDateNotification=TRUE;");
            foreach (var notification in notifications)
            {
               double days = DateTime.Parse(notification.DueDate).Subtract(DateTime.Today).TotalDays;
               if (days <= 7 && days >= 0)
               {
                  message += $"{notification.Type} Assessment: {notification.Name} is due {notification.DueDate}\n\n";
               }
            }
            message += ObjectiveAssessment.CheckNotifications();
            return message;
         }
      }
   }
}
