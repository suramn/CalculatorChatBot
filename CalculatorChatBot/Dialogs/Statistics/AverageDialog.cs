namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    public class AverageDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public AverageDialog(Activity incomingActivity)
        {
            // Extract the incoming text/message
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // What is the properties to be set for the necessary 
            // operation to be performed
            if (!string.IsNullOrEmpty(incomingInfo[1]))
            {
                InputString = incomingInfo[1];

                InputStringArray = InputString.Split(',');

                InputInts = Array.ConvertAll(InputStringArray, int.Parse);
            }
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (InputInts.Length > 1)
            {
                int sum = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    sum += InputInts[i];
                }

                decimal mean = Convert.ToDecimal(sum) / InputInts.Length;

                await context.PostAsync($"Given the list: {InputString}; the average = {decimal.Round(mean, 2)}");
            }
            else
            {
                await context.PostAsync("Your list may be too small to calculate an average. Please try again later.");
            }

            // Return back to the RootDialog
            context.Done<object>(null);
        }
    }
}