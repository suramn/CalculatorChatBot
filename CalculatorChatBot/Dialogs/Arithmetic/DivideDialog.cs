namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Threading.Tasks;
    using System;

    public class DivideDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public DivideDialog(Activity incomingActivity)
        {
            // Parsing through the necessary incoming text
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

            decimal quotient = 0;
            if (InputInts.Length == 2 && InputInts[1] != 0)
            {
                quotient = Convert.ToDecimal(InputInts[0]) / InputInts[1];
                await context.PostAsync($"Given the list {InputString}; the quotient = {decimal.Round(quotient, 2)}");
            }
            else
            {
                await context.PostAsync("The list may be too long, or one of the elements could be 0 - please try again later."); 
            }

            // Return back to the root dialog - popping this child dialog from the dialog stack
            context.Done<object>(null);
        }
    }
}