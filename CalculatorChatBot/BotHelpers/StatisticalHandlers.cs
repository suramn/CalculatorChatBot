namespace CalculatorChatBot.BotHelpers
{
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class StatisticalHandlers
    {
        /// <summary>
        /// This will be having the logic to calculate the mean of a list of numbers
        /// </summary>
        /// <param name="context">The current conversation happening</param>
        /// <param name="parameters">List of numbers on which to conduct the necessary calculations</param>
        /// <returns>A unit of execution</returns>
        public static async Task HandleAverageCommand(IDialogContext context, string parameters)
        {
            string[] paramTemp = parameters.Split(',');
            int[] paramInts = Array.ConvertAll(paramTemp, int.Parse);

            int sum = paramInts[0];
            for (int i = 1; i < paramInts.Length; i++)
            {
                sum += paramInts[i];
            }

            var average = Convert.ToDecimal(sum) / paramInts.Length;

            await context.PostAsync($"Given the list: {parameters} - the calculated average = {decimal.Round(average, 2)}");
        }

        /// <summary>
        /// This will contain the logic of calculating the median of a list of numbers
        /// </summary>
        /// <param name="context">The current conversation taking place</param>
        /// <param name="parameters">The list of integers</param>
        /// <returns>A unit of execution</returns>
        public static async Task HandleMedianCommand(IDialogContext context, string parameters)
        {
            string[] inputTemp = parameters.Split(',');
            int[] inputInts = Array.ConvertAll(inputTemp, int.Parse);

            decimal median;
            int size = inputInts.Length;
            int[] copy = inputInts;

            // When it comes to calculating the median you want to make sure 
            // that your input list of integers is sorted
            Array.Sort(copy);

            // Calculating the median one of two ways
            // 1. It's the middle number in a list of odd length
            // 2. It's the average of the two middle numbers in a list of even length
            if (size % 2 == 0)
            {
                median = Convert.ToDecimal(copy[size / 2 - 1] + copy[size / 2]) / 2;
            }
            else
            {
                median = Convert.ToDecimal(copy[(size - 1) / 2]); 
            }

            await context.PostAsync($"Given the list: {parameters} - the calculated median = {decimal.Round(median, 2)}");
        }

        public static async Task HandleModeCommand(IDialogContext context, string parameters)
        {

        }
    }
}