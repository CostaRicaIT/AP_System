using System;
using System.Collections.Generic;
using System.Linq;
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
                    var startTime = DateTime.Now;
                    await page.GotoAsync("http://ap-test.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Read");
                    await page.FillAsync("#password", "password");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://ap-test.us-east-1.elasticbeanstalk.com/Main/Index");
                    var loadTime = DateTime.Now - startTime;
                    await page.WaitForSelectorAsync(".spinner.hidden", new PageWaitForSelectorOptions { State = WaitForSelectorState.Hidden });
                    TestContext.WriteLine($"Page loaded in {loadTime.Seconds} s");

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    TestContext.WriteLine($"User encountered an error: {ex.Message}");
                }
                finally
                {
                    //await page.CloseAsync(); // Close the page to free resources
                }
            }
        }
    }
}
