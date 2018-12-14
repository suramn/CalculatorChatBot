namespace CalculatorChatBot.Operations
{
    using System;

    public class ArithmeticOps
    {
        /// <summary>
        /// Calculates the overall sum of the incoming list of numbers
        /// </summary>
        /// <param name="inputString">List of numbers that are comma separated</param>
        /// <returns>An integer representing the sum</returns>
        public int OverallSum(string inputString)
        {
            string[] inputArrayStr = inputString.Split(',');
            int[] inputInts = Array.ConvertAll(inputArrayStr, int.Parse);

            int sum = inputInts[0];
            for (int i = 1; i < inputInts.Length; i++)
            {
                sum += inputInts[i]; 
            }

            return sum; 
        }

        /// <summary>
        /// Calculates the overall difference in the list of numbers
        /// </summary>
        /// <param name="inputString">List of numbers that are coming in, which are also comma separated</param>
        /// <returns>Integer that represents the overall difference between the numbers in the array</returns>
        public int OverallDifference(string inputString)
        {
            string[] inputArrayStr = inputString.Split(',');
            int[] inputInts = Array.ConvertAll(inputArrayStr, int.Parse);

            int diff = inputInts[0];
            for (int i = 1; i < inputInts.Length; i++)
            {
                diff -= inputInts[i];
            }

            return diff; 
        }

        /// <summary>
        /// Calculates the overall product - multiplying all of the elements in the list together
        /// </summary>
        /// <param name="inputString">The list of numbers</param>
        /// <returns>A number representing the product</returns>
        public int OverallProduct(string inputString)
        {
            string[] inputArrayStr = inputString.Split(',');
            int[] inputInts = Array.ConvertAll(inputArrayStr, int.Parse);

            int prod = inputInts[0];
            for (int i = 1; i < inputInts.Length; i++)
            {
                prod *= inputInts[i]; 
            }

            return prod; 
        }

        /// <summary>
        /// This calculates the quotient between two numbers
        /// </summary>
        /// <param name="inputString">The list of comma separated integers</param>
        /// <returns>The result when you divide the two numbers in the array</returns>
        public decimal Division(string inputString)
        {
            string[] inputArrayStr = inputString.Split(',');
            int[] inputInts = Array.ConvertAll(inputArrayStr, int.Parse);

            decimal quotient = 0; 
            if (inputInts.Length == 2 && inputInts[1] != 0)
            {
                quotient = Convert.ToDecimal(inputInts[0]) / inputInts[1];
                return decimal.Round(quotient, 2); 
            }
            else
            {
                return 0; 
            }
        }
    }
}