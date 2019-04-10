using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CalculatorChatBot.Operations.Tests
{
    /// <summary>
    /// This class contains the unit tests of the ArithmeticOps.cs class
    /// </summary>
    [TestClass]
    public class ArithmeticTests
    {
        [TestMethod]
        public void OverallSummationTest()
        {
            var arith = new ArithmeticOps();
            var inputString = "1,3,4";

            var expSum = arith.OverallSum(inputString);
            int testSum = 8;

            Assert.AreEqual(expSum, testSum);

            if (testSum == expSum)
            {
                Console.Write($"Expected overall sum: {expSum}; Test overall sum: {testSum}" + Environment.NewLine);
                Console.Write("The OverallSummation test has passed");
            }
        }

        [TestMethod]
        public void OverallDifferenceTest()
        {
            var arith = new ArithmeticOps();
            var inputString = "-1,0,-3";

            var expOverallDiff = arith.OverallDifference(inputString);
            int testOverallDiff = 2;

            Assert.AreEqual(expOverallDiff, testOverallDiff);

            if (testOverallDiff == expOverallDiff)
            {
                Console.Write($"Expected overall difference: {expOverallDiff}; Test overall difference: {testOverallDiff}" + Environment.NewLine);
                Console.Write("The OverallDifference test has passed");
            }
        }

        [TestMethod]
        public void OverallProductTest()
        {
            var arith = new ArithmeticOps();
            var inputString = "1,0,4";
            
            var expOverallProd1 = arith.OverallProduct(inputString);
            int testOverallProd1 = 0;
            
            Assert.AreEqual(expOverallProd1, testOverallProd1);
            
            var scenario = testOverallProd1 == expOverallProd1;
            if (scenario)
            {
                Console.Write($"Scenario - Expected overall product: {expOverallProd1}; Test overall product: {testOverallProd1}" + Environment.NewLine);
                Console.Write("The OverallProduct test has passed");
            }
        }

        [TestMethod]
        public void OverallProductTest2()
        {
            var arith = new ArithmeticOps();
            var inputString2 = "1,3,-2";
            var expOverallProd2 = arith.OverallProduct(inputString2);
            int testOverallProd2 = -6;

            Assert.AreEqual(expOverallProd2, testOverallProd2);

            if (testOverallProd2 == expOverallProd2)
            {
                Console.Write($"Scenario - Expected overall product: {expOverallProd2}; Test overall product: {testOverallProd2}" + Environment.NewLine);
                Console.Write("The OverallProduct test has passed");
            }
        }

        [TestMethod]
        public void OverallDivisionTest()
        {
            var arith = new ArithmeticOps();

            var inputString = "2,10";

            var expectedQuotient = arith.OverallDivision(inputString);
            decimal testQuotient = 0.2m;

            Assert.AreEqual(expectedQuotient, testQuotient);

            if (testQuotient == expectedQuotient)
            {
                Console.Write($"Expected overall quotient: {expectedQuotient}; Test overall difference: {testQuotient}" + Environment.NewLine);
                Console.Write("The OverallDivision test has passed");
            }
        }

        [TestMethod]
        public void OverallModuloTest()
        {
            var arith = new ArithmeticOps();
            var inputString = "2,10";
            var expectedResult = arith.OverallModulo(inputString);
            var testResult = 2;

            Assert.AreEqual(expectedResult, testResult);

            if (expectedResult == testResult)
            {
                Console.Write($"Expected overall remainder: {expectedResult}; Test overall remainder: {testResult}" + Environment.NewLine);
                Console.Write("The OverallModulo test has passed");
            }
        }
    }
}