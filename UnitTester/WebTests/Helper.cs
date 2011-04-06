using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;
using System.Diagnostics;

namespace UnitTester.WebTests
{
    public class Helper
    {
        public static void StopSelenium(ISelenium sel)
        {
            try
            {
                sel.Stop();
                Debug.WriteLine("Selenium Stopped");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
