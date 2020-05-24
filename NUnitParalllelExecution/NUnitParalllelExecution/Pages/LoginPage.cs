using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitParalllelExecution.Pages
{
    class LoginPage
    {
        IWebDriver webDriver { get; }

        public LoginPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }


        public IWebElement lnkSignIn => webDriver.FindElement(By.XPath("//a[text()='Log in']"));
        public IWebElement Username => webDriver.FindElement(By.Id("username"));
        public IWebElement Password => webDriver.FindElement(By.Id("password"));

        public IWebElement SubmitBtn => webDriver.FindElement(By.Id("loginbtn"));

        public void SwitchFrame()
        {
            webDriver.SwitchTo().Frame("iframe");
        }

    }
}
