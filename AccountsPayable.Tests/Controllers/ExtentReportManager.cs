using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

public static class ExtentReportManager
{
    private static ExtentReports _extent;
    private static ExtentSparkReporter _htmlReporter;
    private static ExtentTest _currentTest;

    public static void InitReport()
    {
        DateTime datime = DateTime.Now;
        var formatedTime = datime.ToString("MM-dd-yyyy HHmmss");
        var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtentReports", $"TestReport {formatedTime}.html");
        Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

        _htmlReporter = new ExtentSparkReporter(reportPath);
        _extent = new ExtentReports();
        _extent.AttachReporter(_htmlReporter);
    }

    public static void CreateTest(string testName)
    {
        _currentTest = _extent.CreateTest(testName);
    }

    public static void LogInfo(string message)
    {
        _currentTest?.Info(message);
    }

    public static void LogPass(string message)
    {
        _currentTest?.Pass(message);
    }

    public static void LogFail(string message)
    {
        _currentTest?.Fail(message);
    }

    public static void FlushReport()
    {
        _extent?.Flush();
    }
}
