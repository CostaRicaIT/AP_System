using NUnit.Framework;
using System;
using AccountsPayable.Controllers;
using AccountsPayable.Models;
using System.Web.Mvc;
using Moq;
using System.Web;

namespace AccountsPayable.Tests.Controllers
{
    [TestFixture]
    public class UserPermissionsTests
    {

        //Standard user tests
        [Test]
        public void Index_StandardUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Standard user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void Create_StandardUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Standard user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void Edit_StandardUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Standard user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }


        [Test]
        public void Details_StandardUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Standard user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        //Read only user tests
        [Test]
        public void Index_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read only user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void Create_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read only user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void Edit_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read only user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }


        [Test]
        public void Details_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read only user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        //Admin user tests
        [Test]
        public void Index_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void Create_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void Edit_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }


        [Test]
        public void Details_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }


        //User management view test

        [Test]
        public void UserManagement_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void UserManagement_StandardUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Standard user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void UserManagement_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read Only user permission

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

    }
}
