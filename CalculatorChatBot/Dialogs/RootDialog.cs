using CalculatorChatBot.BotHelpers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace CalculatorChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Strip out the mentions - As all channel messages to a bot must @mention the bot itself, 
            // you must strip out the bot name at the very least. This would utilize the SDK function
            // GetTextWithoutMentions to strip all the mentions
            var text = activity.Text;

            if (!string.IsNullOrEmpty(text))
            {
                // Check for the following commands - which would represent the four basic operations
                // This method of text parsing would then assume that the command is the first token, 
                // and then the parameters are the remainder
                var split = text.Split(' ');

                // If the user is asking to conduct anything below
                if (split.Length >= 2)
                {
                    var baseCmd = split[0].ToLower();
                    var parameters = split[1].ToString();

                    #region Commands
                    if (baseCmd.Contains("add"))
                    {
                        await ArithmeticHandlers.HandleAddCommand(context, parameters); 
                    }
                    else if (baseCmd.Contains("subtract"))
                    {
                        await ArithmeticHandlers.HandleSubtractCommand(context, parameters);
                    }
                    else if (baseCmd.Contains("multiply"))
                    {
                        await ArithmeticHandlers.HandleMultiplyCommand(context, parameters);
                    }
                    else if (baseCmd.Contains("divide"))
                    {
                        await ArithmeticHandlers.HandleDivideCommand(context, parameters);
                    }
                    #endregion
                }
                else if (text.Contains("help"))
                {
                    await MessageHelpers.SendMessage(context, MessageHelpers.CreateHelpMessage("Sure I can provide some help about me."));
                }
                else
                {
                    await MessageHelpers.SendMessage(context, "I'm sorry, I did not understand you :(");
                }
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}