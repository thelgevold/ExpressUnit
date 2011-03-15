using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnitModel;
using ExpressUnit;

using SeleniumHelper;
using XPathItUp;
using System.Threading;
using Selenium;

namespace UnitTester.WebTests
{
    [TestClass]
    public class SampleWebTest
    {
        [WebTest]
        public void SampleTest1()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest2()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest3()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }


        [WebTest]
        public void SampleTest4()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest5()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest6()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest7()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }


        [WebTest]
        public void SampleTest8()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }


        [WebTest]
        public void SampleTest9()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest10()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest11()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }


        [WebTest]
        public void SampleTest12()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest13()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest14()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }

        [WebTest]
        public void SampleTest15()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }


        [WebTest]
        public void SampleTest16()
        {
            using (WebTestContext ctx = new WebTestContext())
            {
                ctx.Selenium.Open("Default.aspx");

                Sleep(ctx.Selenium);
            }
        }


        private void Sleep(ISelenium sel)
        {
            Random rnd = new Random(45);
            //Thread.Sleep(rnd.Next(1000,6000));

            int[] buffer = new int[]{1,2};

            long a = 0;
            int index = 1;
            for (int i = 0; i < 20; i++)
            {
                index = rnd.Next(2, 4);
                sel.Open("Default" + (index).ToString() + ".aspx");
                sel.WaitForPageToLoad("30000");

                Thread.Sleep(1000);
                
            }

        }
    }
}
