using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PasswordLib;

namespace PasswordLibTest
{
    [TestClass]
    public class PasswordLibUnitTest
    {
        [TestMethod]
        public void PasswordComplexity_admin_Ненадежныйreturned()
        {
            string password = "admin";
            string expected = "Ненадежный";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PasswordComplexity_12345_Ненадежныйreturned()
        {
            string password = "12345";
            string expected = "Ненадежный";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PasswordComplexity_admin12_Простойreturned()
        {
            string password = "admin12";
            string expected = "Простой";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PasswordComplexity_Admin20_Простойreturned()
        {
            string password = "Admin20";
            string expected = "Простой";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PasswordComplexity_Admin2019_Хорошийreturned()
        {
            string password = "Admin2019";
            string expected = "Хороший";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PasswordComplexity_Admin20sobaka_Сложныйreturned()
        {
            string password = "Admin20@";
            string expected = "Сложный";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PasswordComplexity_Qw1star_Хорошийreturned()
        {
            string password = "Qw1*";
            string expected = "Хороший";
            string actual = Password.PasswordComplexity(password);
            Assert.AreEqual(expected, actual);
        }
    }
}
