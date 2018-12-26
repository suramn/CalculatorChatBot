using Microsoft.Bot.Builder.Dialogs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams.Models;

namespace CalculatorChatBot.BotHelpers
{
    public static class MessageHelpers
    {
        public static async Task SendMessage(IDialogContext context, string message)
        {
            await context.PostAsync(message); 
        }

        public static string CreateHelpMessage(string firstLine)
        {
            var sb = new StringBuilder();
            sb.AppendLine(firstLine);
            sb.AppendLine();
            sb.AppendLine("For anything regarding help, navigate to the help tab");
            return sb.ToString();
        }

        public static async Task SendOneToOneWelcomeMessage(ConnectorClient connector, 
                                                            TeamsChannelData channelData,
                                                            ChannelAccount botAccount, 
                                                            ChannelAccount userAccount, 
                                                            string tenantId)
        {
            // Construct the message here
            string welcomeMessage = CreateHelpMessage($"The team {channelData.Team.Name} has added the Calculator Chat Bot - helping with some basic math stuff.");

            // Create or get existing chat conversation with the user
            var response = connector.Conversations.CreateOrGetDirectConversation(botAccount, userAccount, tenantId);

            // Construct the message to post to the conversation
            Activity newActivity = new Activity
            {
                Text = welcomeMessage, 
                Type = ActivityTypes.Message, 
                Conversation = new ConversationAccount
                {
                    Id = response.Id
                }
            };

            // Finally post the message
            await connector.Conversations.SendToConversationAsync(newActivity);
        }
    }
}