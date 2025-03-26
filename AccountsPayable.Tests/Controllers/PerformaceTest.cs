using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace AccountsPayable.Tests.Controllers
{
    internal class PerformaceTest
    {
        [TestFixture]
        public class StressTests
        {
            private IPlaywright _playwright;
            private IBrowser _browser;

            [OneTimeSetUp]
            public async Task Setup()
            {
                _playwright = await Playwright.CreateAsync();
                _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            }

            [OneTimeTearDown]
            public async Task TearDown()
            {
                await _browser.CloseAsync();
                _playwright.Dispose();
            }

            [Test]
            public async Task StressTestSimulatingMultipleUsers()
            {
                const int userCount = 50; // Number of simulated users
                var tasks = new List<Task>();

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(SimulateUser(i));
                }

                await Task.WhenAll(tasks); // Wait for all tasks to complete
            }

            private async Task SimulateUser(int userId)
            {
                var page = await _browser.NewPageAsync();
                try
                {
                    page.SetDefaultTimeout(900000);
                    var startTime = DateTime.Now;
                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Lead");
                    await page.FillAsync("#password", "password");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    var loadTime = DateTime.Now - startTime;
                    TestContext.WriteLine($"{loadTime.Seconds}");

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {
                    await page.CloseAsync(); // Close the page to free resources
                }
            }
            [Test]
            public async Task EditStressTestSimulatingMultipleUsers()
            {
                const int userCount = 50; // Number of simulated users
                var tasks = new List<Task>();

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(SimulateEdit(i+7)); //Adding 7 as the templates on db start on index 8 due to deleted data
                }

                await Task.WhenAll(tasks); // Wait for all tasks to complete
            }

            private async Task SimulateEdit(int userId)
            {
                string UserId = userId.ToString();
                string EditUrl = $"http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Edit/{userId}";
                var page = await _browser.NewPageAsync();
                try
                {
                    page.SetDefaultTimeout(900000);
                    var startTime = DateTime.Now;
                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Lead");
                    await page.FillAsync("#password", "password");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    await page.GotoAsync(EditUrl);
                    await page.FillAsync("#TEMP_SUPPLIER_NAME", $"Supplier Name {UserId} ");
                    await page.ClickAsync("#btn-update");
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    var loadTime = DateTime.Now - startTime;
                    TestContext.WriteLine($"{loadTime.Seconds}");


                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
                    Assert.Fail( ex.ToString() );
                }
                finally
                {
                    await page.CloseAsync(); // Close the page to free resources
                }
            }
        }
    }
}
