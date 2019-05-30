namespace CalculatorChatBot.Dialogs.Geometry
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    public class RectangleAreaDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public RectangleAreaDialog(Activity incomingActivity)
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

            // TODO: Complete the Rectangle area (success and failure cases)
            // TODO2: May want to also consider the square here

            await context.PostAsync("hola!");
        }
    }
}