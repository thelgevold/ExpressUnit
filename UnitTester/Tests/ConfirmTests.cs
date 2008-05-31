using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnit;

namespace ExpressUnitTests
{
    [TestClass]
    public class ConfirmTests
    {
        [UnitTest]
        [Ignore]
        public void ConfirmGreaterTest()
        {
            Confirm.IsGreater(4, 1);

            Confirm.IsGreater(2.0, 1.9);
        }

        [UnitTest]
        public void ConfirmObjectsAreEqualTests1()
        {
            bool ok = Confirm.Equals("1", "1");
            ok = Confirm.Equals(null, null);
        }

        [UnitTest]
        public void ConfirmObjectsAreEqualTests2()
        {
            bool ok = Confirm.Equals(1, 1);
        }

        private void ThrowsEqualityException()
        {
            Confirm.Equals("1", "8");
        }

        [UnitTest]
        public void ConfirmUnequalTest()
        {
            TargetMethod target = new TargetMethod(ThrowsEqualityException);
            Confirm.ExceptionThrown(typeof(EqualityException),target);
        }

        [UnitTest]
        public void ConfirmExceptionThrownTest()
        {
            TargetMethod target = new TargetMethod(ThrowDivideByZeroException);
            Confirm.ExceptionThrown(typeof(DivideByZeroException), target);
        }

        [UnitTest]
        public void ConfirmExceptionThrownTest2()
        {
            TargetMethod target = new TargetMethod(ThrowNoException);
            Confirm.ExceptionThrown(typeof(ExceptionNotThrownException), target);
        }

        private void ThrowDivideByZeroException()
        {
            int a = 0;
            int b = 3 / a;
        }

        private void ThrowNoException()
        {
            TargetMethod target = new TargetMethod(ThrowDivideByZeroException);
            Confirm.ExceptionThrown(typeof(ArgumentException), target);
        }

        [UnitTest]
        public void ConfirmTestNulls1()
        {
            Confirm.Equals(null, "");
        }

        [UnitTest]
        public void ConfirmTestNulls2()
        {
            Confirm.Equals("", null);
        }

        [UnitTest]
        public void ConfirmTestBothNull()
        {
            Confirm.Equals(null, null);
        }

        [UnitTest]
        public void ConfirmTestBothOne()
        {
            Confirm.Equals(1, 1);

        }

        
    }
}
