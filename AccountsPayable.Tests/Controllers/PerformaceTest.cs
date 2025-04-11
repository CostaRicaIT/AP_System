using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
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
            int AvgLoadTime;

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
            public async Task Dashboard_StressTestSimulatingMultipleUsers()
            {
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(DashBoard_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                TestContext.WriteLine($"Login average load time: {AvgLoadTime} ms");
            }

            private async Task DashBoard_AverageLoadTime(int userId)
            {
                var page = await _browser.NewPageAsync();
                try
                {
                    var startTime = DateTime.Now;
                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    var loadTime = DateTime.Now - startTime;

                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

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
            public async Task CreateTemplate_SimulatingMultipleUsers()
            {
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(CreateTemplate_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                TestContext.WriteLine($" Template Create view average load time: {AvgLoadTime} ms");
            }

            private async Task CreateTemplate_AverageLoadTime(int userId)
            {
                var page = await _browser.NewPageAsync();
                try
                {

                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    var startTime = DateTime.Now;
                    await page.ClickAsync("button.btn-primary");
                    var loadTime = DateTime.Now - startTime;
                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

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
            public async Task EditTemplate_SimulatingMultipleUsers()
            {
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(EditTemplate_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                TestContext.WriteLine($" Template Edit view average load time: {AvgLoadTime} ms");
            }

            private async Task EditTemplate_AverageLoadTime(int userId)
            {
                var page = await _browser.NewPageAsync();
                try
                {

                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    var startTime = DateTime.Now;
                    await page.ClickAsync("button.edit-button");
                    var loadTime = DateTime.Now - startTime;
                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;
                        
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
        }
    }
}
