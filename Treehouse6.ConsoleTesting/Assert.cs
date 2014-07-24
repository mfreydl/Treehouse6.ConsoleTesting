using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{

    public sealed class Assert
    {

        public static void AreEqual(string expected, string actual, string failureMessage)
        {
            if (expected != actual)
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = expected };
        }

        public static void AreEqual<T>(T expected, T actual, string failureMessage)
        {
            if (!actual.Equals(expected))
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = expected };
        }


        public static void AreNotEqual(string expected, string actual, string failureMessage)
        {
            if (expected == actual)
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = expected };
        }

        public static void AreNotEqual<T>(T expected, T actual, string failureMessage)
        {
            if (actual.Equals(expected))
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = expected };
        }

        public static void IsTrue(bool actual, string failureMessage)
        {
            if (!actual)
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = true };
        }

        public static void IsFalse(bool actual, string failureMessage)
        {
            if (actual)
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = false };
        }

        public static void IsNull(object actual, string failureMessage)
        {
            if (actual != null)
                throw new AssertionFailureException(failureMessage) { Actual = actual, Expected = "null" };
        }

        public static void IsNotNull(object actual, string failureMessage)
        {
            if (actual == null)
                throw new AssertionFailureException(failureMessage) { Actual = "null", Expected = "Not null" };
        }
        

        public static void IsInstanceOfType(object actual, Type expected, string failureMessage)
        {
            if (!expected.IsAssignableFrom(actual.GetType()))
                throw new AssertionFailureException(failureMessage) { Actual = actual.GetType(), Expected = expected };
        }

        public static void IsNotInstanceOfType(object actual, Type expected, string failureMessage)
        {
            if (expected.IsAssignableFrom(actual.GetType()))
                throw new AssertionFailureException(failureMessage) { Actual = actual.GetType(), Expected = expected };
        }

        public static void Fail(string failureMessage)
        {
            throw new AssertionFailureException(failureMessage);
        }
    }

}
