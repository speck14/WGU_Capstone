﻿using System;
using System.Collections.Generic;
using System.Text;
using TermTracker.ViewModels;
using SQLite;

namespace TermTracker.Models
{
   public class Course
   {
      [PrimaryKey, AutoIncrement]
      public int CourseId { get; set; }
      public string Name { get; set; }
      public string StartDate { get; set; }
      public string EndDate { get; set; }
      public string DueDate { get; set; }
      public string Status { get; set; }
      public string InstructorName { get; set; }
      public string InstructorEmail { get; set; }
      public string InstructorPhone { get; set; }
      public string Notes { get; set; }

      public bool StartNotification { get; set; }
      public bool EndNotification { get; set; }
      public int TermId { get; set; }
      public Course() {}

      public Course(int CourseId, string Name, string StartDate, string EndDate, string DueDate, string Status, string InstructorName, string InstructorEmail, string InstructorPhone, 
                     string Notes, bool StartNotification, bool EndNotification, int TermId)
      {
         CourseId = this.CourseId;
         Name = this.Name;
         StartDate = this.StartDate;
         EndDate = this.EndDate;
         DueDate = this.DueDate;
         Status = this.Status;
         InstructorName = this.InstructorName;
         InstructorEmail = this.InstructorEmail;
         InstructorPhone = this.InstructorPhone;
         Notes = this.Notes;
         StartNotification = this.StartNotification;
         EndNotification = this.EndNotification;
         TermId = this.TermId;
      }
      public Course(int CourseId, string Name, string StartDate, string EndDate, string DueDate, string Status, string InstructorName, string InstructorEmail, string InstructorPhone,
         bool StartNotification, bool EndNotification, int TermId)
      {
         CourseId = this.CourseId;
         Name = this.Name;
         StartDate = this.StartDate;
         EndDate = this.EndDate;
         DueDate = this.DueDate;
         Status = this.Status;
         InstructorName = this.InstructorName;
         InstructorEmail = this.InstructorEmail;
         InstructorPhone = this.InstructorPhone;
         StartNotification = this.StartNotification;
         EndNotification = this.EndNotification;
         TermId = this.TermId;
      }

      /* Deletes EVERYTHING in the Course table
      public static int DeleteContents()
      {
         using(SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Course>();
            return conn.DeleteAll<Course>();
         }
      }
      */

      public static List<Course> GetCourses(int termId)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Course>();
            return conn.Table<Course>().Where(course => course.TermId == termId).ToList();
         }
      }
      public static int AddCourse(Course course)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Course>();
            return conn.Insert(course);
         }
      }
      public static int UpdateCourse(Course course)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Course>();
            return conn.Update(course);
         }
      }
      public static int DeleteCourse(Course course)
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Course>();
            return conn.Delete(course);
         }
      }
      public static string CheckNotifications()
      {
         string message = "";
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
            conn.CreateTable<Course>();
            List<Course> notifications = conn.Query<Course>($"SELECT * FROM Course WHERE StartNotification=TRUE OR EndNotification=TRUE;");
            foreach (var notification in notifications)
            {
               double startDays = DateTime.Parse(notification.StartDate).Subtract(DateTime.Today).TotalDays;
               // Rubric didn't state when to notify, I chose within 7 days of date
               if (startDays <= 7 && startDays >= 0)
               {
                  message += $"{notification.Name} starts {notification.StartDate}\n\n";
               }
               double endDays = DateTime.Parse(notification.EndDate).Subtract(DateTime.Today).TotalDays;
               if (endDays <= 7 && endDays >= 0)
               {
                  message += $"{notification.Name} ends {notification.EndDate}\n\n";
                  
               }
            }
            Console.WriteLine(message);
            return message;
         }
      }
   }
}