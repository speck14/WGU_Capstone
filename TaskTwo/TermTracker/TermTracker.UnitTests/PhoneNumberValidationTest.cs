using Microsoft.VisualStudio.TestTools.UnitTesting;
using TermTracker.Models;
using System;

namespace TermTracker.UnitTests
{
   [TestClass]
   public class PhoneNumberValidationTest
   {
      [TestMethod]
      public void Valid_PhoneNumber_WithDashes()
      {
         // Arrange
         string phone = "555-555-5555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsTrue(valid);
      }
      [TestMethod]
      public void Valid_PhoneNumber_WithoutSeparators()
      {
         // Arrange
         string phone = "5555555555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsTrue(valid);
      }
      [TestMethod]
      public void Valid_PhoneNumber_WithParens_WithoutSpace()
      {
         // Arrange
         string phone = "(555)555-5555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsTrue(valid);
      }
      [TestMethod]
      public void Valid_PhoneNumber_WithParens_WithSpace()
      {
         // Arrange
         string phone = "(555) 555-5555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsTrue(valid);
      }

      [TestMethod]
      public void Valid_PhoneNumber_WithSpaces()
      {
         // Arrange
         string phone = "555 555 5555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsTrue(valid);
      }

      [TestMethod]
      public void Invalid_PhoneNumber_With9Digits()
      {
         // Arrange
         string phone = "555-555-555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsFalse(valid);
      }

      [TestMethod]
      public void Invalid_PhoneNumber_With11Digits()
      {
         // Arrange
         string phone = "555-555-55555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsFalse(valid);
      }

      [TestMethod]
      public void Invalid_PhoneNumber_With10Chars()
      {
         // Arrange
         string phone = "aaa-aaa-aaaa";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsFalse(valid);
      }
      [TestMethod]
      public void Invalid_PhoneNumber_WithLeadingText()
      {
         // Arrange
         string phone = "aa555-555-5555";

         // Act
         bool valid = Validation.IsValidPhone(phone);

         // Assert
         Assert.IsFalse(valid);
      }
   }
}
