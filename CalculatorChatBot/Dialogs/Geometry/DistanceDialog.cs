namespace CalculatorChatBot.Dialogs.Geometry
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    public class DistanceDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public DistanceDialog(Activity incomingActivity)
        {
            // Parsing through the incoming information
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // Setting all of the properties
            if (!string.IsNullOrEmpty(incomingInfo[1]))
            {
                InputString = incomingInfo[1];

                InputStringArray = InputString.Split(',');

                InputInts = Array.ConvertAll(InputStringArray, int.Parse);
            }
        }

        public async Task StartAsync(IDialogContext context)
        {
            // throw new NotImplementedException();
            context.Done<object>(null); 
        }
    }
}