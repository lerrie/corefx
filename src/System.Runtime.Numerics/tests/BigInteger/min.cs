// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using Tools;
using Xunit;

namespace System.Numerics.Tests
{
    public class minTest
    {
        private static int s_samples = 10;
        private static Random s_random = new Random(100);

        [Fact]
        public static void RunMinTests()
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Min Method - Two Large BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = GetRandomByteArray(s_random);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }

            // Min Method - Two Small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }

            // Min Method - One large and one small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = GetRandomByteArray(s_random);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }

            // Min Method - One large BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = new byte[] { 0 };
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(s_random);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }

            // Min Method - One small BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = new byte[] { 0 };
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }


            // Min Method - One large BigIntegers and -1
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = new byte[] { 0xFF };
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = new byte[] { 0xFF };
                tempByteArray2 = GetRandomByteArray(s_random);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }

            // Min Method - One small BigIntegers and -1
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = new byte[] { 0xFF };
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = new byte[] { 0xFF };
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }


            // Min Method - One large BigIntegers and 1
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = new byte[] { 1 };
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = new byte[] { 1 };
                tempByteArray2 = GetRandomByteArray(s_random);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }

            // Min Method - One small BigIntegers and 1
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = new byte[] { 1 };
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");

                tempByteArray1 = new byte[] { 1 };
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                Assert.True(VerifyMinString(Print(tempByteArray1) + Print(tempByteArray2) + "bMin"), " Verification Failed");
            }
        }

        private static bool VerifyMinString(string opstring)
        {
            bool ret = true;
            StackCalc sc = new StackCalc(opstring);
            while (sc.DoNextOperation())
            {
                ret &= Eval(sc.snCalc.Peek().ToString(), sc.myCalc.Peek().ToString(), String.Format("Out of Sync stacks found.  BigInteger {0} Mine {1}", sc.snCalc.Peek(), sc.myCalc.Peek()));
            }
            return ret;
        }
        private static bool VerifyIdentityString(string opstring1, string opstring2)
        {
            bool ret = true;

            StackCalc sc1 = new StackCalc(opstring1);
            while (sc1.DoNextOperation())
            {	//Run the full calculation
                sc1.DoNextOperation();
            }

            StackCalc sc2 = new StackCalc(opstring2);
            while (sc2.DoNextOperation())
            {	//Run the full calculation
                sc2.DoNextOperation();
            }

            ret &= Eval(sc1.snCalc.Peek().ToString(), sc2.snCalc.Peek().ToString(), String.Format("Out of Sync stacks found.  BigInteger1: {0} BigInteger2: {1}", sc1.snCalc.Peek(), sc2.snCalc.Peek()));

            return ret;
        }

        private static Byte[] GetRandomByteArray(Random random)
        {
            return GetRandomByteArray(random, random.Next(0, 100));
        }
        private static Byte[] GetRandomByteArray(Random random, int size)
        {
            byte[] value = new byte[size];

            for (int i = 0; i < value.Length; ++i)
            {
                value[i] = (byte)random.Next(0, 256);
            }

            return value;
        }

        private static String Print(byte[] bytes)
        {
            String ret = "make ";

            for (int i = 0; i < bytes.Length; i++)
            {
                ret += bytes[i] + " ";
            }
            ret += "endmake ";

            return ret;
        }

        public static bool Eval<T>(T expected, T actual, String errorMsg)
        {
            bool retValue = expected == null ? actual == null : expected.Equals(actual);

            if (!retValue)
                return Eval(retValue, errorMsg +
                " Expected:" + (null == expected ? "<null>" : expected.ToString()) +
                " Actual:" + (null == actual ? "<null>" : actual.ToString()));

            return true;
        }
        public static bool Eval(bool expression, string message)
        {
            if (!expression)
            {
                Console.WriteLine(message);
            }

            return expression;
        }
    }
}
