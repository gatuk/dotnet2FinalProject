﻿using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    [TestClass]
    public class EmployeeManagerTests
    {
        IEmployeeManager _employeeManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _employeeManager = new EmployeeManager(new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue()
        {
            // in TDD (test-driven development, the test comes first)
            // we use the red-green-refactor workflow
            // we write the test method with the A-A-A framework

            // Arrange - set up the test condition
            string testString = "newuser";
            string actualHash = "";
            string expectedHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";

            // Act - invoke the method being tested, and capture results
            actualHash = _employeeManager.HashSha256(testString);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestAuthenticateEmployeePassesWithCorrectEmailAndPassword()
        {
            // arrange
            string email = "tess@company.com";
            string password = "newuser";
            bool expectedResult = true;
            bool actualResult = false;

            // act
            actualResult = _employeeManager.AuthenticateEmployee(email, password);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAuthenticateEmployeeFailsWithBadEmailAndPassword()
        {
            // arrange
            string email = "tess@company.com";
            string password = "newloser";           // bad password
            bool expectedResult = false;
            bool actualResult = true;

            // act
            actualResult = _employeeManager.AuthenticateEmployee(email, password);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetEmployeeByEmailReturnsCorrectEmployee()
        {
            // arrange
            string email = "tess@company.com";
            int expectedEmployeeID = 1;
            int actualEmployeeID = 0;

            // act
            EmployeeVM employeeVM = _employeeManager.GetEmployeeVMByEmail(email);
            actualEmployeeID = employeeVM.EmployeeID;

            // assert
            Assert.AreEqual(expectedEmployeeID, actualEmployeeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetEmployeeByEmailFailsWithBadEmail()
        {
            // arrange 
            string email = "ness@company.com"; // bad email
            // int expectedEmployeeID = 1;
            int actualEmployeeID = 0;

            // act
            EmployeeVM employeeVM = _employeeManager.GetEmployeeVMByEmail(email);
            actualEmployeeID = employeeVM.EmployeeID;

            // assert - nothing to do
        }

        [TestMethod]
        public void TestGetRolesByEmployeeIDReturnsCorrectRoles()
        {
            // arrange
            int testID = 1;
            int expectedRoleCount = 2;
            int actualRoleCount = 0;

            // act
            actualRoleCount = _employeeManager.GetRolesByEmployeeID(testID).Count;

            // assert
            Assert.AreEqual(expectedRoleCount, actualRoleCount);
        }

        [TestMethod]
        public void TestResetPasswordWorksCorrectly()
        {
            // arrange
            string email = "tess@company.com";
            string password = "newuser";
            string newPassword = "password";
            bool expectedResult = true;
            bool actualResult = false;

            // act
            actualResult = _employeeManager.ResetPassword(email, password, newPassword);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
