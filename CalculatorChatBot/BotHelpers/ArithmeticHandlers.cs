namespace CalculatorChatBot.BotHelpers
{
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Threading.Tasks;

    public class ArithmeticHandlers
    {
        public static async Task HandleAddCommand(IDialogContext context, string parameters)
        {
            string[] paramTemp = parameters.Split(',');
            int[] paramInts = Array.ConvertAll(paramTemp, int.Parse);

            // Setting the result initially to the first element of the array
            int result = paramInts[0];

            for (int i = 1; i < paramInts.Length; i++)
            {
                result += paramInts[i]; 
            }

            await context.PostAsync($"Given {parameters} the sum = {result}");
        }

        public static async Task HandleSubtractCommand(IDialogContext context, string parameters)
        {
            string[] paramTemp = parameters.Split(',');
            int[] paramInts = Array.ConvertAll(paramTemp, int.Parse);

            int result = paramInts[0];
            for (int i = 1; i < paramInts.Length; i++)
            {
                result -= paramInts[i];
            }

            await context.PostAsync($"Given {parameters} the overall difference = {result}");
        }

        public static async Task HandleMultiplyCommand(IDialogContext context, string parameters)
        {
            string[] paramTemp = parameters.Split(',');
            int[] paramInts = Array.ConvertAll(paramTemp, int.Parse);

            int result = paramInts[0];
            for (int i = 1; i < paramInts.Length; i++)
            {
                result *= paramInts[i];
            }

            await context.PostAsync($"Given the list {parameters} the overall product = {result}");
        }

        public static async Task HandleDivideCommand(IDialogContext context, string parameters)
        {
            string[] paramTemp = parameters.Split(',');
            int[] paramInts = Array.ConvertAll(paramTemp, int.Parse);

            var outputMsg = "";

            if (paramInts.Length == 2 && paramInts[1] != 0)
            {
                decimal result = paramInts[0] / paramInts[1];
                outputMsg = $"Given the list {parameters}, the quotient = {Decimal.Round(result, 2)}";
            }
            else
            {
                outputMsg = "There should be only two integers, and the second digit input cannot be 0!";
            }

            await context.PostAsync(outputMsg);
        }
    }
}