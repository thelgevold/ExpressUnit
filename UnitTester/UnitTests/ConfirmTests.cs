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
using ExpressUnit;
using ExpressUnitModel;
using UnitTester.Helpers;
using System.Threading;
using System.Collections;

namespace ExpressUnitTests
{
    [TestClass]
    public class ConfirmTests
    {
        [UnitTest]
        public void Confirm_IsNull()
        {
            Confirm.IsTrue(Confirm.IsNull(null));
        }

        [UnitTest]
        public void Confirm_IsNotNull()
        {
            Confirm.IsTrue(Confirm.IsNotNull(new Object()));
        }

        [UnitTest]
        public void ConfirmIsTrue()
        {
            Confirm.IsTrue(true);
        }

        [UnitTest]
        public void ConfirmIsFalse()
        {
            Confirm.IsFalse(false);
        }

        [UnitTest]
        [ExceptionThrown(typeof(EqualityException))]
        public void ConfirmIsTrueThrowsEqualExceptionIfValueIsNotTrue()
        {
            Confirm.IsTrue(false);
        }

        [UnitTest]
        [ExceptionThrown(typeof(EqualityException))]
        public void ConfirmIsFalseThrowsEqualExceptionIfValueIsNotFalse()
        {
            Confirm.IsFalse(true);
        }

        [UnitTest(UseCase="Confirm Is Greater")]
        public void ConfirmGreaterTest()
        {
            Confirm.IsGreater(4, 1);

            Confirm.IsGreater(2.0, 1.9);
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmStringObjectsAreEqualTests()
        {
            bool ok = Confirm.Equal("1", "1");
            ok = Confirm.Equal(null, null);
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenEmptyStringAndNull()
        {
            try
            {
                Confirm.Equal(null, string.Empty);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmIntegerObjectsAreEqualTests()
        {
            bool ok = Confirm.Equal(1, 1);
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmDoubleObjectsAreEqualTests()
        {
            bool ok = Confirm.Equal(1.0, 1.0);
        }


        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmCurrentObjectIsEqualToItselfTest()
        {
            Confirm.Equal(this, this);
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmTestBothNull()
        {
            Confirm.Equal(null, null);
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmTestBothOne()
        {
            Confirm.Equal(1, 1);
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmThatCollectionsAreEqualWhenCollectionTypeOverridesEqualsToCompareListElementsTest()
        {
            SameCollection col1 = new SameCollection();
            SameCollection col2 = new SameCollection();

            string str = "test";

            col1.Add(str);
            col2.Add(str);

            Confirm.Equal(col1, col2);

        }


        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmEqualityExceptionThrownForDifferentIntegersTest()
        {
            try
            {
                Confirm.Equal(1, 2);
                throw new EqualityException(1, 2);
            }
            catch (EqualityException) { }

        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenUnequalStringsTest()
        {
            try
            {
                Confirm.Equal("test", "test2");
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenUnequalIntegersTest()
        {
            try
            {
                Confirm.Equal(1, 4);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm Equals")]
        public void ConfirmThrowsEqualityExceptionForComparisonBetweenIntegerAndNullTest()
        {
            try
            {
                Confirm.Equal(null, 4);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm Exception Thrown")]
        public void ConfirmEqualityExceptionThrownTest()
        {
            TargetMethod target = new TargetMethod(ThrowsEqualityException);
            Confirm.ExceptionThrown(typeof(EqualityException),target);
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatCollectionsContainingSameObjectsOfTypeDateTimeAreSameTest()
        {
            DateTime date = DateTime.Now;
            List<DateTime> dates = new List<DateTime>();
            List<DateTime> dates2 = new List<DateTime>();
            dates.Add(date);
            dates2.Add(date);
            Confirm.SameCollections(dates, dates2);
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatCollectionsContainingDifferentObjectsOfTypeDateTimeAreNotSameTest()
        {
            try
            {
                List<DateTime> dates = new List<DateTime>();
                List<DateTime> dates2 = new List<DateTime>();
                dates.Add(DateTime.Now);
                dates2.Add(DateTime.Now.AddDays(1));
                Confirm.SameCollections(dates, dates2);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatCollectionsContainingDifferentObjectsOfTypeStringAreNotSameTest()
        {
            try
            {
                List<string> s1 = new List<string>();
                List<string> s2 = new List<string>();
                s1.Add("1");
                s2.Add("2");
                Confirm.SameCollections(s1, s2);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatCollectionsContainingSameObjectsOfTypeStringAreSameTest()
        {
            string str = "Test";
            List<string> s1 = new List<string>();
            List<string> s2 = new List<string>();
            s1.Add(str);
            s2.Add(str);
            Confirm.SameCollections(s1, s2);
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void UnEvenCollectionsShouldThrowEqualityException()
        {
            try
            {
                string str = "Test";
                List<string> s1 = new List<string>();
                List<string> s2 = new List<string>();
                s1.Add(str);
                s2.Add(str);
                s2.Add("ttt");
                Confirm.SameCollections(s1, s2);
                throw new Exception();
            }
            catch (EqualityException) { }
        }


        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatArraysContainingSameObjectsOfTypeIntAreSameTest()
        {
            int[] a1 = new int[2];
            a1[0] = 1;
            a1[1] = 2;
            int[] a2 = new int[2];
            a2[0] = 1;
            a2[1] = 2;

            Confirm.SameCollections(a1, a2);
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatArraysContainingSameObjectsOfTypeStringAreSameTest()
        {
            string[] a1 = new string[2];
            a1[0] = "1";
            a1[1] = "2";
            string[] a2 = new string[2];
            a2[0] = "1";
            a2[1] = "2";

            Confirm.SameCollections(a1, a2);
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatTwoHashTablesAreEqualTest()
        {
            Hashtable hash1 = new Hashtable();
            hash1.Add("1", "1");

            Hashtable hash2 = new Hashtable();
            hash2.Add("1", "1");

            Confirm.SameCollections(hash1, hash2);

            hash1 = new Hashtable();
            hash1.Add(1, "1");

            hash2 = new Hashtable();
            hash2.Add(1, "1");

            Confirm.SameCollections(hash1, hash2);

        }

        [ExceptionThrown(typeof(EqualityException))]
        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatTwoUnEqualHashTablesAreNotEqualTest1()
        {
            Hashtable hash1 = new Hashtable();
            hash1.Add("1", "1");

            Hashtable hash2 = new Hashtable();
            hash2.Add("1", "2");

            bool ok = Confirm.SameCollections(hash1, hash2);
            
        }

        [ExceptionThrown(typeof(EqualityException))]
        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatTwoUnEqualHashTablesAreNotEqualTest2()
        {
            Hashtable hash1 = new Hashtable();
            hash1.Add("1", "1");

            Hashtable hash2 = new Hashtable();
            hash2.Add(1, "1");

            Confirm.SameCollections(hash1, hash2);

        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatCollectionsThatDoNotContainSameObjectsThrowEqualExceptionTest()
        {
            try
            {
                int[] a1 = new int[2];
                a1[0] = 1;
                a1[1] = 2;
                int[] a2 = new int[2];
                a2[0] = 5;
                a2[1] = 4;

                Confirm.SameCollections(a1, a2);
                throw new Exception();
            }
            catch (EqualityException) { }
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatTwoNullValuesAreSameTest()
        {
            Confirm.SameCollections(null, null);
        }

        [UnitTest(UseCase = "Confirm SameCollections")]
        public void ConfirmThatThatCollectionsContainingSameObjectsOfTypeConfirmTestsAreSameTest()
        {
            ConfirmTests tests = new ConfirmTests();
            List<ConfirmTests> t1 = new List<ConfirmTests>();
            List<ConfirmTests> t2 = new List<ConfirmTests>();
            t1.Add(tests);
            t2.Add(tests);
            Confirm.SameCollections(t1, t2);
        }


        [UnitTest(UseCase = "Confirm SameCollections")]
        [ExceptionThrown(typeof(EqualityException))]
        public void ConfirmThatThatCollectionsContainingIntsAndStringsAreNotSameTest()
        {
            List<int> t1 = new List<int>();
            List<string> t2 = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                t1.Add(i);
                t2.Add(i.ToString());
            }

            Confirm.SameCollections(t1, t2);
        }



        [UnitTest(UseCase = "Confirm Exception Thrown")]
        public void ConfirmExceptionThrownTest()
        {
            TargetMethod target = new TargetMethod(ThrowDivideByZeroException);
            Confirm.ExceptionThrown(typeof(DivideByZeroException), target);
        }

        [UnitTest(UseCase = "Confirm Exception Thrown")]
        public void ConfirmExceptionThrownTest2()
        {
            TargetMethod target = new TargetMethod(ThrowNoException);
            Confirm.ExceptionThrown(typeof(ExceptionNotThrownException), target);
        }


        [UnitTest(UseCase = "Confirm Different")]
        public void ConfirmTwoDifferentObjectRefsAreDifferentTest()
        {
            Confirm.Different(new object(), new object());
        }

        [UnitTest(UseCase = "Confirm Different")]
        public void ConfirmNullIsDifferentFromEmptyStringTest()
        {
            Confirm.Different(null, "");
        }

        [UnitTest(UseCase = "Confirm Different")]
        public void ConfirmIntegerOneIsDifferentFromIntegerFiveTest()
        {
            Confirm.Different(5, 1);
        }

        [UnitTest(UseCase = "Confirm Exception Thrown")]
        [ExceptionThrown(typeof(UnequalException))]
        public void ConfirmDifferentUnEqualExceptionThrownWhenNullIsComparedToNullStringTest()
        {
            Confirm.Different(null, null);
        }

        [UnitTest(UseCase = "Confirm Exception Thrown")]
        [ExceptionThrown(typeof(UnequalException))]
        public void ConfirmDifferentUnEqualExceptionThrownWhenEmptyStringIsComparedToEmptyStringTest()
        {
           Confirm.Different(string.Empty, string.Empty);
        }

        [UnitTest(UseCase = "Confirm Exception Thrown")]
        [ExceptionThrown(typeof(UnequalException))]
        public void ConfirmUnEqualExceptionThrownWhenIntegerOneIsComparedToIntegerOne()
        {
            Confirm.Different(1, 1);
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
            Confirm.Equal("1", "8");
        }

        
        
    }
}
