using AccountsPayable.Controllers;
using AccountsPayable.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace AccountsPayable.Tests.Controllers
{
    [TestFixture]
    public class WorkspaceCRUDControllerTest
    {
        private TB_WORKSPACE workspace;
        private TB_TEMPLATE template;
        private WS_COMMENTS comments;
        private WS_LAST_ACTIONS lastActions;

        [SetUp]
        public void SetUp()
        {

            workspace = new TB_WORKSPACE()
            {
                WS_STATUS = "VALIDATED/REJECTED",
                WS_REASON = "REVISE INVOICE",
                WS_EMAIL_RECEIVED = "3/6/2025",
                WS_CREATED_DATE = "3/6/2025",
                WS_SOURCE = "OF QUEUE",
                WS_HANDLED_BY = "Susana Vega",
                WS_INVOICE_DATE = "01/31/2025",
                WS_DUE_DATE = "05/25/2025",
                WS_AMOUNT = "5000",
                WS_INVOICE_NUMBER = "45-BIERKAMP, RENDA-1/31/2025-CB0001701000"
            };

            comments = new WS_COMMENTS()
            {
               WORKSPACE_INFO = "3/14/2025-QMInvoices-REJECTED REASON: This invoice has services that should not be paid and others that should be paid"
            };

            lastActions = new WS_LAST_ACTIONS()
            {
                LAST_ACTIONS_INFO = "Rejected invoice 01/31/2025"
            };
            template = new TB_TEMPLATE
            {
                TEMP_ID = 67,
                TEMP_FOLDER = "Folder 1",
                TEMP_TAX_ID = "61-7676",
                TEMP_SUPPLIER_NAME = "REGIONAL MOUNTAIN",
                TEMP_SUPPLIER_NUMBER = "2334",
                TEMP_REMIT_TO = "PO BOX 34343. CALIFORNIA, CA",
                TEMP_SUPPLIER_SITE = "MAIN",
                TEMP_VENDOR_ACCOUNT = "NA",
                FK_TB_ORACLE_SOURCE_ID = 1,


                TEMP_INVOICE_FORMAT = "XXXXXX XXXXX/XXXXXXXX/XXXXXXXXXXX XX/XX/XXXX (8. Patient Name/3a. Pat CNTL# (When provided) + 45. Serv.Date)",
                TEMP_INVOICE_TYPE = "Insurrance Form #1",
                TEMP_INVOICE_NOTES = "Valid invoice",

                TEMP_W9_W8 = "W9 Info",
                TEMP_VSU = "VSU info",

                TEMP_PAYMENT_METHOD = "Credit card",
                TEMP_REMIT_TOACCOUNT = "Account #",
                FK_TB_ORACLE_PAY_TERMS_ID = 1,
                TEMP_INVOICE_DESCRIPTION = "Invoice from provider X",
                TEMP_BILLING_PERIOD = "January",
                TEMP_BILLING_PRERIOD_DATE = "01/12/2025",
                TEMP_DISTRIBUTION_SET = "101.153.00.000.53101.000",
                TEMP_DISTRIBUTION_COMBINATION = "Combination 1",
                TEMP_ACCOUNTING_DATE = "01/12/2025",
                FK_TB_LEGAL_ENTITY_ID = 4,
                FK_TB_ORGANIZATION_TYPE_ID = 1,
                TEMP_TAXPAYER_ID = "27-43635",
                FK_TB_ORACLE_TYPE_ID = 1,
                TEMP_ORACLE_DESCRIPTION = "NA",
                TEMP_ORACLE_NOTES = "NA",
                TEMP_ORACLE_INSTRUCTIONS = "NA",

                CONTACTS_CURRENT = "Current contact",
                CONTACTS_PRIOR = "Prior Contact",

                FK_TB_APPROVER_ID = 1,
                TEMP_APPROVER_COMMENTS = "NA",

                FK_TB_HIGHLIGHTS_ID = 1,
                FK_TB_EMAIL_BACKUP_ID = 1,
                FK_TB_TEMPLATE_ALIAS_ID = 1,
                FK_TB_TEMPLATE_HISTORIC_REMIT_ID = 1,
            };

        }

        [Test]
        public void Create_ValidModelState_Success()
        {
            using (var scope = new TransactionScope()) // Using transaction scope to undo changes created by test
            {
                // Arrange
                var controllerType = typeof(WSCrudController);
                var controllerInstance = Activator.CreateInstance(controllerType);

                // Get the private or internal 'db' property
                var dbProperty = controllerType.GetProperty("db", BindingFlags.Instance | BindingFlags.NonPublic);

                // Ensure that the property is not null before attempting to set its value
                if (dbProperty != null)
                {
                    // Mock the database context
                    var dbContextMock = new Mock<AccountsPayableTestProdEntities>();

                    // Set the mocked DbContext to the 'db' property
                    dbProperty.SetValue(controllerInstance, dbContextMock.Object);
                }

                // Mock the session state
                var sessionMock = new Mock<HttpSessionStateBase>();
                sessionMock.Setup(s => s["FullName"]).Returns("Test User");

                // Set the session state to the controller
                var controllerContextMock = new Mock<ControllerContext>();
                controllerContextMock.Setup(c => c.HttpContext.Session).Returns(sessionMock.Object);
                controllerType.GetProperty("ControllerContext").SetValue(controllerInstance, controllerContextMock.Object);


                // Act

                var result = controllerType.GetMethod("Create", new[] { typeof(TB_WORKSPACE), typeof(WS_COMMENTS), typeof(WS_LAST_ACTIONS), typeof(TB_TEMPLATE) })
                                .Invoke(controllerInstance, new object[] { workspace, comments, lastActions, template });


                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;// Cast the result to JsonResult
                                                      // Ensure the cast was successful
                Assert.IsNotNull(jsonResult);
                // Extract the data from the JsonResult
                var responseData = jsonResult.Data;
                // Perform assertions on the data
                Assert.IsNotNull(responseData);
                //Validate if the controller returns success = true
                Assert.IsTrue((bool)responseData.GetType().GetProperty("success")?.GetValue(responseData));

                Assert.AreEqual(workspace.FK_WS_COMMENTS_ID, comments.COMMENTS_ID);
                Assert.AreEqual(workspace.FK_WS_LAST_ACTIONS_ID, lastActions.LAST_ACTIONS_ID);

                
            }
        }


        [Test]
        public void Edit_ValidModelState_Success()
        {
            using (var scope = new TransactionScope()) // Using transaction scope to undo changes created by test
            {
                // Arrange
                var controllerType = typeof(WSCrudController);
                var controllerInstance = Activator.CreateInstance(controllerType);

                // Get the private or internal 'db' property
                var dbProperty = controllerType.GetProperty("db", BindingFlags.Instance | BindingFlags.NonPublic);

                // Ensure that the property is not null before attempting to set its value
                if (dbProperty != null)
                {
                    // Mock the database context
                    var dbContextMock = new Mock<AccountsPayableTestProdEntities>();

                    // Set the mocked DbContext to the 'db' property
                    dbProperty.SetValue(controllerInstance, dbContextMock.Object);
                }

                // Mock the session state
                var sessionMock = new Mock<HttpSessionStateBase>();
                sessionMock.Setup(s => s["FullName"]).Returns("Test User");

                // Set the session state to the controller
                var controllerContextMock = new Mock<ControllerContext>();
                controllerContextMock.Setup(c => c.HttpContext.Session).Returns(sessionMock.Object);
                controllerType.GetProperty("ControllerContext").SetValue(controllerInstance, controllerContextMock.Object);

                workspace.WS_ID = 21;

                // Act

                var result = controllerType.GetMethod("Edit", new[] { typeof(TB_WORKSPACE), typeof(WS_COMMENTS), typeof(WS_LAST_ACTIONS) })
                                .Invoke(controllerInstance, new object[] { workspace, comments, lastActions });


                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;// Cast the result to JsonResult
                                                      // Ensure the cast was successful
                Assert.IsNotNull(jsonResult);
                // Extract the data from the JsonResult
                var responseData = jsonResult.Data;
                // Perform assertions on the data
                Assert.IsNotNull(responseData);
                //Validate if the controller returns success = true
                Assert.IsTrue((bool)responseData.GetType().GetProperty("success")?.GetValue(responseData));

                Assert.AreEqual(workspace.FK_WS_COMMENTS_ID, comments.COMMENTS_ID);
                Assert.AreEqual(workspace.FK_WS_LAST_ACTIONS_ID, lastActions.LAST_ACTIONS_ID);


            }
        }

        [Test]
        public void Delete_TemplateNotDisabled_Success()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(WSCrudController);
                var controllerInstance = Activator.CreateInstance(controllerType);
                // Get the private or internal 'db' property
                var dbProperty = controllerType.GetProperty("db", BindingFlags.Instance | BindingFlags.NonPublic);

                // Ensure that the property is not null before attempting to set its value
                if (dbProperty != null)
                {
                    // Mock the database context
                    var dbContextMock = new Mock<AccountsPayableTestProdEntities>();
                    dbProperty.SetValue(controllerInstance, dbContextMock.Object);
                }
                int workspaceid = 21; // ID of a workspace that is not disabled
                var workspace = new TB_WORKSPACE
                {
                    WS_ID = workspaceid,
                    WS_ISDISABLED = 1
                };
                // Act
                var result = controllerType.GetMethod("DELETE", new[] { typeof(int?), typeof(int) })
                         .Invoke(controllerInstance, new object[] { workspaceid, 1 });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                Assert.IsNotNull(jsonResult);

                Assert.IsTrue((bool)jsonResult.Data.GetType().GetProperty("success")?.GetValue(jsonResult.Data));
                Assert.AreEqual(workspace.WS_ISDISABLED, 1);
            }

        }

        [Test]
        public void Delete_TemplateAlreadyDisabled_Failture()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(WSCrudController);
                var controllerInstance = Activator.CreateInstance(controllerType);
                // Get the private or internal 'db' property
                var dbProperty = controllerType.GetProperty("db", BindingFlags.Instance | BindingFlags.NonPublic);

                // Ensure that the property is not null before attempting to set its value
                if (dbProperty != null)
                {
                    // Mock the database context
                    var dbContextMock = new Mock<AccountsPayableTestProdEntities>();
                    dbProperty.SetValue(controllerInstance, dbContextMock.Object);
                }
                int workspaceid = 13; // ID of a workspace that is disabled
                var workspace = new TB_WORKSPACE
                {
                    WS_ID = workspaceid,
                    WS_ISDISABLED = 1
                };
                // Act
                var result = controllerType.GetMethod("DELETE", new[] { typeof(int?), typeof(int) })
                         .Invoke(controllerInstance, new object[] { workspaceid, 1 });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                Assert.IsNotNull(jsonResult);

                Assert.IsFalse((bool)jsonResult.Data.GetType().GetProperty("success")?.GetValue(jsonResult.Data));
                Assert.AreEqual(workspace.WS_ISDISABLED, 1);
            }
        }
    }
}
