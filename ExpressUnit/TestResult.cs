using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnit
{
    public class TestResult
    {
        private string testName;
        private Exception ex;
        private bool passed;
        public TestResult()
        {
            
        }

        public string TestName
        {
            get
            {
                return testName;
            }
            set
            {
                testName = value;
            }
        }

        public bool Passed
        {
            get
            {
                return passed;
            }
            set
            {
                passed = value;
            }
        }

        public string ResultText
        {
            get
            {
                string msg = string.Empty;
                if (Passed == false)
                {
                    if (Exception.InnerException != null)
                    {
                        msg = string.Format("(Failed): {0}", Exception.InnerException.Message);
                    }
                    else if (Exception != null)
                    {
                        msg = Exception.Message;
                    }
                }
                else
                {
                    msg = "(Passed)";
                }
                return string.Format("{0} {1}",TestName,msg);
            }
        }

        public Exception Exception
        {
            get
            {
                return ex;
            }
            set
            {
                ex = value;
            }
        }


    }
}
