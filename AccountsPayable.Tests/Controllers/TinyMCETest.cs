using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PlaywrightTest
    {
        [Test]
        public async Task VerifyTinyMCEInitializationSettings()
        {
            // Initialize playwright synchronously
            IPlaywright playwright = await InitializePlaywright();
            IBrowser browser = null;

            try
            {
                if (playwright != null)
                {
                    // Launch browser
                    browser = await playwright.Chromium?.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

                    if (browser != null)
                    {
                        var page = await browser.NewPageAsync();

                        if (page != null)
                        {
                            // Navigate to your webpage containing the TinyMCE editors
                            await page.GotoAsync("http://ap-test.us-east-1.elasticbeanstalk.com/");

                            // Check read-only TinyMCE editor initialization settings
                            IElementHandle readOnlyEditor = await page.QuerySelectorAsync("textarea.TinyEditorReadOnly");
                            string readOnlyAttribute = readOnlyEditor != null ? await readOnlyEditor.EvaluateAsync<string>("el => el.getAttribute('readonly')") : null;
                            Console.WriteLine("Read-only attribute: " + readOnlyAttribute);

                            // Check editable TinyMCE editor initialization settings
                            IElementHandle editableEditor = await page.QuerySelectorAsync("textarea.TinyEditor");
                            string toolbar = editableEditor != null ? await editableEditor.EvaluateAsync<string>("el => el.getAttribute('aria-label')") : null;
                            Console.WriteLine("Toolbar attribute: " + toolbar);
                        }
                    }
                }
            }
            finally
            {
                // Close the browser
                if (browser != null)
                {
                    await browser.CloseAsync();
                }
            }
        }

        // Helper method to initialize Playwright
        private async Task<IPlaywright> InitializePlaywright()
        {
            return await Microsoft.Playwright.Playwright.CreateAsync();
        }
    }
}
