using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Models
{
   public partial class PerformanceAssessment : Assessment
   {
      [PrimaryKey, AutoIncrement]
      public int PerfAssessmentId { get; set; }
      public int AssessmentFK { get; set; }
      public PerformanceAssessment() { }
      public PerformanceAssessment(int ObjAssessmentId, int AssessmentFK)
      {
         PerfAssessmentId = this.PerfAssessmentId;
         AssessmentFK = this.AssessmentFK;
      }
      public static int AddPerfAssessment(PerformanceAssessment assessment)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Insert(assessment);
         }
      }
      public static List<PerformanceAssessment> GetPerfAssessments(int assessmentId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<PerformanceAssessment>();
            return conn.Table<PerformanceAssessment>().Where(performanceAssessment => performanceAssessment.AssessmentFK == assessmentId).ToList();
         }
      }
   }
}
