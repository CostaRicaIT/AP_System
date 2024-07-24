using AccountsPayable.Controllers;
using AccountsPayable.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;

namespace AccountsPayable.Tests.Controllers
{

    [TestFixture]
    public class CRUDControllerTests
    {

        [Test]
        public void Create_ValidModelState_Success()
        {
            using (var scope = new TransactionScope()) // Using transaction scope to undo changes created by test
            {
                // Arrange
                var controllerType = typeof(CRUDController);
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

                var template = new TB_TEMPLATE
                {
                    TEMP_TAX_ID = "61-7676",
                    TEMP_REMIT_TO = "PO BOX 34343. CALIFORNIA, CA",
                    TEMP_SUPPLIER_NAME = "REGIONAL MOUNTAIN",
                    TEMP_VENDOR_ACCOUNT = "NA",
                    TEMP_SUPPLIER_NUMBER = "2334",
                    TEMP_SUPPLIER_SITE = "MAIN",

                    FK_TB_LEGAL_ENTITY_ID = 4,
                    TEMP_TAXPAYER_ID = "27-43635",
                    FK_TB_ORACLE_TYPE_ID = 1,
                    TEMP_ORACLE_DESCRIPTION = "NA",
                    FK_TB_ORACLE_PAY_TERMS_ID = 1,
                    TEMP_DISTRIBUTION_SET = "101.153.00.000.53101.000",
                    FK_TB_ORACLE_SOURCE_ID = 1,
                    TEMP_ORACLE_NOTES = "NA",
                    TEMP_ORACLE_INSTRUCTIONS = "NA",
                    FK_TB_APPROVER_ID = 1,
                    TEMP_APPROVER_COMMENTS = "NA",
                    TEMP_INVOICE_FORMAT = "XXXXXX XXXXX/XXXXXXXX/XXXXXXXXXXX XX/XX/XXXX (8. Patient Name/3a. Pat CNTL# (When provided) + 45. Serv.Date)",
                    TEMP_INVOICE_TYPE = "Insurrance Form #1",
                    TEMP_FOLDER = "Folder 1",
                    TEMP_PAYMENT_METHOD = "Credit card",
                    TEMP_REMIT_TOACCOUNT = "Account #",
                    TEMP_BILLING_PERIOD = "January",
                    TEMP_DISTRIBUTION_COMBINATION = "Combination 1",
                    TEMP_ACCOUNTING_DATE = "March 1st",
                    TEMP_VSU = "VSU info",
                    TEMP_W9_W8 = "W9 Info",
                    TEMP_INVOICE_NOTES = "Valid invoice",
                    TEMP_INVOICE_DESCRIPTION = "Invoice from provider X",
                    CONTACTS_CURRENT = "Current contact",
                    CONTACTS_PRIOR = "Prior Contact",
                    FK_TB_ORGANIZATION_TYPE_ID = 1,
                };
                var highlights = new TB_HIGHLIGHTS
                {
                    HIGHLIGHTS = "Remit-to Account: XXX\r\nPayment Terms: Immediate\r\nInvoice Description: Drug & Med Tests \r\nBilling Period: XXX *Please add it in Oracle if invoice is greater than $1K\r\nDistribution Set: Drug & Med Tests-Aya Compliance\r\nDistribution Combination/Description: 101.153.00.000.53101.000.0000. Aya Healthcare, Inc.QM Compliance.Default.Admin.Drug & Med Tests.Default.Default\r\nAccounting Date: XX/XX/XXXX (Will be the invoice date; however if the invoice date is from a month that already closed, the date to be selected will be the first date of the following/current month)",
                    HIGHLIGHTS_COMMENTS = "NA",
                    HIGHLIGHTS_INSTRUCTIONS = "NA",
                    HIGHLIGHTS_EXCEPTIONS = "NA",
                    HIGHLIGHTS_COMMON_ISSUES = "NA",
                    HIGHLIGHTS_SUPPLIER_AGENCY = "NA",
                    HIGHLIGHTS_TEMPLATE_COMMENTS = "Ver histórico/instrucciones en tab: Check list"
                };
                var historicRemit = new TB_HISTORIC_REMIT
                {
                    HISTORIC_REMIT_INFO = "NA"
                };
                var aliasList = new List<TB_ALIAS>
            {
                 new TB_ALIAS
                 {
                     ALIAS_NAME = "REGIONAL MOUNTAIN MED CTR"
                 }
            };
                var emailBackupList = new List<TB_EMAIL_BACKUP>
            {
                new TB_EMAIL_BACKUP
                {
                    EMAIL_BACKUP ="EMAIL Test"
                }
            };

                // Act

                var result = controllerType.GetMethod("Create", new[] { typeof(TB_TEMPLATE), typeof(TB_HIGHLIGHTS), typeof(TB_HISTORIC_REMIT), typeof(List<TB_ALIAS>), typeof(List<TB_EMAIL_BACKUP>) })
                                 .Invoke(controllerInstance, new object[] { template, highlights, historicRemit, aliasList, emailBackupList });


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

                // Validate that all ALIAS AND Emails are saved
                Assert.AreEqual(template.TB_ALIAS.Count, aliasList.Count, "Number of aliases saved");
                Assert.AreEqual(template.TB_EMAIL_BACKUP.Count, emailBackupList.Count, "Number of email backups saved");

                //Validate relation for HIGHLIGHTS
                Assert.AreEqual(template.FK_TB_HIGHLIGHTS_ID, highlights.HIGHLIGHTS_ID, "TB_HIGHLIGHTS relationship");
                //Validate relation for HIGHLIGHTS
                Assert.AreEqual(template.FK_TB_TEMPLATE_HISTORIC_REMIT_ID, historicRemit.HISTORIC_REMIT_ID, "TB_HISTORIC_REMIT relationship");

                foreach (var aliasEntity in aliasList)
                {
                    Assert.IsTrue(template.TB_ALIAS.Contains(aliasEntity), "TB_ALIAS relationship");
                }

                foreach (var emailEntity in emailBackupList)
                {
                    Assert.IsTrue(template.TB_EMAIL_BACKUP.Contains(emailEntity), "TB_EMAIL_BACKUP relationship");
                }
            }
        }

        [Test]
        public void Edit_ValidModelState_Success()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
                var controllerInstance = Activator.CreateInstance(controllerType);

                // Get the private or internal 'db' property
                var dbProperty = controllerType.GetProperty("db", BindingFlags.Instance | BindingFlags.NonPublic);

                // Ensure that the property is not null before attempting to set its value
                if (dbProperty != null)
                {
                    // Mock the database context
                    var dbContextMock = new Mock<AccountsPayableTestProdEntities>(); // Replace YourDbContext with the actual type of your DbContext
                    dbProperty.SetValue(controllerInstance, dbContextMock.Object);
                }

                // Create necessary entities and data for the Edit method
                var template = new TB_TEMPLATE
                {
                    TEMP_ID = 67,
                    TEMP_TAX_ID = "61-7676",
                    TEMP_REMIT_TO = "PO BOX 34343. CALIFORNIA, CA",
                    TEMP_SUPPLIER_NAME = "REGIONAL MOUNTAIN",
                    TEMP_VENDOR_ACCOUNT = "NA",
                    TEMP_SUPPLIER_NUMBER = "2334",
                    TEMP_SUPPLIER_SITE = "MAIN",

                    FK_TB_LEGAL_ENTITY_ID = 4,
                    TEMP_TAXPAYER_ID = "27-43635",
                    FK_TB_ORACLE_TYPE_ID = 1,
                    TEMP_ORACLE_DESCRIPTION = "NA",
                    FK_TB_ORACLE_PAY_TERMS_ID = 1,
                    TEMP_DISTRIBUTION_SET = "101.153.00.000.53101.000",
                    FK_TB_ORACLE_SOURCE_ID = 1,
                    TEMP_ORACLE_NOTES = "NA",
                    TEMP_ORACLE_INSTRUCTIONS = "NA",
                    FK_TB_APPROVER_ID = 1,
                    TEMP_APPROVER_COMMENTS = "NA",
                    TEMP_INVOICE_FORMAT = "XXXXXX XXXXX/XXXXXXXX/XXXXXXXXXXX XX/XX/XXXX (8. Patient Name/3a. Pat CNTL# (When provided) + 45. Serv.Date)",
                    TEMP_INVOICE_TYPE = "Insurrance Form #1",
                    TEMP_FOLDER = "Folder 1",
                    TEMP_PAYMENT_METHOD = "Credit card",
                    TEMP_REMIT_TOACCOUNT = "Account #",
                    TEMP_BILLING_PERIOD = "January",
                    TEMP_DISTRIBUTION_COMBINATION = "Combination 1",
                    TEMP_ACCOUNTING_DATE = "March 1st",
                    TEMP_VSU = "VSU info",
                    TEMP_W9_W8 = "W9 Info",
                    TEMP_INVOICE_NOTES = "Valid invoice",
                    TEMP_INVOICE_DESCRIPTION = "Invoice from provider X",
                    CONTACTS_CURRENT = "Current contact",
                    CONTACTS_PRIOR = "Prior Contact",
                    FK_TB_ORGANIZATION_TYPE_ID = 1,
                };
                var highlights = new TB_HIGHLIGHTS
                {
                    HIGHLIGHTS = "Remit-to Account: XXX\r\nPayment Terms: Immediate\r\nInvoice Description: Drug & Med Tests \r\nBilling Period: XXX *Please add it in Oracle if invoice is greater than $1K\r\nDistribution Set: Drug & Med Tests-Aya Compliance\r\nDistribution Combination/Description: 101.153.00.000.53101.000.0000. Aya Healthcare, Inc.QM Compliance.Default.Admin.Drug & Med Tests.Default.Default\r\nAccounting Date: XX/XX/XXXX (Will be the invoice date; however if the invoice date is from a month that already closed, the date to be selected will be the first date of the following/current month)",
                    HIGHLIGHTS_COMMENTS = "Comment",
                    HIGHLIGHTS_INSTRUCTIONS = "NA",
                    HIGHLIGHTS_EXCEPTIONS = "NA",
                    HIGHLIGHTS_COMMON_ISSUES = "NA",
                    HIGHLIGHTS_SUPPLIER_AGENCY = "NA",
                    HIGHLIGHTS_TEMPLATE_COMMENTS = "Ver histórico/instrucciones en tab: Check list"
                };
                var historicRemit = new TB_HISTORIC_REMIT
                {
                    HISTORIC_REMIT_INFO = "NA1"
                };
                var aliasList = new List<TB_ALIAS>
            {
                 new TB_ALIAS
                 {
                     ALIAS_NAME = "REGIONAL CLOUD "
                 },
                    new TB_ALIAS
                 {
                     ALIAS_NAME = "REGIONAL "
                 }
            };
                var emailBackupList = new List<TB_EMAIL_BACKUP>
            {
                new TB_EMAIL_BACKUP
                {
                    EMAIL_BACKUP ="EMAIL Test"
                }
            };

                // Act
                var result = controllerType.GetMethod("Edit", new[] { typeof(TB_TEMPLATE), typeof(TB_HIGHLIGHTS), typeof(TB_HISTORIC_REMIT), typeof(List<TB_ALIAS>), typeof(List<TB_EMAIL_BACKUP>) })
                                  .Invoke(controllerInstance, new object[] { template, highlights, historicRemit, aliasList, emailBackupList });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                Assert.IsNotNull(jsonResult);

                var responseData = jsonResult.Data;
                Assert.IsNotNull(responseData);

                Assert.IsTrue((bool)responseData.GetType().GetProperty("success")?.GetValue(responseData));


                //Validate that relation of highlights is updated
                Assert.AreEqual(template.FK_TB_HIGHLIGHTS_ID, highlights.HIGHLIGHTS_ID, "TB_HIGHLIGHTS relationship");
                //Validate that relation of  historic remit is updated
                Assert.AreEqual(template.FK_TB_TEMPLATE_HISTORIC_REMIT_ID, historicRemit.HISTORIC_REMIT_ID, "TB_HISTORIC_REMIT relationship");

                //Validate that relationship of ALIAS IS updated
                Assert.AreEqual(template.FK_TB_TEMPLATE_ALIAS_ID, aliasList.LastOrDefault()?.ALIAS_ID, "TB_TEMPLATE_ALIAS relationship");
                //Validate that relationship of ALIAS IS updated
                Assert.AreEqual(template.FK_TB_EMAIL_BACKUP_ID, emailBackupList.LastOrDefault()?.EMAIL_BACKUP_ID, "TB_TEMPLATE_EMAIL_BACKUP relationship");

            }
        }

        [Test]
        public void Create_ValidModelState_newLegalEntity_new_Approver_Success_new_OT()
        {
            using (var scope = new TransactionScope()) // Using transaction scope to undo changes created by test
            {
                // Arrange
                var controllerType = typeof(CRUDController);
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

                // Adding new legal entity and approver to be used on insert

                var newLegalEntity = "Access Test";
                var newLegalEntityResult = controllerType.GetMethod("AddLegalEntity", new[] { typeof(string) })
                           .Invoke(controllerInstance, new object[] { newLegalEntity });

                var jsonResultnewLegalEntity = newLegalEntityResult as JsonResult;
                var responseDatanewLegalEntity = jsonResultnewLegalEntity.Data;
                int newLegalEntityId = (int)responseDatanewLegalEntity.GetType().GetProperty("id")?.GetValue(responseDatanewLegalEntity);

                var newApprover = "Zamora, Fernando";
                var newApproverResult = controllerType.GetMethod("AddApprover", new[] { typeof(string) })
                           .Invoke(controllerInstance, new object[] { newApprover });

                var jsonResultnewApprover = newApproverResult as JsonResult;
                var responseDatanewApprover = jsonResultnewApprover.Data;
                int newApproverId = (int)responseDatanewApprover.GetType().GetProperty("id")?.GetValue(responseDatanewApprover);

                var newOT = "Partnership - Test";
                var newOTResult = controllerType.GetMethod("AddOrganizationType", new[] { typeof(string) })
                           .Invoke(controllerInstance, new object[] { newOT });

                var jsonResultnewOT = newOTResult as JsonResult;
                var responseDatanewOT = jsonResultnewOT.Data;
                int newOTId = (int)responseDatanewOT.GetType().GetProperty("id")?.GetValue(responseDatanewOT);


                var template = new TB_TEMPLATE
                {
                    TEMP_TAX_ID = "61-7676",
                    TEMP_REMIT_TO = "PO BOX 34343. CALIFORNIA, CA",
                    TEMP_SUPPLIER_NAME = "REGIONAL MOUNTAIN",
                    TEMP_VENDOR_ACCOUNT = "NA",
                    TEMP_SUPPLIER_NUMBER = "2334",
                    TEMP_SUPPLIER_SITE = "MAIN",

                    FK_TB_LEGAL_ENTITY_ID = newLegalEntityId,
                    TEMP_TAXPAYER_ID = "27-43635",
                    FK_TB_ORACLE_TYPE_ID = 1,
                    TEMP_ORACLE_DESCRIPTION = "NA",
                    FK_TB_ORACLE_PAY_TERMS_ID = 1,
                    TEMP_DISTRIBUTION_SET = "101.153.00.000.53101.000",
                    FK_TB_ORACLE_SOURCE_ID = 1,
                    TEMP_ORACLE_NOTES = "NA",
                    TEMP_ORACLE_INSTRUCTIONS = "NA",
                    FK_TB_APPROVER_ID = newApproverId,
                    TEMP_APPROVER_COMMENTS = "NA",
                    TEMP_INVOICE_FORMAT = "XXXXXX XXXXX/XXXXXXXX/XXXXXXXXXXX XX/XX/XXXX (8. Patient Name/3a. Pat CNTL# (When provided) + 45. Serv.Date)",
                    TEMP_INVOICE_TYPE = "Insurrance Form #1",
                    TEMP_FOLDER = "Folder 1",
                    TEMP_PAYMENT_METHOD = "Credit card",
                    TEMP_REMIT_TOACCOUNT = "Account #",
                    TEMP_BILLING_PERIOD = "January",
                    TEMP_DISTRIBUTION_COMBINATION = "Combination 1",
                    TEMP_ACCOUNTING_DATE = "March 1st",
                    TEMP_VSU = "VSU info",
                    TEMP_W9_W8 = "W9 Info",
                    TEMP_INVOICE_NOTES = "Valid invoice",
                    TEMP_INVOICE_DESCRIPTION = "Invoice from provider X",
                    CONTACTS_CURRENT = "Current contact",
                    CONTACTS_PRIOR = "Prior Contact",
                    FK_TB_ORGANIZATION_TYPE_ID = newOTId
                };
                var highlights = new TB_HIGHLIGHTS
                {
                    HIGHLIGHTS = "Remit-to Account: XXX\r\nPayment Terms: Immediate\r\nInvoice Description: Drug & Med Tests \r\nBilling Period: XXX *Please add it in Oracle if invoice is greater than $1K\r\nDistribution Set: Drug & Med Tests-Aya Compliance\r\nDistribution Combination/Description: 101.153.00.000.53101.000.0000. Aya Healthcare, Inc.QM Compliance.Default.Admin.Drug & Med Tests.Default.Default\r\nAccounting Date: XX/XX/XXXX (Will be the invoice date; however if the invoice date is from a month that already closed, the date to be selected will be the first date of the following/current month)",
                    HIGHLIGHTS_COMMENTS = "NA",
                    HIGHLIGHTS_INSTRUCTIONS = "NA",
                    HIGHLIGHTS_EXCEPTIONS = "NA",
                    HIGHLIGHTS_COMMON_ISSUES = "NA",
                    HIGHLIGHTS_SUPPLIER_AGENCY = "NA",
                    HIGHLIGHTS_TEMPLATE_COMMENTS = "Ver histórico/instrucciones en tab: Check list"
                };
                var historicRemit = new TB_HISTORIC_REMIT
                {
                    HISTORIC_REMIT_INFO = "NA"
                };
                var aliasList = new List<TB_ALIAS>
            {
                 new TB_ALIAS
                 {
                     ALIAS_NAME = "REGIONAL MOUNTAIN MED CTR"
                 }
            };
                var emailBackupList = new List<TB_EMAIL_BACKUP>
            {
                new TB_EMAIL_BACKUP
                {
                    EMAIL_BACKUP ="EMAIL Test"
                }
            };

                // Act

                var result = controllerType.GetMethod("Create", new[] { typeof(TB_TEMPLATE), typeof(TB_HIGHLIGHTS), typeof(TB_HISTORIC_REMIT), typeof(List<TB_ALIAS>), typeof(List<TB_EMAIL_BACKUP>) })
                                 .Invoke(controllerInstance, new object[] { template, highlights, historicRemit, aliasList, emailBackupList });


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

                // Validate that all ALIAS AND Emails are saved
                Assert.AreEqual(template.TB_ALIAS.Count, aliasList.Count, "Number of aliases saved");
                Assert.AreEqual(template.TB_EMAIL_BACKUP.Count, emailBackupList.Count, "Number of email backups saved");

                //Validate relation for HIGHLIGHTS
                Assert.AreEqual(template.FK_TB_HIGHLIGHTS_ID, highlights.HIGHLIGHTS_ID, "TB_HIGHLIGHTS relationship");
                //Validate relation for HIGHLIGHTS
                Assert.AreEqual(template.FK_TB_TEMPLATE_HISTORIC_REMIT_ID, historicRemit.HISTORIC_REMIT_ID, "TB_HISTORIC_REMIT relationship");

                foreach (var aliasEntity in aliasList)
                {
                    Assert.IsTrue(template.TB_ALIAS.Contains(aliasEntity), "TB_ALIAS relationship");
                }

                foreach (var emailEntity in emailBackupList)
                {
                    Assert.IsTrue(template.TB_EMAIL_BACKUP.Contains(emailEntity), "TB_EMAIL_BACKUP relationship");
                }
            }
        }

        [Test]
        public void Edit_ValidModelState_newLegalEntity_new_Approver_Success_new_OT()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
                var controllerInstance = Activator.CreateInstance(controllerType);

                // Get the private or internal 'db' property
                var dbProperty = controllerType.GetProperty("db", BindingFlags.Instance | BindingFlags.NonPublic);

                // Ensure that the property is not null before attempting to set its value
                if (dbProperty != null)
                {
                    // Mock the database context
                    var dbContextMock = new Mock<AccountsPayableTestProdEntities>(); // Replace YourDbContext with the actual type of your DbContext
                    dbProperty.SetValue(controllerInstance, dbContextMock.Object);
                }

                // Adding new legal entity and approver to be used on insert

                var newLegalEntity = "Access Test";
                var newLegalEntityResult = controllerType.GetMethod("AddLegalEntity", new[] { typeof(string) })
                           .Invoke(controllerInstance, new object[] { newLegalEntity });

                var jsonResultnewLegalEntity = newLegalEntityResult as JsonResult;
                var responseDatanewLegalEntity = jsonResultnewLegalEntity.Data;
                int newLegalEntityId = (int)responseDatanewLegalEntity.GetType().GetProperty("id")?.GetValue(responseDatanewLegalEntity);

                var newApprover = "Zamora, Fernando";
                var newApproverResult = controllerType.GetMethod("AddApprover", new[] { typeof(string) })
                           .Invoke(controllerInstance, new object[] { newApprover });

                var jsonResultnewApprover = newApproverResult as JsonResult;
                var responseDatanewApprover = jsonResultnewApprover.Data;
                int newApproverId = (int)responseDatanewApprover.GetType().GetProperty("id")?.GetValue(responseDatanewApprover);

                var newOT = "Partnership - Test";
                var newOTResult = controllerType.GetMethod("AddOrganizationType", new[] { typeof(string) })
                           .Invoke(controllerInstance, new object[] { newOT });

                var jsonResultnewOT = newOTResult as JsonResult;
                var responseDatanewOT = jsonResultnewOT.Data;
                int newOTId = (int)responseDatanewOT.GetType().GetProperty("id")?.GetValue(responseDatanewOT);


                // Create necessary entities and data for the Edit method
                var template = new TB_TEMPLATE
                {
                    TEMP_ID = 67,
                    TEMP_TAX_ID = "61-7676",
                    TEMP_REMIT_TO = "PO BOX 34343. CALIFORNIA, CA",
                    TEMP_SUPPLIER_NAME = "REGIONAL MOUNTAIN",
                    TEMP_VENDOR_ACCOUNT = "NA",
                    TEMP_SUPPLIER_NUMBER = "2334",
                    TEMP_SUPPLIER_SITE = "MAIN",
                    FK_TB_LEGAL_ENTITY_ID = newLegalEntityId,
                    TEMP_TAXPAYER_ID = "27-43635",
                    FK_TB_ORACLE_TYPE_ID = 1,
                    TEMP_ORACLE_DESCRIPTION = "NA",
                    FK_TB_ORACLE_PAY_TERMS_ID = 1,
                    TEMP_DISTRIBUTION_SET = "101.153.00.000.53101.000",
                    FK_TB_ORACLE_SOURCE_ID = 1,
                    TEMP_ORACLE_NOTES = "NA",
                    TEMP_ORACLE_INSTRUCTIONS = "NA",
                    FK_TB_APPROVER_ID = newApproverId,
                    TEMP_APPROVER_COMMENTS = "NA",
                    TEMP_INVOICE_FORMAT = "XXXXXX XXXXX/XXXXXXXX/XXXXXXXXXXX XX/XX/XXXX (8. Patient Name/3a. Pat CNTL# (When provided) + 45. Serv.Date)",
                    TEMP_INVOICE_TYPE = "Insurrance Form #1",
                    TEMP_FOLDER = "Folder 1",
                    TEMP_PAYMENT_METHOD = "Credit card",
                    TEMP_REMIT_TOACCOUNT = "Account #",
                    TEMP_BILLING_PERIOD = "January",
                    TEMP_DISTRIBUTION_COMBINATION = "Combination 1",
                    TEMP_ACCOUNTING_DATE = "March 1st",
                    TEMP_VSU = "VSU info",
                    TEMP_W9_W8 = "W9 Info",
                    TEMP_INVOICE_NOTES = "Valid invoice",
                    TEMP_INVOICE_DESCRIPTION = "Invoice from provider X",
                    CONTACTS_CURRENT = "Current contact",
                    CONTACTS_PRIOR = "Prior Contact",
                    FK_TB_ORGANIZATION_TYPE_ID = newOTId,
                };
                var highlights = new TB_HIGHLIGHTS
                {
                    HIGHLIGHTS = "Remit-to Account: XXX\r\nPayment Terms: Immediate\r\nInvoice Description: Drug & Med Tests \r\nBilling Period: XXX *Please add it in Oracle if invoice is greater than $1K\r\nDistribution Set: Drug & Med Tests-Aya Compliance\r\nDistribution Combination/Description: 101.153.00.000.53101.000.0000. Aya Healthcare, Inc.QM Compliance.Default.Admin.Drug & Med Tests.Default.Default\r\nAccounting Date: XX/XX/XXXX (Will be the invoice date; however if the invoice date is from a month that already closed, the date to be selected will be the first date of the following/current month)",
                    HIGHLIGHTS_COMMENTS = "Comment",
                    HIGHLIGHTS_INSTRUCTIONS = "NA",
                    HIGHLIGHTS_EXCEPTIONS = "NA",
                    HIGHLIGHTS_COMMON_ISSUES = "NA",
                    HIGHLIGHTS_SUPPLIER_AGENCY = "NA",
                    HIGHLIGHTS_TEMPLATE_COMMENTS = "Ver histórico/instrucciones en tab: Check list"
                };
                var historicRemit = new TB_HISTORIC_REMIT
                {
                    HISTORIC_REMIT_INFO = "NA1"
                };
                var aliasList = new List<TB_ALIAS>
            {
                 new TB_ALIAS
                 {
                     ALIAS_NAME = "REGIONAL CLOUD "
                 },
                    new TB_ALIAS
                 {
                     ALIAS_NAME = "REGIONAL "
                 }
            };
                var emailBackupList = new List<TB_EMAIL_BACKUP>
            {
                new TB_EMAIL_BACKUP
                {
                    EMAIL_BACKUP ="EMAIL Test"
                }
            };

                // Act
                var result = controllerType.GetMethod("Edit", new[] { typeof(TB_TEMPLATE), typeof(TB_HIGHLIGHTS), typeof(TB_HISTORIC_REMIT), typeof(List<TB_ALIAS>), typeof(List<TB_EMAIL_BACKUP>) })
                                  .Invoke(controllerInstance, new object[] { template, highlights, historicRemit, aliasList, emailBackupList });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                Assert.IsNotNull(jsonResult);

                var responseData = jsonResult.Data;
                Assert.IsNotNull(responseData);

                Assert.IsTrue((bool)responseData.GetType().GetProperty("success")?.GetValue(responseData));


                //Validate that relation of highlights is updated
                Assert.AreEqual(template.FK_TB_HIGHLIGHTS_ID, highlights.HIGHLIGHTS_ID, "TB_HIGHLIGHTS relationship");
                //Validate that relation of  historic remit is updated
                Assert.AreEqual(template.FK_TB_TEMPLATE_HISTORIC_REMIT_ID, historicRemit.HISTORIC_REMIT_ID, "TB_HISTORIC_REMIT relationship");

                //Validate that relationship of ALIAS IS updated
                Assert.AreEqual(template.FK_TB_TEMPLATE_ALIAS_ID, aliasList.LastOrDefault()?.ALIAS_ID, "TB_TEMPLATE_ALIAS relationship");
                //Validate that relationship of ALIAS IS updated
                Assert.AreEqual(template.FK_TB_EMAIL_BACKUP_ID, emailBackupList.LastOrDefault()?.EMAIL_BACKUP_ID, "TB_TEMPLATE_EMAIL_BACKUP relationship");

            }
        }

        [Test]
        public void Create_NewLegalEntity_Success()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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

                // Create necessary entities and data for the Edit method
                var newLegalEntity = "Access Test";

                // Act
                var result = controllerType.GetMethod("AddLegalEntity", new[] { typeof(string) })
                                          .Invoke(controllerInstance, new object[] { newLegalEntity });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                var responseData = jsonResult.Data;
                Assert.IsNotNull(jsonResult);
                Assert.AreEqual(newLegalEntity, responseData.GetType().GetProperty("name")?.GetValue(responseData));
            }
        }

        [Test]
        public void Create_NewLegalEntity_Failture()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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

                // Create necessary entities and data for the Edit method
                var newLegalEntity = "Access Personnel"; // Legal entity that already exists on DB

                // Act
                var result = controllerType.GetMethod("AddLegalEntity", new[] { typeof(string) })
                                          .Invoke(controllerInstance, new object[] { newLegalEntity });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                var responseData = jsonResult.Data;
                Assert.IsNotNull(jsonResult);
                Assert.AreEqual("Entity already exists", responseData.GetType().GetProperty("message")?.GetValue(responseData));
            }
        }

        [Test]
        public void Create_Approver_Success()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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

                // Create necessary entities and data for the Edit method
                var newApprover = "Zamora, Fernando";

                // Act
                var result = controllerType.GetMethod("AddApprover", new[] { typeof(string) })
                                          .Invoke(controllerInstance, new object[] { newApprover });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                var responseData = jsonResult.Data;
                Assert.IsNotNull(jsonResult);
                Assert.AreEqual(newApprover, responseData.GetType().GetProperty("name")?.GetValue(responseData));
            }
        }

        [Test]
        public void Create_NewApprover_Failture()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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

                // Create necessary entities and data for the Edit method
                var newApprover = "APPROVER"; // Legal entity that already exists on DB

                // Act
                var result = controllerType.GetMethod("AddApprover", new[] { typeof(string) })
                                          .Invoke(controllerInstance, new object[] { newApprover });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                var responseData = jsonResult.Data;
                Assert.IsNotNull(jsonResult);
                Assert.AreEqual("Entity already exists", responseData.GetType().GetProperty("message")?.GetValue(responseData));
            }
        }
        [Test]
        public void Create_OT_Success()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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

                // Create necessary entities and data for the Edit method
                var newOT = "Partnership - Test";

                // Act
                var result = controllerType.GetMethod("AddOrganizationType", new[] { typeof(string) })
                                          .Invoke(controllerInstance, new object[] { newOT });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                var responseData = jsonResult.Data;
                Assert.IsNotNull(jsonResult);
                Assert.AreEqual(newOT, responseData.GetType().GetProperty("name")?.GetValue(responseData));
            }
        }
        [Test]
        public void Create_NewOT_Failture()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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

                // Create necessary entities and data for the Edit method
                var newOT = "Partnership"; // Legal entity that already exists on DB

                // Act
                var result = controllerType.GetMethod("AddOrganizationType", new[] { typeof(string) })
                                          .Invoke(controllerInstance, new object[] { newOT });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                var responseData = jsonResult.Data;
                Assert.IsNotNull(jsonResult);
                Assert.AreEqual("Organization Type already exists", responseData.GetType().GetProperty("message")?.GetValue(responseData));
            }
        }
        [Test]
        public void Delete_TemplateNotDisabled_Success()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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
                int templateid = 42; // ID of a template that is not disabled
                var templateData = new TB_TEMPLATE
                {
                    TEMP_ID = templateid,
                    TEMP_ISDISABLED = 1
                };
                // Act
                var result = controllerType.GetMethod("DELETE", new[] { typeof(int?), typeof(int) })
                         .Invoke(controllerInstance, new object[] { templateid, 1 });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                Assert.IsNotNull(jsonResult);

                Assert.IsTrue((bool)jsonResult.Data.GetType().GetProperty("success")?.GetValue(jsonResult.Data));
                Assert.AreEqual(templateData.TEMP_ISDISABLED, 1);
            }

        }

        [Test]
        public void Delete_TemplateAlreadyDisabled_Failture()
        {
            // Arrange
            using (var scope = new TransactionScope())
            {
                var controllerType = typeof(CRUDController);
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
                int templateid = 41; // ID of a template that is disabled
                var templateData = new TB_TEMPLATE
                {
                    TEMP_ID = templateid,
                    TEMP_ISDISABLED = 1
                };
                // Act
                var result = controllerType.GetMethod("DELETE", new[] { typeof(int?), typeof(int) })
                         .Invoke(controllerInstance, new object[] { templateid, 1 });

                // Assert
                Assert.IsInstanceOf<JsonResult>(result);
                var jsonResult = result as JsonResult;
                Assert.IsNotNull(jsonResult);

                Assert.IsFalse((bool)jsonResult.Data.GetType().GetProperty("success")?.GetValue(jsonResult.Data));
                Assert.AreEqual(templateData.TEMP_ISDISABLED, 1);
            }
        }

        [Test]
        public void StripHtmlTags_Returns_Null_For_Null_Inputs()
        {
            // Arrange
            var controller = new CRUDController();
            string input = null;

            // Act
            var result = controller.StripHtmlTags(input);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void StripHtmlTags_Removes_P_Tags()
        {
            // Arrange
            var controller = new CRUDController();
            string input = "<p>This is a <p>test</p>.</p>";
            string expectedOutput = "This is a test.";

            // Act
            var result = controller.StripHtmlTags(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        public void StripHtmlTags_Uses_Regex_To_Remove_P_Tags()
        {
            // Arrange
            var controller = new CRUDController(); // No need to mock anything for the controller
            string input = "<p>This is a <p>test</p>.</p>";
            string expectedResult = "This is a test.";

            // Act
            var result = controller.StripHtmlTags(input);

            // Assert
            Assert.AreEqual(expectedResult, result); // Assert the result based on your expectation
        }

    }
}