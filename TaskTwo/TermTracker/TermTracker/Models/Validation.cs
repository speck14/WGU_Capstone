using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TermTracker.Models
{
   class Validation
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
         Regex regex = new Regex(@"[0-9]{3}-[0-9]{3}-[0-9]{4}");
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
   }
}
