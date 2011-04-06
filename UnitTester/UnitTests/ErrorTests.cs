using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnitModel;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class ErrorTests
    {
        [UnitTest]
        public void Test_Error_Message_For_IsNull_With_ErrorMessage()
        {
            string errorMessage = "Object is not null";

            try
            {
                Confirm.IsNull("a",errorMessage);
            }
            catch (EqualityException ex)
            {
                Confirm.Equal(string.Format("The expected value is: [{0}], but the actual value is: [{1}] Error: {2}", "null", "a", errorMessage), ex.Message);
                return;
            }

            throw new Exception("Error");
        }

        [UnitTest]
        public void Test_Error_Message_For_IsNull_Without_ErrorMessage()
        {
            try
            {
                Confirm.IsNull("a");
            }
            catch (EqualityException ex)
            {
                Confirm.Equal(string.Format("The expected value is: [{0}], but the actual value is: [{1}]", "null", "a"), ex.Message);
                return;
            }

            throw new Exception("Error");
        }

        [UnitTest]
        public void Test_Error_Message_For_Equal_With_Custom_Error_Message()
        {
            string errorMessage = "The two strings are not equal";

            try
            {
                Confirm.Equal("a", "b", errorMessage);
            }
            catch (EqualityException ex)
            {
                Confirm.Equal(string.Format("The expected value is: [{0}], but the actual value is: [{1}] Error: {2}", "a", "b", errorMessage), ex.Message);
                return;
            }

            throw new Exception("Error");
        }

        [UnitTest]
        public void Test_Error_Message_For_Equal_Without_Custom_Error_Message()
        {
            try
            {
                Confirm.Equal("a", "b");
            }
            catch (EqualityException ex)
            {
                Confirm.Equal(string.Format("The expected value is: [{0}], but the actual value is: [{1}]", "a", "b"), ex.Message);
                return;
            }

            throw new Exception("Error");
        }

        [UnitTest]
        public void Test_Error_Message_For_Different_With_Custom_Error_Message()
        {
            string errorMessage = "The two strings are not different";
            try
            {
                Confirm.Different("a", "a", errorMessage);
            }
            catch (UnequalException ex)
            {
                Confirm.Equal(string.Format("The the actual value should not be equal to {0} Error: {1}", "a",errorMessage), ex.Message);
                return;
            }

            throw new Exception("Error");
        }

        [UnitTest]
        public void Test_Error_Message_For_Different_Without_Custom_Error_Message()
        {
            try
            {
                Confirm.Different("a", "a");
            }
            catch (UnequalException ex)
            {
                Confirm.Equal(string.Format("The the actual value should not be equal to {0}", "a"), ex.Message);
                return;
            }

            throw new Exception("Error");
        }
    }
}
