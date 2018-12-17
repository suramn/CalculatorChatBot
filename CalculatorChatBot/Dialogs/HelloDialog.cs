namespace CalculatorChatBot.Dialogs
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    [Serializable]
    public class HelloDialog : IDialog<object>
    {
        public int NumTimes { get; set; }

        public HelloDialog(Activity incomingActivity)
        {
            // Extract the incoming text
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // What is the parameter/property to have something done - making sure
            // to look at the length of the incoming string
            NumTimes = incomingInfo.Length == 2 ? int.Parse(incomingInfo[1]) : 0; 
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (NumTimes > 0)
            {
                for (int i = 0; i < NumTimes; i++)
                {
                    // Sending out the standard greeting message
                    await context.PostAsync("Hello!");
                }
            }
            else
            {
                // Send the message 'Hello!' once
                await context.PostAsync("Hello!");
            }

            // Return back to the RootDialog - popping this child dialog off the stack
            context.Done<object>(null);
        }
    }
}