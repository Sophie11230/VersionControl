using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample
{
    class AccountControllerTestFixture
    {        

        [Test]
        public void TestValidateEmail(String email, bool expectedRes)
        {
            // Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidateEmail(email);            
            //Assert
            Assert.AreEqual(expectedRes, actualResult);

        }
    }



}
