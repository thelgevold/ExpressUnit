using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnitModel
{
    public delegate void TargetMethod();
    public delegate void DisplayTestResults(IList<TestResult> testResult);
}
