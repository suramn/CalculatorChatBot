namespace CalculatorChatBot.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StatisticalOps
    {
        /// <summary>
        /// Calculates the mean of the list of numbers
        /// </summary>
        /// <param name="inputString">Comma separated list of numbers</param>
        /// <returns>The mean (or average) of the list</returns>
        public decimal CalculateAverage(string inputString)
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

        /// <summary>
        /// Method to find the middle of the list of integers, also known as
        /// the median
        /// </summary>
        /// <param name="inputString">The list of integers that are separated by a comma</param>
        /// <returns>The median value</returns>
        public decimal CalculateMedian(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            decimal median;
            int size = inputIntsArr.Length;
            int[] copyArr = inputIntsArr;

            Array.Sort(copyArr);

            if (size % 2 == 0)
            {
                median = Convert.ToDecimal(copyArr[size / 2 - 1] + copyArr[size / 2]) / 2;
            }
            else
            {
                median = Convert.ToDecimal(copyArr[(size - 1) / 2]);
            }

            return decimal.Round(median, 2);
        }

        /// <summary>
        /// This function will now calculate the mode of the list of 
        /// integers
        /// </summary>
        /// <param name="inputString">List of comma separated integers</param>
        /// <returns>The mode - either 0, one single value, or multiple integer values</returns>
        public int[] CalculateMode(string inputString)
        {
            // From string to int array
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntArr = Array.ConvertAll(inputStringArr, int.Parse);

            // originalList is the original list of numbers
            List<int> originalList = new List<int>(inputIntArr);

            // This is what will be returned - modes.ToArray();
            List<int> modes = new List<int>();

            // The below code will have the data showing 
            // each element and how many times each element appears in the list
            var query = from numbers in originalList
                        group numbers by numbers
                            into groupedNumbers
                        select new
                        {
                            Number = groupedNumbers.Key,
                            Count = groupedNumbers.Count()
                        };

            int max = query.Max(g => g.Count);

            if (max == 1)
            {
                int mode = 0;
                modes.Add(mode); 
            }
            else
            {
                modes = query.Where(x => x.Count == max).Select(x => x.Number).ToList(); 
            }

            return modes.ToArray();
        }

        /// <summary>
        /// This method will be calculate the range in the list of numbers that is provided
        /// by the user
        /// </summary>
        /// <param name="inputString">The list of integers that the user provides</param>
        /// <returns>The difference between the largest and smallest elements in the list of integers</returns>
        public int CalculateRange(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            int range;

            if (inputIntsArr.Length >= 2)
            {
                var minVal = inputIntsArr.Min();
                var maxVal = inputIntsArr.Max();

                range = maxVal - minVal;
            }
            else
            {
                range = 0; 
            }

            return range;
        }

        /// <summary>
        /// Method that would calculate the variance of a list of numbers that are provided
        /// to the library
        /// </summary>
        /// <param name="inputString">The list of integers</param>
        /// <returns>The variance</returns>
        public double CalculateVariance(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            var mean = Convert.ToDouble(CalculateAverage(inputString));
            double squareDiffs = 0;
            int N = inputIntsArr.Length;

            for (int i = 0; i < inputIntsArr.Length; i++)
            {
                squareDiffs += Math.Pow(Math.Abs(Convert.ToDouble(inputIntsArr[i]) - mean), 2);
            }

            return squareDiffs / N;
        }

        /// <summary>
        /// Method that calculates the standard deviation
        /// </summary>
        /// <param name="inputString">The list of integers</param>
        /// <returns>Standard deviation of the list of numbers</returns>
        public double CalculateStandardDeviation(string inputString)
        {
            var calculatedVariance = CalculateVariance(inputString);

            double standardDev = Math.Sqrt(calculatedVariance);

            return standardDev;
        }
    }
}