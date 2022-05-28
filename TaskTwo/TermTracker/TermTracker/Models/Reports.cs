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
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<ObjectiveAssessment>();
            return conn.Table<ObjectiveAssessment>().Where(assessment => DateTime.Parse(assessment.ScheduledDate) >= StartDate && DateTime.Parse(assessment.ScheduledDate) <= EndDate).ToList();
         }
      }
   }
}
