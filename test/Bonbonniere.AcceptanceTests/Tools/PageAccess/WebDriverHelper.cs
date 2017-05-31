using Bonbonniere.AcceptanceTests.Tools.Settings;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace Bonbonniere.AcceptanceTests.Tools.PageAccess
{
    public class WebDriverHelper
    {
        private static BrowserType _browserType = AppConfig.Browser;
        private static string _language = AppConfig.BrowserLanguage;

        private static RemoteWebDriver _currentDriver;
        private static List<string> _openedWindowHandles = new List<string>();

        public static RemoteWebDriver CurrentDriver
        {
            get
            {
                if (_currentDriver != null)
                    return _currentDriver;

                switch (_browserType)
                {
                    case BrowserType.Chrome:
                        ChromeOptions chromeOpts = new ChromeOptions();
                        chromeOpts.AddArguments("disable-infobars");//close the infobar that chrome is being controlled by automated test
                        chromeOpts.AddArguments("--start-maximized");
                        chromeOpts.AddArguments("--lang=" + _language);
                        _currentDriver = new ChromeDriver(chromeOpts);
                        break;
                    case BrowserType.IE:
                        var ieOpts = new InternetExplorerOptions
                        {
                            IgnoreZoomLevel = true,
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            EnablePersistentHover = false
                        };
                        _currentDriver = new InternetExplorerDriver(ieOpts);
                        break;
                }

                //_currentDriver.Manage().Window.Maximize();

                if (_openedWindowHandles == null)
                {
                    _openedWindowHandles = new List<string>();
                }
                else
                {
                    _openedWindowHandles.Clear();
                }
                _openedWindowHandles.Add(_currentDriver.CurrentWindowHandle);

                return _currentDriver;
            }
        }

        public static bool IsDriverRunning
        {
            get { return _currentDriver != null; }
        }

        public static void QuitDriver()
        {
            if (_currentDriver != null)
            {
                try
                {
                    _currentDriver.Close();
                }
                catch { }

                try
                {
                    _currentDriver.Quit();
                }
                catch { }

                _currentDriver = null;
                _openedWindowHandles = null;
            }
        }
    }
}
