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
        public class ButtonTest
        {
            private IPlaywright _playwright;
            private IBrowser _browser;

            [OneTimeSetUp]
            public async Task SetUp()
            {
                _playwright = await Playwright.CreateAsync();
                _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            }
            [OneTimeTearDown]
            public async Task TearDown()
            {
                await _browser.CloseAsync();
                _playwright?.Dispose();
            }

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
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Lead");
                    await page.FillAsync("#password", "password");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    // Test Edit button
                    await page.WaitForURLAsync(IndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(70), EditURL);

                    // Test View button
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);

                    // Test Create
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.btn-primary");
                    Assert.AreEqual(page.Url, CreateURL);

                    // Test Workspace create
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.Workspace-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(77), WorkSpaceCreateURL);

                    //Test Workspace view
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);

                    //Test Workspace Edit
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(75), WorkSpaceEditURL);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
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
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Associate");
                    await page.FillAsync("#password", "associateadmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync(IndexURL);

                    // Test View button
                    await page.ClickAsync("button.view-button");
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);

                    // Test Workspace
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.Workspace-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(77), WorkSpaceCreateURL);


                    //Test Workspace view
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);

                    //Test Workspace Edit
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(75), WorkSpaceEditURL);

                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
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
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Read");
                    await page.FillAsync("#password", "readadmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button


                    // Test View button
                    await page.WaitForURLAsync(IndexURL);
                    await page.ClickAsync("button.view-button");
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);



                    //Test Workspace view
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
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
                    await page.GotoAsync(URL);
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    // Test Edit button
                    await page.WaitForURLAsync(IndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(70), EditURL);

                    // Test View button
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(73), DetailsURL);

                    // Test Create
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.btn-primary");
                    Assert.AreEqual(page.Url, CreateURL);

                    // Test Delete button
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


                    // Test Workspace
                    await page.GotoAsync(IndexURL);
                    await page.ClickAsync("button.Workspace-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(77), WorkSpaceCreateURL);

                    //Test Workspace view
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.view-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(78), WorkSpaceViewURL);

                    //Test Workspace Edit
                    await page.GotoAsync(WorkSpaceIndexURL);
                    await page.ClickAsync("button.edit-button");
                    //Remove id from current URL to perform validation
                    Assert.AreEqual(page.Url.Remove(75), WorkSpaceEditURL);



                    // Test Delete button
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
                    var alertMessageWS = await dialogCompletion.Task;

                    Assert.AreEqual("Are you sure you want to delete this Workspace?", alertMessageWS);

                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
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
