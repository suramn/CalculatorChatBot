namespace CalculatorChatBot.Dialogs.Geometry
{
    using System;
    using System.Threading.Tasks;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    public class CircleAreaDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }
        public string InputString { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public CircleAreaDialog(Activity incomingActivity)
        {
            // Parsing through the incoming message text
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // Which properties are being set for the operations to 
            // be performed
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
           
            if (InputInts.Length != 1)
            {
                await context.PostAsync("Hitting the error condition in the Circle Area Dialog");
            }
            else
            {
                await context.PostAsync("Making sure to hit the correct branch in the Circle Area Dialog");
            }

            // TODO: Implement the necessary logic - it's rather straight-forward
            // If the length of the array input is = 1 - calculate area
            // If the length of the array input is = 0 - throw the error card
            // If the length of the array input is > 1 - throw the error card as well
        }
    }
}