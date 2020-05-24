using AventStack.ExtentReports;
using BoDi;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnitParalllelExecution.Extent;
using NUnitParalllelExecution.ExtentReport;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace NUnitParalllelExecution
{
    
    [Binding]
    class Hooks
    {
        IWebDriver _driver;
        private readonly IObjectContainer objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        //[BeforeFeature]
        //public void BeforeRun()
        //{
        //    ExtentTestManager.CreateParentTest(GetType().Name);
        //}
        //[AfterFeature]
        //public void AfterRun()
        //{
        //    ExtentManager.Instance.Flush();
        //}
        [BeforeScenario]
        public void Setup()
        {
            _driver = new ChromeDriver();
            objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            ExtentTestManager.CreateParentTest(GetType().Name);
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        

        [AfterScenario]
        public void Cleanup()
        {
            _driver.Quit();

            ExtentManager.Instance.Flush();
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
        }
    }
}
