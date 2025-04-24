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
        public class SessionLogOutTest
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

            public async Task SessionTimeOut60Min()
            {
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
            }

            [Test]

            public async Task SessionTimeOut20Min()
            {
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

            }
        }
    }
}
