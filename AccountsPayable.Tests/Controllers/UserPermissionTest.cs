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
        [SetUp]
        public void SetUp()
        {
            ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //Lead User tests
        [Test]

        public void Index_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user index view start");
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
            ExtentReportManager.LogPass("Lead user index view success");
        }

        [Test]
        public void Create_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user Create view start");
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
            ExtentReportManager.LogPass("Lead user Create view success");
        }

        [Test]
        public void Edit_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user Edit view start");
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
            ExtentReportManager.LogPass("Lead user Edit view success");
        }


        [Test]
        public void Details_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user Details view start");
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
            ExtentReportManager.LogPass("Lead user Details view success");
        }

        [Test]
        public void WorkSpace_Create_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user Workspace Create view start");
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
            ExtentReportManager.LogPass("Lead user Workspace Create view success");
        }


        [Test]
        public void WorkSpace_Edit_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user Workspace Edit view start");
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
            ExtentReportManager.LogPass("Lead user Workspace Edit view success");
        }

        [Test]
        public void WorkSpace_View_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead user Workspace Details view start");
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
            ExtentReportManager.LogPass("Lead user Workspace Details view success");
        }

        //Read only user tests
        [Test]
        public void Index_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user index view start");
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
            ExtentReportManager.LogPass("Read user index view success");
        }

        [Test]
        public void Create_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user Create view start");
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
            ExtentReportManager.LogPass("Read user Create view success");
        }

        [Test]
        public void Edit_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user Edit view start");
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
            ExtentReportManager.LogPass("Read user Edit view success");
        }


        [Test]
        public void Details_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user Details view start");
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
            ExtentReportManager.LogPass("Read user Details view success");
        }



        [Test]
        public void WorkSpace_Create_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user Workspace Create view start");
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
            ExtentReportManager.LogPass("Read user Workspace Create view success");
        }

        [Test]
        public void WorkSpace_Edit_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user Workspace Edit view start");
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
            ExtentReportManager.LogPass("Read user Workspace Edit view success");
        }

        [Test]
        public void WorkSpace_View_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read user Workspace Details view start");
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
            ExtentReportManager.LogPass("Read user Workspace Details view success");
        }

        //Admin user tests
        [Test]
        public void Index_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user index view start");
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
            ExtentReportManager.LogPass("Admin user index view success");
        }

        [Test]
        public void Create_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user Create view start");
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
            ExtentReportManager.LogPass("Admin user Create view success");
        }

        [Test]
        public void Edit_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user Edit view start");
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
            ExtentReportManager.LogPass("Admin user Edit view success");
        }


        [Test]
        public void Details_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user Details view start");
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
            ExtentReportManager.LogPass("Admin user Details view success");
        }


        [Test]
        public void WorkSpace_Create_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user Workspace Create view start");
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
            ExtentReportManager.LogPass("Admin user Workspace Create view success");
        }

        [Test]
        public void WorkSpace_Edit_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user Workspace Edit view start");
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
            ExtentReportManager.LogPass("Admin user Workspace Edit view success");

        }

        [Test]
        public void WorkSpace_View_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin user Workspace Details view start");
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
            ExtentReportManager.LogPass("Admin user Workspace Details view success");
        }


        //Manager User tests
        [Test]
        public void Index_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user index view start");
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
            ExtentReportManager.LogPass("Manager user index view success");
        }

        [Test]
        public void Create_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user Create view start");
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
            ExtentReportManager.LogPass("Manager user Create view success");
        }

        [Test]
        public void Edit_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user Edit view start");
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
            ExtentReportManager.LogPass("Manager user Edit view success");
        }


        [Test]
        public void Details_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user Details view start");
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
            ExtentReportManager.LogPass("Manager user Details view success");
        }

        [Test]
        public void WorkSpace_Create_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user Workspace Create view start");
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
            ExtentReportManager.LogPass("Manager user Workspace Create view success");
        }


        [Test]
        public void WorkSpace_Edit_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user Workspace Edit view start");
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
            ExtentReportManager.LogPass("Manager user Workspace Edit view success");
        }

        [Test]
        public void WorkSpace_View_ManagerUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager user Workspace Details view start");
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
            ExtentReportManager.LogPass("Manager user Workspace Details view success");
        }


        //Associate User tests
        [Test]
        public void Index_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user index view start");
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
            ExtentReportManager.LogPass("Associate user index view success");
        }

        [Test]
        public void Create_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user Create view start");
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
            ExtentReportManager.LogPass("Associate user Create view success");
        }

        [Test]
        public void Edit_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user Edit view start");
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
            ExtentReportManager.LogPass("Associate user Edit view success");
        }


        [Test]
        public void Details_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user Details view start");
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
            ExtentReportManager.LogPass("Associate user Details view success");
        }

        [Test]
        public void WorkSpace_Create_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user Workspace Create view start");
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
            ExtentReportManager.LogPass("Associate user Workspace Create view success");
        }


        [Test]
        public void WorkSpace_Edit_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user Workspace Edit view start");
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
            ExtentReportManager.LogPass("Associate user Workspace Edit view success");
        }

        [Test]
        public void WorkSpace_View_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate user Workspace Details view start");
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
            ExtentReportManager.LogPass("Associate user Workspace Details view success");
        }


        //No User tests
        [Test]
        public void Index_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user index view start");
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
            ExtentReportManager.LogPass("No user index view success");

        }

        [Test]
        public void Create_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user Create view start");
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
            ExtentReportManager.LogPass("No user Create view success");
        }

        [Test]
        public void Edit_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user Edit view start");
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
            ExtentReportManager.LogPass("No user Edit view success");
        }


        [Test]
        public void Details_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user Details view start");
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
            ExtentReportManager.LogPass("No user Details view success");
        }

        [Test]
        public void WorkSpace_Create_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user Workspace Create view start");
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
            ExtentReportManager.LogPass("No user Workspace Create view success");
        }


        [Test]
        public void WorkSpace_Edit_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user Workspace Edit view start");
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
            ExtentReportManager.LogPass("No user Workspace Edit view success");

        }

        [Test]
        public void WorkSpace_View_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No user Workspace Details view start");
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
            ExtentReportManager.LogPass("No user Workspace Details view success");
        }


        //User management view test

        [Test]
        public void UserManagement_AdminUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Admin User management view start");
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
            ExtentReportManager.LogPass("Admin User management  view success");
        }

        [Test]
        public void UserManagement_LeadUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Lead User management view start");
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
            ExtentReportManager.LogPass("Lead User management  view success");
        }

        [Test]
        public void UserManagement_ReadOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Read User management view start");
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
            ExtentReportManager.LogPass("Read User management  view success");
        }

        [Test]
        public void UserManagement_ManagerOnlyUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Manager User management view start");
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
            ExtentReportManager.LogPass("Manager User management  view success");
        }

        [Test]
        public void UserManagement_AssociateUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("Associate User management view start");
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
            ExtentReportManager.LogPass("Associate User management view success");
        }

        [Test]
        public void UserManagement_NoUserPermission_ReturnsViewResult()
        {
            ExtentReportManager.LogInfo("No User management view start");
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
            ExtentReportManager.LogPass("No User management view success");

        }

    }
}
