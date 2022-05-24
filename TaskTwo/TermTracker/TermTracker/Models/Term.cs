using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace TermTracker.Models
{
   public class Term
   {
      [PrimaryKey, AutoIncrement]
      public int TermId { get; set; }
      public string Title { get; set; }
      public string StartDate { get; set; }
      public string EndDate { get; set; }
      public Term() {}

      public Term(int TermId, string Title, string StartDate, string EndDate)
      {
         TermId = this.TermId;
         Title = this.Title;
         StartDate = this.StartDate;
         EndDate = this.EndDate;
      }
      public static List<Term> GetTerms()
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Term>();
            return conn.Table<Term>().ToList();
         }
      }
      public static int AddTerm(Term term)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Term>();
            return conn.Insert(term);
         }
      }
      public static int UpdateTerm(Term term)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Term>();
            return conn.Update(term);
         }
      }
      public static int DeleteTerm(Term term)
      {
         using(SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Term>();
            return conn.Delete(term);
         }
      }
   }
}
