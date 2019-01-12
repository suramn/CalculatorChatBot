namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    [Serializable]
    public class VarianceDialog : IDialog<object>
    {
        public VarianceDialog(Activity incomingActivity)
        {

        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hit the Variance dialog");

            // Popping the current dialog off the dialog stack
            context.Done<object>(null);
        }
    }
}