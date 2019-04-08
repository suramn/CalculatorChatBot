namespace CalculatorChatBot.Operations.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StatisticalTests
    {
        [TestMethod]
        public void AverageTest()
        {
            var stats = new StatisticalOps();
            string inputStr = "1,2,3";
            decimal testMean = 2;

            var expectedMean = stats.CalculateAverage(inputStr);
            Assert.AreEqual(testMean, expectedMean);

            if (testMean == expectedMean)
            {
                Console.Write($"Expected Mean - {expectedMean}, Test Mean - {testMean}" + Environment.NewLine);
                Console.Write("The AverageTest calculations pass");
            }
        }

        [TestMethod]
        public void MedianTest()
        {
            var stats = new StatisticalOps();
            string inputStr = "2,4,5,7,9";
            decimal testMedian = 5;

            var expectedMedian = stats.CalculateMedian(inputStr);
            Assert.AreEqual(testMedian, expectedMedian);

            if (testMedian == expectedMedian)
            {
                Console.Write($"Expected Median - {expectedMedian}, Test Median - {testMedian}" + Environment.NewLine);
                Console.Write("The MedianTest calculations pass");
            }
        }

        [TestMethod]
        public void ModeTest()
        {
            var stats = new StatisticalOps();
            string inputStr = "1,1,2,3,5,5,7";
            int[] testModes = stats.CalculateMode(inputStr);

            int[] expectedModes = new int[] { 1, 5 };

            if (testModes.Length == expectedModes.Length)
            {
                for (int i = 0; i < testModes.Length; i++)
                {
                    Assert.AreEqual(testModes[i], expectedModes[i]);
                }
                Console.Write("The ModeTest calculations pass");
            }
        }

        [TestMethod]
        public void RangeTest()
        {
            var stats = new StatisticalOps();
            string inputStr = "-3, 4, 2, 1";
            int testRange = stats.CalculateRange(inputStr);

            int expectedRange = 7;
            Assert.AreEqual(testRange, expectedRange);

            if (testRange == expectedRange)
            {
                Console.Write("The RangeTest calculations pass");
            }
        }

        [TestMethod]
        public void StandardDeviationTest()
        {
            var stats = new StatisticalOps();
            string inputStr = "-3, 4, 2, 1";
            double expectedStandardDev = 3.11;

            double testStandardDev = stats.CalculateStandardDeviation(inputStr);
            Assert.AreEqual(testStandardDev, expectedStandardDev);

            if (testStandardDev == expectedStandardDev)
            {
                Console.Write("The StandardDeviationTest calculations pass");
            }
        }
    }
}