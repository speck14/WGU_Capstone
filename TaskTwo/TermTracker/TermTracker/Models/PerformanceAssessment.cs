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
         AddAssessment(assessment);
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Insert(assessment);
         }
      }
      /*public static List<PerformanceAssessment> GetPerfAssessments(int assessmentId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Table<PerformanceAssessment>().Where(performanceAssessment => performanceAssessment.AssessmentID == assessmentId).ToList();
         }
      } */
   }
}
