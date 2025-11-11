using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace AccountsPayable.Tests.Controllers
{
    internal class SessionTest
    {
        [TestFixture]
        [Category("Long Tests")]
        public class SessionLogOutTest : BaseTest
        {
            [SetUp]
            public void SetUp()
            {
                ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
            }
            [Test]

            public async Task SessionTimeOut60Min()
            {
                ExtentReportManager.LogInfo("session TimeOut test 60 minutes started");
                var page = await _browser.NewPageAsync();
                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");

                //Wait for session to expire 
                await Task.Delay(3600000);

                //Reload or go to another session-dependent page
                await page.ClickAsync("button.view-button");

                // Validate that session is expired
                var isOnLoginPage = page.Url.Contains("/LogIn");

                Assert.IsTrue(isOnLoginPage);
                ExtentReportManager.LogPass("session TimeOut test 60 minutes success");
            }

            [Test]

            public async Task SessionTimeOut20Min()
            {
                ExtentReportManager.LogInfo("session TimeOut test 20 minutes started");
                var page = await _browser.NewPageAsync();
                await page.GotoAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Access/LogIn");
                await page.FillAsync("[name='username']", "User.Manager");
                await page.FillAsync("#password", "manageradmin");
                await page.ClickAsync("button[type='submit']"); // Clicks the submit button
                await page.WaitForURLAsync("http://testaccountpayableapp.us-east-1.elasticbeanstalk.com/Main/Index");

                //Wait for session to expire 
                await Task.Delay(1200000);

                //Reload or go to another session-dependent page
                await page.ClickAsync("button.view-button");

                // Validate that session is expired
                var isOnLoginPage = page.Url.Contains("/LogIn");

                Assert.IsFalse(isOnLoginPage);
                ExtentReportManager.LogPass("session TimeOut test 20 minutes success");

            }
        }
    }
}
