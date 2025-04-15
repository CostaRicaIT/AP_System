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

        //Lead User tests
        [Test]
        public void Index_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Create_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Edit_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Details_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void WorkSpace_Create_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }


        [Test]
        public void WorkSpace_Edit_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void WorkSpace_View_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(21) as ViewResult;

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }



        [Test]
        public void WorkSpace_Create_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read Only User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void WorkSpace_Edit_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read Only User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void WorkSpace_View_ReadOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 3 }; // Read Only User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(21) as ViewResult;

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user


            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }


        [Test]
        public void WorkSpace_Create_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void WorkSpace_Edit_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; //Admin User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void WorkSpace_View_AdminUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 1 }; // Admin User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);

        }


        //Manager User tests
        [Test]
        public void Index_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Manager User role (valid)
            var user = new object(); // Simulate a logged-in user

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // default view
        }

        [Test]
        public void Create_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Manager User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Edit_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Details_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Manager User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check
            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void WorkSpace_Create_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Manager User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check
            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }


        [Test]
        public void WorkSpace_Edit_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Manager User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void WorkSpace_View_ManagerUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; // Manager User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }


        //Associate User tests
        [Test]
        public void Index_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Create_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Edit_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Details_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void WorkSpace_Create_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5}; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }


        [Test]
        public void WorkSpace_Edit_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }

        [Test]
        public void WorkSpace_View_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName)); // Check if the returned view name is empty or null
        }


        //No User tests
        [Test]
        public void Index_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Create_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Edit_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void Details_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new MainController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void WorkSpace_Create_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Create(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }


        [Test]
        public void WorkSpace_Edit_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Edit(21) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void WorkSpace_View_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new WorkSpaceController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

            mockSession.SetupGet(s => s["Permission"]).Returns(permission);
            mockHttpContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new System.Web.Routing.RouteData(), controller);

            // Act
            var result = controller.Details(21) as ViewResult;

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void UserManagement_LeadUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 2 }; // Lead User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void UserManagement_ManagerOnlyUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 4 }; //Manager user permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void UserManagement_AssociateUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 5 }; // Associate Only user permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
        public void UserManagement_NoUserPermission_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockSession = new Mock<HttpSessionStateBase>();

            var permission = new TB_VIEW_PERMISSIONS { FK_TB_LOGIN_ROLE_ID = 0 }; // No User permission
            var user = new object(); // Simulate a logged-in user
            mockSession.SetupGet(s => s["User"]).Returns(user); // This is key for the "Login" check

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
