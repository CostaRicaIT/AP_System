using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.Playwright;

public class BaseTest
{
    protected IBrowser _browser;
    protected IPage Page;
    protected IBrowserContext Context;
    protected IPlaywright Playwright;

    [SetUp]
    public async Task Setup()
    {
        ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);

        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        Context = await _browser.NewContextAsync();
        Page = await Context.NewPageAsync();

        ExtentReportManager.LogInfo("Browser launched and page initialized");
    }

    [TearDown]
    public async Task TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message;

        if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            ExtentReportManager.LogFail(message);
        }

        await _browser.CloseAsync();
        Playwright.Dispose();

        ExtentReportManager.LogInfo("Browser closed and Playwright disposed");
    }
}
[SetUpFixture]
public class TestSuiteSetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        ExtentReportManager.InitReport();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        ExtentReportManager.FlushReport();
    }
}
