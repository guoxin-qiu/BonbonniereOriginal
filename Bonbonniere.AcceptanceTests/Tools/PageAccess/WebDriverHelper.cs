﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bonbonniere.AcceptanceTests.Tools.PageAccess
{
    public class WebDriverHelper
    {
        private static BrowserType _browserType = BrowserType.Chrome;
        private const string _language = "zh-cn";//zh-cn, en-us

        private static IWebDriver _currentDriver;
        private static List<string> _openedWindowHandles = new List<string>();

        public static IWebDriver CurrentDriver
        {
            get
            {
                if (_currentDriver != null)
                    return _currentDriver;

                var driverFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools\\BrowserDrivers");
                switch (_browserType)
                {
                    case BrowserType.Chrome:
                        ChromeOptions chromeOpts = new ChromeOptions();
                        chromeOpts.AddArguments("disable-infobars");//close the infobar that chrome is being controlled by automated test
                        chromeOpts.AddArguments("--start-maximized");
                        chromeOpts.AddArguments("--lang=" + _language);
                        _currentDriver = new ChromeDriver(driverFolder, chromeOpts);
                        break;
                    case BrowserType.IE:
                        var ieOpts = new InternetExplorerOptions
                        {
                            IgnoreZoomLevel = true,
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            EnablePersistentHover = false
                        };
                        _currentDriver = new InternetExplorerDriver(driverFolder, ieOpts);
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