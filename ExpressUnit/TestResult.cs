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
