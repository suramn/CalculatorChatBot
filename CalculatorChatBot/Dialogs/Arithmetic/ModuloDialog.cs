namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    public class ModuloDialog : IDialog<object>
    {
        #region Dialog Properties
        public string InputString { get; set; }

        public string[] InputStringArray { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public ModuloDialog(Activity result)
        {
            // Extract the incoming text/message
            string[] incomingInfo = result.Text.Split(' ');

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

            if (InputInts.Length == 2 && InputInts[1] != 0)
            {
                int remainder = InputInts[0] % InputInts[1];
                await context.PostAsync($"Given the list {InputString}; the remainder = {remainder}");
            }
            else
            {
                await context.PostAsync($"The list: {InputString} may be invalid for this operation. Please double check, and try again");
            }

            // Return to the RootDialog - making sure to pop this child dialog off the stack
            context.Done<object>(null);
        }
    }
}