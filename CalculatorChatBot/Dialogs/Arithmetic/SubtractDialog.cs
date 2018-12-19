namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    /// <summary>
    /// This class will produce the overall difference of a list of numbers. If the list is too short, the 
    /// bot will reply with an appropriate message.
    /// </summary>
    [Serializable]
    public class SubtractDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }

        public string[] InputStringArray { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public SubtractDialog(Activity incomingActivity)
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
                int diff = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    diff -= InputInts[i];
                }

                #region Building out the results object to create a card

                #endregion
            }
            else
            {
                await context.PostAsync($"The input list is too short - you need more numbers");
            }

            // Return back to the RootDialog - popping this child dialog off the stack
            context.Done<object>(null); 
        }
    }
}