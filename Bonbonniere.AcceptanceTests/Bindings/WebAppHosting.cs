using Bonbonniere.AcceptanceTests.Tools.PageAccess;
using Bonbonniere.AcceptanceTests.Tools.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;

namespace Bonbonniere.AcceptanceTests.Bindings
{
    [Binding]
    public class WebAppHosting
    {
        private RemoteWebDriver _webDriver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            //if (ScenarioContext.Current.ScenarioInfo.Tags.Contains("useconfig")) { }
            _webDriver = WebDriverHelper.CurrentDriver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            WebDriverHelper.QuitDriver();
        }

        [Given(@"I am on the site home page")]
        public void GivenIOpenTheWebsite()
        {
            _webDriver.Navigate().GoToUrl(AppConfig.WebsiteRootUrl);
        }

        [When(@"^I open menu ""(.*)""$")]
        public void WhenIOpenMenu(string menuTitle)
        {
            var elMenu = _webDriver.FindElement(By.Id("navigation-menu")).FindElement(By.XPath(string.Format("./ul/li/a[text()='{0}']", menuTitle)));
            elMenu.Click();
        }

        [When(@"I fill in")]
        public void WhenIFillIn(Table table)
        {
            var inputs = _webDriver.FindElementsByXPath("//input|//textarea|//select")
                ?? Enumerable.Empty<IWebElement>();

            var curRow = table.Rows[0];
            foreach (var name in table.Header)
            {
                var curName = name;
                var curCellData = curRow[curName];
                var input = inputs.FirstOrDefault(x => x.GetAttribute("name") == curName);
                Assert.IsNotNull(input, "Unable to locate <input> name '{0}'", name);

                var inputType = input.GetAttribute("type");
                switch (inputType)
                {
                    case "radio":
                        var radios = inputs.Where(
                            x =>
                            x.GetAttribute("type") == "radio" &&
                            x.GetAttribute("name") == curName);
                        var radio = radios.FirstOrDefault(x => x.GetAttribute("value") == curCellData);
                        if (radio == null)
                        {
                            foreach (var r in radios)
                            {
                                if (r.FindElement(By.XPath("./parent::*")).Text == curCellData)//eg: <label><input name="Gender" type="radio">Male</label>
                                {
                                    radio = r;
                                    break;
                                }
                            }
                        }
                        Assert.IsNotNull(radio, "Unable to locate radio name '{0}' value '{1}'", curName, curCellData);
                        radio.Click();
                        break;
                    case "checkbox":
                        var chkValues = curCellData.Split('^');
                        var chks = inputs.Where(
                            x =>
                            x.GetAttribute("type") == "checkbox" &&
                            x.GetAttribute("name") == curName);
                        foreach(var chk in chks)
                        {
                            if (chkValues.Contains(chk.GetAttribute("value")) || 
                                chkValues.Contains(chk.FindElement(By.XPath("./parent::*")).Text))
                            {
                                if (!chk.Selected)
                                    chk.Click();
                            }
                            else
                            {
                                if (chk.Selected)
                                    chk.Click();
                            }
                        }
                        break;
                    default:
                        if (string.Equals(input.TagName, "select", StringComparison.OrdinalIgnoreCase))
                        {
                            SelectElement select = new SelectElement(input);
                            if (select.IsMultiple)
                            {
                                select.DeselectAll();
                                curCellData.Split('^').ToList().ForEach(x => select.SelectByText(x));
                            }
                            else
                                select.SelectByText(curCellData);
                        }
                        else
                        {
                            input.SendKeys(curRow[curName]);
                        }
                        break;
                }
            }
        }

        [When(@"I hit ""(.*)""")]
        public void WhenIHit(string buttonText)
        {
            var button = _webDriver.FindElementByXPath(string.Format("(//input[@type='submit'][@value='{0}']|input[@type='button'][@value='{0}']|//button[text()='{0}'])", buttonText));
            Assert.IsNotNull(button, string.Format("Could not find a button with matching text '{0}'", buttonText));
            button.Click();
        }

        [When(@"I follow ""(.*)""")]
        public void WhenIFollow(string linkText)
        {
            var link = _webDriver.FindElementByLinkText(linkText);
            Assert.IsNotNull(link, string.Format("Could not find an anchor with matching text '{0}'", linkText));
            link.Click();
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
            Assert.AreEqual(table.ToString(), BuildTableFromPage().ToString());
        }

        private Table BuildTableFromPage()
        {
            var html_table = _webDriver.FindElement(By.TagName("table"));
            var html_header = html_table.FindElements(By.XPath("./thead/tr/th"));
            var html_dataRows = html_table.FindElements(By.XPath("./tbody/tr"));

            var table = new Table(html_header.Select(x => x.Text).ToArray());
            foreach (var html_row in html_dataRows)
            {
                var tds = html_row.FindElements(By.TagName("td"));
                table.AddRow(tds.Select(p => p.Text).ToArray());
            }

            return table;
        }
    }
}
