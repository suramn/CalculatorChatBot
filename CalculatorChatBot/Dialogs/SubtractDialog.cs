namespace CalculatorChatBot.Dialogs
{
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    public class SubtractDialog : IDialog<object>
    {
        #region Dialog properties

        #endregion

        public SubtractDialog(Activity incomingActivity)
        {

        }

        public Task StartAsync(IDialogContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}