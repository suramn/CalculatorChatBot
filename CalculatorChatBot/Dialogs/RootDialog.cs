namespace CalculatorChatBot.Dialogs
{
    using CalculatorChatBot.BotHelpers;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Scorables;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Connector.Teams.Models;
    using System;
    using System.Threading.Tasks;

    [Serializable]
    public class RootDialog : DispatchDialog
    {
        #region Hello World like functionality
        [RegexPattern("hello")]
        [RegexPattern("hi")]
        [ScorableGroup(1)]
        public async Task RunHelloDialog(IDialogContext context, IActivity activity)
        {
            var helloResult = activity as Activity;
            context.Call(new HelloDialog(helloResult), EndDialog);
        }

        [RegexPattern("greet everyone")]
        [ScorableGroup(1)]
        public async Task RunGreetDialog(IDialogContext context, IActivity activity)
        {
            var channelData = context.Activity.GetChannelData<TeamsChannelData>();
            if (channelData.Team != null)
            {
                context.Call(new GreetDialog(), EndDialog);
            }
            else
            {
                await context.PostAsync("I'm sorry, you can only do this from within a Team.");
                context.Done<object>(null);
            }
        }
        #endregion

        #region Arithmetic Operations
        [RegexPattern("add")]
        [RegexPattern("sum")]
        [ScorableGroup(1)]
        public async Task RunAddDialog(IDialogContext context, IActivity activity)
        {
            var result = activity as Activity;
            context.Call(new AddDialog(result), EndDialog);
        }

        [RegexPattern("subtract")]
        [RegexPattern("difference")]
        [ScorableGroup(1)]
        public async Task RunSubtractDialog(IDialogContext context, IActivity activity)
        {
            var result = activity as Activity;
            context.Call(new SubtractDialog(result), EndDialog);
        }

        [RegexPattern("product")]
        [RegexPattern("multiply")]
        [ScorableGroup(1)]
        public async Task RunMultiplyDialog(IDialogContext context, IActivity activity)
        {
            var multiResult = activity as Activity;
            context.Call(new MultiplyDialog(multiResult), EndDialog);
        }

        [RegexPattern("divide")]
        [RegexPattern("quotient")]
        [ScorableGroup(1)]
        public async Task RunDivideDialog(IDialogContext context, IActivity activity)
        {
            var divideResult = activity as Activity;
            context.Call(new DivideDialog(divideResult), EndDialog);
        }
        #endregion

        #region Statistical Operations
        [RegexPattern("average")]
        [RegexPattern("mean")]
        [ScorableGroup(1)]
        public async Task RunAverageDialog(IDialogContext context, IActivity activity)
        {
            var meanResult = activity as Activity;
            context.Call(new AverageDialog(meanResult), EndDialog);
        }
        #endregion

        [RegexPattern("help")]
        [ScorableGroup(1)]
        public async Task GetHelp(IDialogContext context, IActivity activity)
        {
            // Send the generic help message
            await context.PostAsync(MessageHelpers.CreateHelpMessage(""));
            context.Done<object>(null); 
        }

        [MethodBind]
        [ScorableGroup(2)]
        public async Task Default(IDialogContext context, IActivity activity)
        {
            // Send message
            await context.PostAsync("I'm sorry, but I didn't understand.");
            context.Done<object>(null); 
        }

        /// <summary>
        /// This method would always ensure that the RootDialog would be popped off
        /// the DialogStack whenever the bot is shutting down
        /// </summary>
        public async Task EndDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}