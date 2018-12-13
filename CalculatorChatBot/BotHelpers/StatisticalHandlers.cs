namespace CalculatorChatBot.BotHelpers
{
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class StatisticalHandlers
    {
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
    }
}