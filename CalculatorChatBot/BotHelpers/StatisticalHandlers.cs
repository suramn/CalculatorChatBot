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

        public static async Task HandleMedianCommand(IDialogContext context, string parameters)
        {
            string[] inputTemp = parameters.Split(',');
            int[] inputInts = Array.ConvertAll(inputTemp, int.Parse);

            // TODO: Have the necessary logic to be able to get the median
            // properly
            await context.PostAsync($"bleh");
        }
    }
}