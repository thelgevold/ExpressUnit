using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;

namespace ExpressUnit
{
    public class SeleniumManager
    {
        public static ISelenium InitializeWebTest()
        {
           // ISelenium selenium = new DefaultSelenium("localhost", 4444, "*iexplore", startPage);
            ISelenium selenium = new DefaultSelenium("localhost", 4444, "*iexplore", "http://localhost:4444");
            selenium.Start();

          //  selenium.Open(startPage);
            //selenium.WaitForPageToLoad("30000");

            return selenium;
        }
    }
}
