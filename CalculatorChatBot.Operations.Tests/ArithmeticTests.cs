using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CalculatorChatBot.Operations.Tests
{
    [TestClass]
    public class ArithmeticTests
    {
        [TestMethod]
        public void OverallSummationTest()
        {
            var arith = new ArithmeticOps();

            var expSum = arith.OverallSum("1,3,4");
            int testSum = 8;

            Assert.AreEqual(expSum, testSum);

            if (testSum == expSum)
            {
                Console.Write($"Expected overall sum: {expSum}; Test overall sum: {testSum}" + Environment.NewLine);
                Console.Write("The OverallSummation test has passed");
            }
        } 
    }
}
