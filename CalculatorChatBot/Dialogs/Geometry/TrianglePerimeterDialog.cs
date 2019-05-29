namespace CalculatorChatBot.Dialogs.Geometry
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Collections.Generic;

    public class TrianglePerimeterDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }
        public string InputString { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public TrianglePerimeterDialog(Activity incomingActivity)
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

            if (InputInts.Length != 3)
            {
                // TODO: Make sure to have some type of error/warning of the adaptive card
                // which would make the most sense
                await context.PostAsync("hiii");
            }
            else
            {
                // TODO: Complete with two possible cases
                // A) Equilateral - all sides are the same value
                // B) Isoceles - two out of three sides are the same value
                // C) Scaled - all three sides are of different values
                await context.PostAsync("heyooooo!");
            }
        }
    }
}