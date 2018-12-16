namespace CalculatorChatBot.Dialogs
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Scorables;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    [Serializable]
    public class RootDialog : DispatchDialog
    {
        [RegexPattern("add")]
        [RegexPattern("addition")]
        [ScorableGroup(1)]
        public async Task RunAddDialog(IDialogContext context, IActivity activity)
        {
            var result = activity as Activity;
            context.Call(new AddDialog(result), EndDialog); 
        }

        [RegexPattern("subtract")]
        [ScorableGroup(1)]
        public async Task RunSubtractDialog(IDialogContext context, IActivity activity)
        {
            var result = activity as Activity;
            context.Call(new SubtractDialog(result), EndDialog); 
        }

        [MethodBind]
        [ScorableGroup(2)]
        public async Task Default(IDialogContext context, IActivity activity)
        {
            // Send message
            await context.PostAsync("I'm sorry, but I didn't understand.");
            context.Done<object>(null); 
        }

        public async Task EndDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}