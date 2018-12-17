namespace CalculatorChatBot.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

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

        public Task StartAsync(IDialogContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}