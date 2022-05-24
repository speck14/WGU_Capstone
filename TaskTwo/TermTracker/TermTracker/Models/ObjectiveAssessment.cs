using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TermTracker.ViewModels;

namespace TermTracker.Models
{
   public partial class ObjectiveAssessment : Assessment
   {
      [PrimaryKey, AutoIncrement]
      public int ObjAssessmentId { get; set; }
      public int AssessmentFK { get; set; }
      public string PreAssessmentScore { get; set; }
      public DateTime ScheduledDate { get; set; }
      public bool ScheduledDateNotification { get; set; }
      public ObjectiveAssessment() { }
      public ObjectiveAssessment(int ObjAssessmentId, int AssessmentFK, string PreAssessmentScore, DateTime ScheduledDate, bool ScheduledDateNotification)
      {
         ObjAssessmentId = this.ObjAssessmentId;
         AssessmentFK = this.AssessmentFK;
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
      public static List<ObjectiveAssessment> GetObjAssessments(int assessmentId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            return conn.Table<ObjectiveAssessment>().Where(objectiveAssessment => objectiveAssessment.AssessmentFK == assessmentId).ToList();
         }
      }

      // Implement ScheduledDateNotificaitons
      public static string CheckObjNotifications()
      {
         string message = "";
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            List<ObjectiveAssessment> notifications = conn.Query<ObjectiveAssessment>($"SELECT * FROM ObjectiveAssessment WHERE ScheduledDateNotification=TRUE;");
            foreach (var notification in notifications)
            {
               double days = notification.ScheduledDate.Subtract(DateTime.Today).TotalDays;
               if (days <= 7 && days >= 0)
               {
                  message += $"Objective Assessment: {notification.Name} is scheduled for {notification.ScheduledDate}\n\n";
               }
            }
            return message;
         }
      }
}
