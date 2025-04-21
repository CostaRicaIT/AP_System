
using System;
using NUnit.Framework;
using AccountsPayable.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;

namespace AccountsPayable.Tests.Controllers
{
    [TestFixture]
    public class AccessControllerTest
    {
        [Test]
        public void LoginAuthorize_CorrectCredentials_ReturnsContent1()
        {
            // Arrange
            var controller = new AccessController();
            var expectedContent = "1";
            var username = "User.Lead";
            var password = "password"; // Replace with actual valid credentials in your database

            var mockHttpContext = new Mock<HttpContextBase>();// Mock HttpContext setup
            var mockSession = new Mock<HttpSessionStateBase>();
            mockHttpContext.SetupGet(x => x.Session).Returns(mockSession.Object);
            // Set the HttpContext for the controller
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = controller.LoginAuthorize(username, password) as ContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, result.Content);
            mockSession.VerifySet(x => x["User"] = It.IsAny<object>(), Times.Once);
            mockSession.VerifySet(x => x["CurrentUserName"] = It.IsAny<string>(), Times.Once);
            mockSession.VerifySet(x => x["Permission"] = It.IsAny<object>(), Times.Once);
        }

        [Test]
        public void LoginAuthorize_IncorrectCredentials_ReturnsErrorMessage()
        {
            // Arrange
            var controller = new AccessController();
            var expectedContent = "Incorrect username or password, please try again";
            var username = "User.Test";
            var password = "password"; // Replace with actual invalid credentials in your database

            // Act
            var result = controller.LoginAuthorize(username, password) as ContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, result.Content);
        }
    }
}
