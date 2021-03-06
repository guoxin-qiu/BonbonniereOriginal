﻿using System;
using System.Configuration;
using System.Linq;

namespace Bonbonniere.AcceptanceTests.Tools.Settings
{
    public static class AppConfig
    {
        public static string WebsiteRootUrl { get { return GetValueByKey("WebsiteRootUrl"); } }
        public static string LogOnUrl { get { return GetValueByKey("LogOnUrl"); } }
        public static string WebsitePostfix { get { return GetValueByKey("WebsitePostfix"); } }
        public static BrowserType Browser
        {
            get
            {
                var browser = BrowserType.Chrome;
                Enum.TryParse(GetValueByKey("Browser"), out browser);
                return browser;
            }
        }
        public static string BrowserLanguage { get { return GetValueByKey("BrowserLanguage"); } }


        private static string GetValueByKey(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                return ConfigurationManager.AppSettings[key];
            }
            else
            {
                throw new Exception(string.Format("{0} can't be found in App.config", key));
            }
        }
    }
}
