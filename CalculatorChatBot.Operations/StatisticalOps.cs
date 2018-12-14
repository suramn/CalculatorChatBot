namespace CalculatorChatBot.Operations
{
    using System;
    using System.Text;
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
    }
}
