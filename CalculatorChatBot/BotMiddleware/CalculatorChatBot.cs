namespace CalculatorChatBot.BotMiddleware
{
    using System;
    using System.Diagnostics;
    using Microsoft.Azure;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Connector.Teams;
    using System.Threading.Tasks;
    using Cards;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CalculatorChatBot
    {
        public static async Task WelcomeTeam(ConnectorClient connectorClient, Activity activity, string tenantId, string teamId)
        {
            var botDisplayName = CloudConfigurationManager.GetSetting("BotDisplayName");
            var teamName = await GetTeamNameAsync(connectorClient, teamId);
            var welcomeTeamMessageCard = WelcomeTeamAdaptiveCard.GetCard(teamName, botDisplayName);
            await NotifyTeam(connectorClient, welcomeTeamMessageCard, teamId);
        }

        private static async Task<string> GetTeamNameAsync(ConnectorClient connectorClient, string teamId)
        {
            var teamsConnectorClient = connectorClient.GetTeamsConnectorClient();
            var teamDetailsResult = await teamsConnectorClient.Teams.FetchTeamDetailsAsync(teamId);
            return teamDetailsResult.Name;
        }

        private static async Task NotifyTeam(ConnectorClient connectorClient, string cardToSend, string teamId)
        {
            try
            {
                var welcomeTeamReplyActivity = new Activity()
                {
                    Type = ActivityTypes.Message,
                    Conversation = new ConversationAccount()
                    {
                        Id = teamId
                    }, 
                    Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = JsonConvert.DeserializeObject(cardToSend)
                        }
                    }
                };

                await connectorClient.Conversations.SendToConversationAsync(welcomeTeamReplyActivity);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"I have hit a snag: {ex.InnerException.ToString()}");
            }
        }
    }
}