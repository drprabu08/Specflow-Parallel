using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using NUnitParalllelExecution.Extent;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TechTalk.SpecFlow;

namespace NUnitParalllelExecution.ExtentReport
{
    class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string featureName)
        {

            _parentTest = ExtentManager.Instance.CreateTest<Feature>(featureName);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string scenarioName)
        {

            _childTest = _parentTest.CreateNode<Scenario>(scenarioName);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void CreateTestStep(string stepType, string stepName)
        {
            if(stepType == "Given")
                _childTest.CreateNode<Given>(stepName);
            else if (stepType == "Then")
                _childTest.CreateNode<Then>(stepName);
            else if (stepType == "When")
                _childTest.CreateNode<When>(stepName);
            
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void CreateFailedTestStep(string stepType, string stepName, string ex, MediaEntityModelProvider mediaEntity)
        {
            if (stepType == "Given")
                _childTest.CreateNode<Given>(stepName).Fail(ex, mediaEntity);
            else if (stepType == "Then")
                _childTest.CreateNode<Then>(stepName).Fail(ex, mediaEntity);
            else if (stepType == "When")
                _childTest.CreateNode<When>(stepName).Fail(ex, mediaEntity);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MediaEntityModelProvider CapturScreenshotANdReturnModel(string name, IWebDriver _driver)
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }
    }
}
