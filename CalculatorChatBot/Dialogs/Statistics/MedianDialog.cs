namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    public class MedianDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public MedianDialog(Activity incomingActivity)
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
            // Performing some type of validation on the incoming data
            if (InputInts.Length > 2)
            {
                decimal median;
                int size = InputInts.Length;
                int[] copyArr = InputInts;

                // Sorting the array
                Array.Sort(copyArr);

                if (size % 2 == 0)
                {
                    median = Convert.ToDecimal(copyArr[size / 2 - 1] + copyArr[size / 2]) / 2;
                }
                else
                {
                    median = Convert.ToDecimal(copyArr[(size - 1) / 2]);
                }

                await context.PostAsync($"Given the list: {InputString}; the median = {decimal.Round(median, 2)}");
            }
            else
            {
                await context.PostAsync($"Please double check the input: {InputString} and try again");
            }

            // Making sure to return back to the RootDialog
            context.Done<object>(null);
        }
    }
}