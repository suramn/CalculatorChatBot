namespace CalculatorChatBot.Operations
{
    using System;
    using System.Text;
    using System.Collections.Generic; 

    public class StatisticalOps
    {
        /// <summary>
        /// Calculates the mean of the list of numbers
        /// </summary>
        /// <param name="inputString">Comma separated list of numbers</param>
        /// <returns>The mean (or average) of the list</returns>
        public decimal Average(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);
            int sizeOfArray = inputIntsArr.Length;

            int sum = inputIntsArr[0];
            for (int i = 1; i < inputIntsArr.Length; i++)
            {
                sum += inputIntsArr[i]; 
            }

            var average = Convert.ToDecimal(sum) / sizeOfArray;

            return decimal.Round(average, 2);
        }
    }
}
