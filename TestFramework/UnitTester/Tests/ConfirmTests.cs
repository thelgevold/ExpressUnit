using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnit;
using ExpressUnitModel;

namespace ExpressUnitTests
{
    [TestClass]
    public class ConfirmTests
    {
        [UnitTest]
        public void ConfirmGreaterTest()
        {
            Confirm.IsGreater(4, 1);

            Confirm.IsGreater(2.0, 1.9);
        }

        [UnitTest]
        public void ConfirmStringObjectsAreEqualTests()
        {
            bool ok = Confirm.Equals("1", "1");
            ok = Confirm.Equals(null, null);
        }

        [UnitTest]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenEmptyStringAndNull()
        {
            try
            {
                Confirm.Equals(null, string.Empty);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest]
        public void ConfirmIntegerObjectsAreEqualTests()
        {
            bool ok = Confirm.Equals(1, 1);
        }

        [UnitTest]
        public void ConfirmDoubleObjectsAreEqualTests()
        {
            bool ok = Confirm.Equals(1.0, 1.0);
        }


        [UnitTest]
        public void ConfirmCurrentObjectIsEqualToItselfTest()
        {
            Confirm.Equals(this, this);
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

        [UnitTest]
        public void ConfirmEqualityExceptionThrownForDifferentIntegersTest()
        {
            try
            {
                Confirm.Equals(1, 2);
                throw new EqualityException(1, 2);
            }
            catch (EqualityException) { }

        }

        [UnitTest]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenUnequalStringsTest()
        {
            try
            {
                Confirm.Equals("test", "test2");
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenUnequalIntegersTest()
        {
            try
            {
                Confirm.Equals(1, 4);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenIntegerAndNullTest()
        {
            try
            {
                Confirm.Equals(null, 4);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest]
        public void ConfirmEqualityExceptionThrownTest()
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


        [UnitTest]
        public void ConfirmTwoDifferentObjectRefsAreDifferentTest()
        {
            Confirm.Different(new object(), new object());
        }
      
        [UnitTest]
        public void ConfirmNullIsDifferentFromEmptyStringTest()
        {
            Confirm.Different(null, "");
        }

        [UnitTest]
        public void ConfirmIntegerOneIsDifferentFromIntegerFiveTest()
        {
            Confirm.Different(5, 1);
        }

        [UnitTest]
        public void ConfirmDifferentUnEqualExceptionThrownWhenNullIsComparedToNullStringTest()
        {
            try
            {
                Confirm.Different(null, null);
                throw new Exception();
            }
            catch (UnequalException) { }
        }

        [UnitTest]
        public void ConfirmDifferentUnEqualExceptionThrownWhenEmptyStringIsComparedToEmptyStringTest()
        {
            try
            {
                Confirm.Different(string.Empty, string.Empty);
                throw new Exception();
            }
            catch (UnequalException) { }
        }

        [UnitTest]
        public void ConfirmUnEqualExceptionThrownWhenIntegerOneIsComparedToIntegerOne()
        {
            try
            {
                Confirm.Different(1, 1);
                throw new Exception();
            }
            catch (UnequalException) { }

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

        private void ThrowsEqualityException()
        {
            Confirm.Equals("1", "8");
        }

      
        
    }
}
