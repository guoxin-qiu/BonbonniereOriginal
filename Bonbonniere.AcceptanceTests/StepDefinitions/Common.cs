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
            _webDriver.FindElement(By.Name("btn-" + btnName.Replace(" ",""))).Click();
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

        [Then(@"I should see grid with")]
        public void ThenIShouldSeeGridWith(Table table)
        {
            var expectRowCount = table.RowCount;
            var htable = _webDriver.FindElement(By.TagName("table"));
            var htheadtr = htable.FindElement(By.TagName("thead")).FindElement(By.TagName("tr"));
            var htbodytrs = htable.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

            var actualRowCount = htbodytrs.Count;
            Assert.AreEqual(expectRowCount,actualRowCount);

            var htheadtds = htheadtr.FindElements(By.TagName("th"));
            var expectColumnsCount = table.Rows[0].Keys.Count;
            Assert.AreEqual(expectColumnsCount, htheadtds.Count);

            for (int i = 0; i < table.Rows[0].Keys.Count; i++)
            {
                Assert.AreEqual(table.Rows[0].Keys.ToArray()[i], htheadtds[i].Text);
            }

            for (int i = 0; i < table.RowCount; i++)
            {
                var tableRow = table.Rows[i];
                var hRow = htbodytrs[i].FindElements(By.TagName("td"));
                for (int j = 0; j < expectColumnsCount; j++)
                {
                    Assert.AreEqual(tableRow[j], hRow[j].Text);
                }
            }
        }
    }
}
