using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Models;
using TermTracker.Views;
using System.ComponentModel;
using SQLite;

namespace TermTracker.Models
{
   class Reports 
   {
      public Reports() { }
      public static List<ObjectiveAssessment> GenerateReports(DateTime StartDate, DateTime EndDate)
      {
         var reportResults = new List<ObjectiveAssessment>();
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            var allAssessments =  conn.Table<ObjectiveAssessment>().ToList();
            foreach (var assessment in allAssessments)
            {
               if(DateTime.Parse(assessment.ScheduledDate) >= StartDate && DateTime.Parse(assessment.ScheduledDate) <= EndDate)
               {
                  if(assessment.ScheduledTime != "00:00" || assessment.ScheduledDate != assessment.DueDate)
                  reportResults.Add(assessment);
               }
            }
         }
         return reportResults;
      }
   }
}
