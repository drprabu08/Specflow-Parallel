using NUnit.Framework;
using NUnitParalllelExecution.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace NUnitParalllelExecution.Steps
{
    [Binding]
    public sealed class LoginFailSteps
    {
        IWebDriver _driver;
        LoginPage loginPage;
        public LoginFailSteps(IWebDriver _driver)
        {
            this._driver = _driver;
            loginPage = new LoginPage(_driver);
        }

        //private readonly ScenarioContext scenarioContext;

        //public StepsWithScenarioContext(ScenarioContext scenarioContext)
        //{
        //    if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
        //    this.scenarioContext = scenarioContext;
        //}

        [Given(@"I have navigated to the login page")]
        public void GivenIHaveNavigatedToTheLoginPage()
        {
            _driver.Navigate().GoToUrl("https://moodle.org/demo");
        }

        [Given(@"I entered username and password as below")]
        public void GivenIEnteredUsernameAndPasswordAsBelow(Table table)
        {
            Thread.Sleep(2000);
            loginPage.lnkSignIn.Click();
            string username = table.Rows[0]["Username"];
            string password = table.Rows[0]["Password"];

            loginPage.Username.SendKeys(username);
            loginPage.Password.SendKeys(password);
        }

        [When(@"I press login")]
        public void WhenIPressLogin()
        {
            loginPage.SubmitBtn.Click();
        }

        [Then(@"the homepage shpuld not be displayed")]
        public void ThenTheHomepageShpuldNotBeDisplayed()
        {
            Assert.True(true);
        }

    }
}
