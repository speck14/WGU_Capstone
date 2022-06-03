using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TermTracker.Models
{
   public static class Validation
   {
      public static bool IsValidEmail(string email)
      {
         // Regex source: https://stackoverflow.com/a/9853682
         Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
         Match match = regex.Match(email);
         if (match.Success)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
      public static bool IsValidPhone(string phone)
      {
         // Regex Source: https://stackoverflow.com/a/18091377
         Regex regex = new Regex(@"^\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$");
         Match match = regex.Match(phone);
         if (match.Success)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
      public static bool IsValidTime(string time)
      {
         // RegEx pattern source: https://www.geeksforgeeks.org/how-to-validate-time-in-24-hour-format-using-regular-expression/
         Regex regex = new Regex(@"([01]?[0-9]|2[0-3]):[0-5][0-9]");
         Match match = regex.Match(time);
         if (match.Success)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
   }
}
