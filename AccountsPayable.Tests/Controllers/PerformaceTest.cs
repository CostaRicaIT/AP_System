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
        public class StressTests : BaseTest
        {

            int AvgLoadTime;

        

            [Test]
            public async Task Dashboard_StressTestSimulatingMultipleUsers()
            {
                ExtentReportManager.LogInfo("Template Dashboard StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(DashBoard_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($"Login average load time: {AvgLoadTime} ms");
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
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
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
                ExtentReportManager.LogInfo("Template Create StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(CreateTemplate_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($" Template Create view average load time: {AvgLoadTime} ms");
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
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
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
                ExtentReportManager.LogInfo("Template Edit StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(EditTemplate_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($" Template Edit view average load time: {AvgLoadTime} ms");
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
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {

                    await page.CloseAsync(); // Close the page to free resources
                }
            }

            [Test]
            public async Task ViewTemplate_SimulatingMultipleUsers()
            {
                ExtentReportManager.LogPass($" Template view average load time: {AvgLoadTime} ms");

                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(ViewTemplate_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($" Template view average load time: {AvgLoadTime} ms");
            }

            private async Task ViewTemplate_AverageLoadTime(int userId)
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
                    await page.ClickAsync("button.view-button");
                    var loadTime = DateTime.Now - startTime;
                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {

                    await page.CloseAsync(); // Close the page to free resources
                }
            }

            [Test]
            public async Task Workspace_Dashboard_StressTestSimulatingMultipleUsers()
            {
                ExtentReportManager.LogInfo("Workspace Dashboard StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(Workspace_DashBoard_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($"WorkSpace dashboard average load time: {AvgLoadTime} ms");
            }

            private async Task Workspace_DashBoard_AverageLoadTime(int userId)
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
                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/WorkSpace/Index");
                    var loadTime = DateTime.Now - startTime;

                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {

                    await page.CloseAsync(); // Close the page to free resources
                }
            }

            [Test]
            public async Task CreateWorkspace_SimulatingMultipleUsers()
            {
                ExtentReportManager.LogInfo("Workspace Create StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(CreateWorkspace_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($" Workspace create view average load time: {AvgLoadTime} ms");
            }

            private async Task CreateWorkspace_AverageLoadTime(int userId)
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
                    await page.ClickAsync("button.Workspace-button");
                    var loadTime = DateTime.Now - startTime;
                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {

                    await page.CloseAsync(); // Close the page to free resources
                }
            }

            [Test]
            public async Task ViewWorkspace_SimulatingMultipleUsers()
            {
                ExtentReportManager.LogInfo("Workspace View StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(ViewWorkspace_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($" Workspace view average load time: {AvgLoadTime} ms");
            }

            private async Task ViewWorkspace_AverageLoadTime(int userId)
            {
                var page = await _browser.NewPageAsync();
                try
                {

                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index");
                    var startTime = DateTime.Now;
                    await page.ClickAsync("button.view-button");
                    var loadTime = DateTime.Now - startTime;
                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
                    Assert.Fail(ex.ToString());
                }
                finally
                {

                    await page.CloseAsync(); // Close the page to free resources
                }
            }

            [Test]
            public async Task EditWorkspace_SimulatingMultipleUsers()
            {
                ExtentReportManager.LogInfo("Workspace Edit StressTest 50 users start");
                const int userCount = 50; // Number of simulated users

                var tasks = new List<Task>();
                AvgLoadTime = 0;

                for (int i = 0; i < userCount; i++)
                {
                    tasks.Add(EditWorkspace_AverageLoadTime(i));
                }


                await Task.WhenAll(tasks); // Wait for all tasks to complete
                AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
                ExtentReportManager.LogPass($" Workspace Edit view average load time: {AvgLoadTime} ms");
            }

            private async Task EditWorkspace_AverageLoadTime(int userId)
            {
                var page = await _browser.NewPageAsync();
                try
                {

                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                    await page.FillAsync("[name='username']", "User.Manager");
                    await page.FillAsync("#password", "manageradmin");
                    await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                    await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                    await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index");
                    var startTime = DateTime.Now;
                    await page.ClickAsync("button.edit-button");
                    var loadTime = DateTime.Now - startTime;
                    AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;

                }
                catch (Exception ex)
                {
                    // Log any errors that occur
                    ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
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
