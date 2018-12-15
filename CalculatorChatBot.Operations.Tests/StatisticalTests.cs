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
    }
}