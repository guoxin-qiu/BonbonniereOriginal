using Bonbonniere.AcceptanceTests.Tools.PageAccess;
using Bonbonniere.AcceptanceTests.Tools.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Bonbonniere.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Common
    {
        private IWebDriver _webDriver;

        public Common()
        {
            _webDriver = WebDriverHelper.CurrentDriver;
        }

        [Given(@"I am on the site home page")]
        public void GivenIOpenTheWebsite()
        {
            _webDriver.Navigate().GoToUrl(AppConfig.WebsiteRootUrl);
        }

        [When(@"^I open menu ""(.*)""$")]
        public void WhenIOpenMenu(string menuTitle)
        {
            var elMenu = _webDriver.FindElement(By.Id("navigation-menu")).FindElement(By.XPath("./ul/li/a[text()='" + menuTitle + "']"));
            elMenu.Click();
        }

        [When(@"^I populate form with")]
        public void WhenIPopulateFormWith(Table table)
        {
            var row = table.Rows[0];
            foreach(var key in row.Keys)
            {
                _webDriver.FindElement(By.Name(key)).SendKeys(row[key]);
            }
        }

        [When(@"^I press ""(.*)"" button")]
        public void WhenIPressRegisterButton(string btnName)
        {
            _webDriver.FindElement(By.Name("btn-" + btnName)).Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Then(@"^I should see page ""(.*)""")]
        public void ThenIShouldSeePage(string pageName)
        {
            Assert.AreEqual(pageName + AppConfig.WebsitePostfix, _webDriver.Title);
        }

        [Then(@"^I should see the error message ""(.*)""")]
        public void ThenIShouldSeeTheErrorMessage(string errorMsg)
        {
            var errMsgList = _webDriver.FindElements(By.ClassName("field-validation-error"))
                .Where(fe => !string.IsNullOrEmpty(fe.Text))
                .Select(fe => fe.Text);
            Assert.AreEqual(errorMsg, string.Join("", errMsgList));
        }

        [Then(@"I should see ""(.*)"" on page")]
        public void ThenIShouldSeeOnPage(string text)
        {
            Assert.IsTrue(_webDriver.PageSource.Contains(text));
        }

    }
}
