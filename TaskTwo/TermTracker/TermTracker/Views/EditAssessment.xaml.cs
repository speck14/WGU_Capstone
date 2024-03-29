﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.ViewModels;
using TermTracker.Models;

namespace TermTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class EditAssessment : ContentPage
   {
      private int courseId;
      public EditAssessment(PerformanceAssessment currentAssessment)
      {
         InitializeComponent();
         this.courseId = currentAssessment.CourseId;
         this.BindingContext = new EditAssessmentVM(currentAssessment);
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         EditAssessmentVM.SetCourseId(courseId);
      }
   }
}