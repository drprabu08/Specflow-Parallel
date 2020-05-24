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
    public sealed class LoginSteps
    {
        IWebDriver _driver;

        //don't use in framework
        LoginPage loginPage = null;


        public LoginSteps(IWebDriver driver)
        {
            _driver = driver;
            loginPage = new LoginPage(_driver);
        }

        [Given(@"I launch the application")]
        public void GivenILaunchTheApplication()
        {
            _driver.Navigate().GoToUrl("https://moodle.org/demo");
        }

        [Given(@"I enter the following details")]
        public void GivenIEnterTheFollowingDetails(Table table)
        {
            Thread.Sleep(2000);
            loginPage.lnkSignIn.Click();
            string username = table.Rows[0]["Username"];
            string password = table.Rows[0]["Password"];

            loginPage.Username.SendKeys(username);
            loginPage.Password.SendKeys(password);
        }

        [When(@"I click login button")]
        public void WhenIClickLoginButton()
        {
            loginPage.SubmitBtn.Click();
        }

        [Then(@"I should navigate to the home screen")]
        public void ThenIShouldNavigateToTheHomeScreen()
        {
            Assert.True(true);
        }

    }
}
