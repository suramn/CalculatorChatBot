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
            for (int i = 0; i < inputInts.Length; i++)
            {
                diff -= inputInts[i];
            }

            return diff; 
        }
    }
}