using AventStack.ExtentReports;
using BoDi;
using NUnit.Framework;
using NUnitParalllelExecution.Extent;
using NUnitParalllelExecution.ExtentReport;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]
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

        [BeforeFeature]
        public static void InitFeature(FeatureContext featureContext)
        {
            ExtentTestManager.CreateParentTest(featureContext.FeatureInfo.Title);
        }
        

        [BeforeScenario]
        public void Setup(ScenarioContext scenarioContext)
        {
            _driver = new ChromeDriver();
            objectContainer.RegisterInstanceAs<IWebDriver>(_driver);

            ExtentTestManager.CreateTest(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AddStep(ScenarioContext stepContext)
        {
            var stepType = stepContext.StepContext.StepInfo.StepDefinitionType.ToString();
            
            PropertyInfo pinfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pinfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(stepContext, null);

            if(stepContext.TestError == null)
            {
                ExtentTestManager.CreateTestStep(stepType, stepContext.StepContext.StepInfo.Text);
            }
            else if (stepContext.TestError != null)
            {

                ExtentTestManager.CreateFailedTestStep(stepType, stepContext.StepContext.StepInfo.Text, stepContext.TestError.Message, ExtentTestManager.CapturScreenshotANdReturnModel(stepContext.StepContext.StepInfo.Text, _driver));
            }
        }
        

        [AfterScenario]
        public void Cleanup()
        {
            _driver.Quit();

            ExtentManager.Instance.Flush();
            
        }

        

    }
}
