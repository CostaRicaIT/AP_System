using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AccountsPayable.Tests.Controllers
{
    [TestFixture]
    internal class CRUDPerformanceTests : BaseTest
    {
        int AvgLoadTime;

       

        [Test]
        public async Task EditStressTestSimulatingMultipleUsers()
        {
            ExtentReportManager.LogInfo("Edit StressTest 50 users start");
            const int userCount = 50; // Number of simulated users
            var tasks = new List<Task>();
            AvgLoadTime = 0;

            for (int i = 0; i < userCount; i++)
            {
                tasks.Add(SimulateEdit(i + 7)); //Adding 7 as the templates on db start on index 8 due to deleted data
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
            ExtentReportManager.LogPass($"Template average edit time: {AvgLoadTime} seconds");
        }

        private async Task SimulateEdit(int userId)
        {
            string UserId = userId.ToString();
            string EditUrl = $"http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Edit/{userId}";
            var page = await _browser.NewPageAsync();
            page.SetDefaultTimeout(50000);
            try
            {

                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com//Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                await page.GotoAsync(EditUrl);
                var startTime = DateTime.Now;
                await page.FillAsync("#TEMP_SUPPLIER_NAME", $"Performance test ID: {UserId}");
                await page.ClickAsync("#btn-update");
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                var loadTime = DateTime.Now - startTime;
                AvgLoadTime = AvgLoadTime + loadTime.Seconds;


            }
            catch (Exception ex)
            {
                // Log any errors that occur
                ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
            }
            finally
            {
                await page.CloseAsync(); // Close the page to free resources
            }
        }

        [Test]
        public async Task CreateStressTestSimulatingMultipleUsers()
        {
            ExtentReportManager.LogInfo("Create template StressTest 50 users start");
            const int userCount = 50; // Number of simulated users
            var tasks = new List<Task>();
            AvgLoadTime = 0;

            for (int i = 0; i < userCount; i++)
            {
                tasks.Add(SimulateCreate());
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
            ExtentReportManager.LogPass($"Average create time: {AvgLoadTime} seconds");
        }

        private async Task SimulateCreate()
        {


            string CreateUrl = $"http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Create/";
            var page = await _browser.NewPageAsync();
            page.SetDefaultTimeout(50000);
            try
            {

                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com//Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                await page.GotoAsync(CreateUrl);
                var startTime = DateTime.Now;
                await page.FillAsync("#form-SupName", $"Performance test Create");
                await page.ClickAsync("#form-submit");
                await page.ClickAsync("#NoAddAlias_Emailbtn");
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                var loadTime = DateTime.Now - startTime;
                AvgLoadTime = AvgLoadTime + loadTime.Seconds;


            }
            catch (Exception ex)
            {
                // Log any errors that occur
                ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
            }
            finally
            {
                await page.CloseAsync(); // Close the page to free resources
            }
        }

        [Test]
        public async Task DeleteStressTestSimulatingMultipleUsers()
        {
            ExtentReportManager.LogInfo("Delete StressTest 50 users start");
            const int userCount = 50; // Number of simulated users
            var tasks = new List<Task>();
            AvgLoadTime = 0;

            for (int i = 0; i < userCount; i++)
            {
                await SimulateDelete();
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
            ExtentReportManager.LogPass($"Average delete time: {AvgLoadTime} ms");
        }

        private async Task SimulateDelete()
        {
            var page = await _browser.NewPageAsync();
            page.SetDefaultTimeout(50000);
            try
            {

                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com//Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                var startTime = DateTime.Now;

                // Setup dialog event handling before clicking the button
                var dialogCompletion = new TaskCompletionSource<string>();

                page.Dialog += async (_, dialog) =>
                {
                    dialogCompletion.TrySetResult(dialog.Message);
                    await dialog.AcceptAsync();
                };
                await page.ClickAsync("button.erase-button");
                // Wait for dialog message
                var alertMessage = await dialogCompletion.Task;
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                var loadTime = DateTime.Now - startTime;
                AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;


            }
            catch (Exception ex)
            {
                // Log any errors that occur
                ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
            }
            finally
            {
                await page.CloseAsync(); // Close the page to free resources
            }
        }

        [Test]
        public async Task CreateWorkspaceStressTestSimulatingMultipleUsers()
        {
            ExtentReportManager.LogInfo("Workspace Create StressTest 50 users start");
            const int userCount = 50; // Number of simulated users
            var tasks = new List<Task>();
            AvgLoadTime = 0;

            for (int i = 0; i < userCount; i++)
            {
                tasks.Add(SimulateCreateWorkspace(i + 7));
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
            ExtentReportManager.LogPass($"Workspace average create time: {AvgLoadTime} seconds");
        }

        private async Task SimulateCreateWorkspace(int id)
        {


            string CreateUrl = $"http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Create/{id}";
            var page = await _browser.NewPageAsync();
            page.SetDefaultTimeout(50000);
            try
            {

                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com//Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                await page.GotoAsync(CreateUrl);
                var startTime = DateTime.Now;
                await page.FillAsync("#form-Status", $"Performance test Create");
                await page.ClickAsync("#btn-save");
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index");
                var loadTime = DateTime.Now - startTime;
                AvgLoadTime = AvgLoadTime + loadTime.Seconds;


            }
            catch (Exception ex)
            {
                // Log any errors that occur
                ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
            }
            finally
            {
                await page.CloseAsync(); // Close the page to free resources
            }
        }


        [Test]
        public async Task DeleteWorkspaceStressTestSimulatingMultipleUsers()
        {
            ExtentReportManager.LogInfo("Delete Create StressTest 50 users start");
            const int userCount = 50; // Number of simulated users
            var tasks = new List<Task>();
            AvgLoadTime = 0;

            for (int i = 0; i < userCount; i++)
            {
                //tasks.Add(SimulateDelete());
                await SimulateDeleteWorspace();
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
            ExtentReportManager.LogPass($"Workspace average delete time: {AvgLoadTime} ms");
        }

        private async Task SimulateDeleteWorspace()
        {
            var page = await _browser.NewPageAsync();
            page.SetDefaultTimeout(100000);
            try
            {

                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com//Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index");
                var startTime = DateTime.Now;

                // Setup dialog event handling before clicking the button
                var dialogCompletion = new TaskCompletionSource<string>();

                page.Dialog += async (_, dialog) =>
                {
                    dialogCompletion.TrySetResult(dialog.Message);
                    await dialog.AcceptAsync();
                };
                await page.ClickAsync("button.erase-button");
                // Wait for dialog message
                var alertMessage = await dialogCompletion.Task;
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Index");
                var loadTime = DateTime.Now - startTime;
                AvgLoadTime = AvgLoadTime + loadTime.Milliseconds;


            }
            catch (Exception ex)
            {
                // Log any errors that occur
                ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
            }
            finally
            {
                await page.CloseAsync(); // Close the page to free resources
            }
        }

        [Test]
        public async Task EditWorkspaceStressTestSimulatingMultipleUsers()
        {
            ExtentReportManager.LogInfo("Workspace Edit StressTest 50 users start");
            const int userCount = 50; // Number of simulated users
            var tasks = new List<Task>();
            AvgLoadTime = 0;

            for (int i = 0; i < userCount; i++)
            {
                tasks.Add(SimulateEditWorkspace(i + 40));
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            AvgLoadTime = AvgLoadTime / 50; //Get average of load time using 50 users
            ExtentReportManager.LogPass($"Workspace average edit time: {AvgLoadTime} seconds");
        }

        private async Task SimulateEditWorkspace(int id)
        {


            string EditUrl = $"http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Workspace/Edit/{id}";
            var page = await _browser.NewPageAsync();
            page.SetDefaultTimeout(50000);
            try
            {

                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com//Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");
                await page.GotoAsync(EditUrl);
                var startTime = DateTime.Now;
                await page.FillAsync("#WS_STATUS", $"Performance test Edit");
                await page.ClickAsync("#btn-update");
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/WorkSpace/Index");
                var loadTime = DateTime.Now - startTime;
                AvgLoadTime = AvgLoadTime + loadTime.Seconds;


            }
            catch (Exception ex)
            {
                // Log any errors that occur
                ExtentReportManager.LogFail($"User encountered an error: {ex.Message}");
            }
            finally
            {
                await page.CloseAsync(); // Close the page to free resources
            }
        }
    }
}
