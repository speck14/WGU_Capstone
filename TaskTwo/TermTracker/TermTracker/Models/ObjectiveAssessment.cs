using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TermTracker.ViewModels;

namespace TermTracker.Models
{
   public partial class ObjectiveAssessment : Assessment
   {
      public string PreAssessmentScore { get; set; }
      public string ScheduledDate { get; set; }
      public bool ScheduledDateNotification { get; set; }
      public ObjectiveAssessment() { }
      public ObjectiveAssessment(string PreAssessmentScore, string ScheduledDate, bool ScheduledDateNotification)
      {
         PreAssessmentScore = this.PreAssessmentScore;
         ScheduledDate = this.ScheduledDate;
         ScheduledDateNotification = this.ScheduledDateNotification;
      }
      public static int AddObjAssessment(ObjectiveAssessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            return conn.Insert(assessment);
         }
      }
      public static List<ObjectiveAssessment> GetObjAssessments(int courseId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            return conn.Table<ObjectiveAssessment>().Where(objectiveAssessment => objectiveAssessment.CourseId == courseId).ToList();
         }
      }
      public static int DeleteObjAssessment(int courseId)
      {
         var deleted = 0;
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            List<ObjectiveAssessment> assessments = conn.Query<ObjectiveAssessment>($"SELECT * FROM ObjectiveAssessment WHERE CourseId=courseId");
            foreach (var assessment in assessments)
            {
               conn.CreateTable<ObjectiveAssessment>();
               deleted += conn.Delete(assessment);
            }
            return deleted;
         }
      }
      public static int DeleteSingleObjAssessment(ObjectiveAssessment assessment)
      {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
               conn.CreateTable<ObjectiveAssessment>();
               return conn.Delete(assessment);
            }
      }

      public static string CheckObjNotifications()
      {
         var message = "";
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            List<ObjectiveAssessment> notifications = conn.Query<ObjectiveAssessment>($"SELECT * FROM ObjectiveAssessment WHERE DueDateNotification=TRUE OR ScheduledDateNotification=TRUE;");
            foreach (var notification in notifications)
            {
               if(notification.DueDateNotification == true)
               {
                  double days = DateTime.Parse(notification.DueDate).Subtract(DateTime.Today).TotalDays;
                  if (days <= 7 && days >= 0)
                  {
                     message += $"{notification.Type} Assessment: {notification.Name} is due {notification.DueDate}\n\n";
                  }
               }
               else if(notification.ScheduledDateNotification == true)
               {
                  double days = DateTime.Parse(notification.ScheduledDate).Subtract(DateTime.Today).TotalDays;
                  if (days <= 7 && days >= 0)
                  {
                     message += $"{notification.Type} Assessment: {notification.Name} is scheduled {notification.ScheduledDate}\n\n";
                  }
               }
            }
         }
         return message;
      }
   }
}
