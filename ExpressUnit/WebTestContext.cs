using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;
using System.Threading;

namespace ExpressUnit
{
    public class WebTestContext : IDisposable
    {
        private static object locker = new object();

        public ISelenium Selenium
        {
            get;
            set;
        }

        public WebTestContext()
        {
            lock (locker)
            {
                ExpressUnitConfigurationSection config = (ExpressUnitConfigurationSection)System.Configuration.ConfigurationManager.GetSection("ExpressUnitConfiguration");

                Selenium = new DefaultSelenium(config.WebTestSettings.SeleniumHost, config.WebTestSettings.SeleniumPort, config.WebTestSettings.Browser, config.WebTestSettings.StartUrl);

                Selenium.Start();

                Selenium.Open(config.WebTestSettings.StartUrl);
            }

        }

        public void Dispose()
        {
            Selenium.Stop();
        }
    }
}
