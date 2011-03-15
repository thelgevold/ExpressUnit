/*
Copyright (C) 2009  Torgeir Helgevold

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
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

        public string UseCase
        {
            get;
            set;
        }

        public TimeSpan Duration
        {
            get;
            set;
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
            get;
            set;
        }

        public string StackTrace
        {
            get
            {
                if (Exception != null)
                {
                    return Exception.StackTrace;
                }
                return string.Empty;
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
