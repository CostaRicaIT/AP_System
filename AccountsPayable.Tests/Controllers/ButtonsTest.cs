using System;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AccountsPayable.Tests.Controllers
{
    internal class ButtonsTest
    {
        [TestFixture]
        public class ButtonTest : BaseTest
        {
            [Test]
            public async Task ButtonTest_Lead()
            {
                var page = await _browser.NewPageAsync();
                var URL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn/";
                var EditURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Edit/";
                var DetailsURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Details/";
                var CreateURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Create";
                var IndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index";
                var WorkSpaceCreateURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Create/";
                var WorkSpaceIndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index";
                var WorkSpaceViewURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Details/";
                var WorkSpaceEditURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Edit/";


                try
                {
                    ExtentReportManager.LogInfo("Button test for Lead user start ");
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Lead");
                    await page.FillAsync("#password", "password");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button                                             
                    await page.WaitForURLAsync(IndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(70), EditURL);
                    ExtentReportManager.LogPass("Edit button test success");

                    // Test View button
                    ExtentReportManager.LogInfo("View button test Lead user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);
                    ExtentReportManager.LogPass("View button test success");

                    // Test Create
                    ExtentReportManager.LogInfo("Create button test Lead user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.btn-primary");
                    Assert.AreEqual(page.Url, CreateURL);
                    ExtentReportManager.LogPass("Create button test success");

                    // Test Workspace create
                    ExtentReportManager.LogInfo("Workspace Create button test Lead user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.Workspace-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(77), WorkSpaceCreateURL);
                    ExtentReportManager.LogPass("Workspace Create button test success");

                    //Test Workspace view
                    ExtentReportManager.LogInfo("Workspace View button test Lead user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);
                    ExtentReportManager.LogPass("Workspace View button test success");

                    //Test Workspace Edit
                    ExtentReportManager.LogInfo("Workspace Edit button test Lead user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(75), WorkSpaceEditURL);
                    ExtentReportManager.LogPass("Workspace Edit button test success");
                }
                catch (Exception ex)
                {
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {
                    await page.CloseAsync();
                }
            }


            [Test]
            public async Task ButtonTest_Associate()
            {
                var page = await _browser.NewPageAsync();
                var DetailsURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Details/";
                var IndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index";
                var URL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn/";
                var WorkSpaceCreateURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Create/";
                var WorkSpaceIndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index";
                var WorkSpaceViewURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Details/";
                var WorkSpaceEditURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Edit/";

                try
                {
                    ExtentReportManager.LogInfo("Button test for Associate user start ");
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Associate");
                    await page.FillAsync("#password", "associateadmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync(IndexURL);

                    // Test View button
                    ExtentReportManager.LogInfo("View button test Associate user");
                    await page.ClickAsync("button.view-button");
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);
                    ExtentReportManager.LogPass("View button test success");

                    // Test Workspace Create
                    ExtentReportManager.LogInfo("Workspace Create button test Associate user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.Workspace-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(77), WorkSpaceCreateURL);
                    ExtentReportManager.LogPass("Workspace Create button test success");


                    //Test Workspace view
                    ExtentReportManager.LogInfo("Workspace View button test Associate user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);
                    ExtentReportManager.LogPass("Workspace View button test success");

                    //Test Workspace Edit
                    ExtentReportManager.LogInfo("Workspace Edit button test Associate user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(75), WorkSpaceEditURL);
                    ExtentReportManager.LogPass("Workspace Edit button test success");

                }
                catch (Exception ex)
                {
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {
                    await page.CloseAsync();
                }
            }

            [Test]
            public async Task ButtonTest_Read()
            {
                var page = await _browser.NewPageAsync();
                var DetailsURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Details/";
                var IndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index";
                var URL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn/";
                var WorkSpaceIndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index";
                var WorkSpaceViewURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Details/";

                try
                {
                    ExtentReportManager.LogInfo("Button test for Read user start ");
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Read");
                    await page.FillAsync("#password", "readadmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button


                    // Test View button
                    ExtentReportManager.LogInfo("View button test Read user");
                    await page.WaitForURLAsync(IndexURL);
                    await page.ClickAsync("button.view-button");
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);
                    ExtentReportManager.LogPass("View button test success");


                    //Test Workspace view
                    ExtentReportManager.LogInfo("Workspace View button test Read user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);
                    ExtentReportManager.LogPass("Workspace View button test success");
                }
                catch (Exception ex)
                {
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {
                    await page.CloseAsync();
                }
            }
            [Test]
            public async Task ButtonTest_Manager()
            {
                var page = await _browser.NewPageAsync();
                var EditURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Edit/";
                var DetailsURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Details/";
                var CreateURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Create";
                var IndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index";
                var URL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn/";
                var WorkSpaceCreateURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Create/";
                var WorkSpaceIndexURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index";
                var WorkSpaceViewURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Details/";
                var WorkSpaceEditURL = "http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Edit/";
                try
                {
                    ExtentReportManager.LogInfo("Button test for Manager user start ");
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button

                    ExtentReportManager.LogInfo("Edit button test Manager user");
                    await page.WaitForURLAsync(IndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(70), EditURL);
                    ExtentReportManager.LogPass("Edit button test success");
                    // Test View button
                    ExtentReportManager.LogInfo("View button test Manager user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);
                    ExtentReportManager.LogPass("View button test success");

                    // Test Create
                    ExtentReportManager.LogInfo("Create button test Manager user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.btn-primary");
                    Assert.AreEqual(page.Url, CreateURL);
                    ExtentReportManager.LogPass("Create button test success");

                    // Test Delete button
                    ExtentReportManager.LogInfo("Delete button test Manager user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.erase-button");

                    // Test Delete button
                    await page.GotoAsync(IndexURL);

                    // Setup dialog event handling before clicking the button
                    var dialogCompletion = new TaskCompletionSource<string>();

                    page.Dialog += async (_, dialog) =>
                    {
                        dialogCompletion.TrySetResult(dialog.Message);
                        await dialog.DismissAsync();
                    };

                    // Click delete button
                    await page.ClickAsync("button.erase-button");

                    // Wait for dialog message
                    var alertMessage = await dialogCompletion.Task;

                    Assert.AreEqual("Are you sure you want to delete this Template?", alertMessage);
                    ExtentReportManager.LogPass("Delete button test success");


                    // Test Workspace
                    ExtentReportManager.LogInfo("Workspace Create button test Manager user");
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.Workspace-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(77), WorkSpaceCreateURL);
                    ExtentReportManager.LogPass("Workspace Create button test success");

                    //Test Workspace view
                    ExtentReportManager.LogInfo("Workspace View button test Manager user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);
                    ExtentReportManager.LogPass("Workspace View button test success");

                    //Test Workspace Edit
                    ExtentReportManager.LogInfo("Workspace Edit button test Manager user");
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(75), WorkSpaceEditURL);
                    ExtentReportManager.LogPass("Workspace Edit button test success");



                    // Test Delete button
                    ExtentReportManager.LogInfo("Workspace Delete button test Manager user");
                    await page.GotoAsync(WorkSpaceIndexURL);

                    // Setup dialog event handling before clicking the button
                    var dialogCompletionWS = new TaskCompletionSource<string>();

                    page.Dialog += async (_, dialog) =>
                    {
                        dialogCompletionWS.TrySetResult(dialog.Message);
                        await dialog.DismissAsync();
                    };

                    // Click delete button
                    await page.ClickAsync("button.erase-button");

                    // Wait for dialog message
                    var alertMessageWS = await dialogCompletionWS.Task;

                    Assert.AreEqual("Are you sure you want to delete this Workspace?", alertMessageWS);
                    ExtentReportManager.LogPass("Workspace Delete button test success");

                }
                catch (Exception ex)
                {
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {
                    await page.CloseAsync();
                }
            }

        }
    }
}

