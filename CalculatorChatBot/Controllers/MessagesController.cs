namespace CalculatorChatBot
{
    using CalculatorChatBot.BotHelpers;
    using CalculatorChatBot.BotMiddleware;
    using CalculatorChatBot.Dialogs;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Connector.Teams;
    using Microsoft.Bot.Connector.Teams.Models;
    using Polly;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [HttpPost]
        [Route("api/messages")]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            using (var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    await HandleTextMessageAsync(activity);
                }
                else
                {
                    await HandleSystemMessageAsync(activity);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task<IHttpActionResult> HandleTextMessageAsync(Activity activity)
        {
            // This is used for removing the '@botName' from the incoming message so it
            // can be parsed correctly
            var messageActivity = StripBotAtMentions.StripAtMentionText(activity);
            try
            {
                // This sends all messages to the RootDialog for processing.
                await Conversation.SendAsync(messageActivity, () => new RootDialog());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return Ok();
        }

        private async Task<IHttpActionResult> HandleSystemMessageAsync(Activity message)
        {
            TeamEventBase eventData = message.GetConversationUpdateData();
            switch (eventData.EventType)
            {
                case TeamEventType.MembersAdded:
                    var connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    connector.SetRetryPolicy(
                        RetryHelpers.DefaultPolicyBuilder.WaitAndRetryAsync(new[] {
                            TimeSpan.FromSeconds(2),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(10)
                        }));

                    var tenantId = message.GetTenantId();
                    var botAccount = message.Recipient;
                    var channelData = message.GetChannelData<TeamsChannelData>();

                    // If the bot is in the collection of added members, then send the welcome message
                    // to all team members
                    if (message.MembersAdded.Any(m => m.Id.Equals(botAccount.Id)))
                    {
                        // Fetch members in the current conversation
                        IList<ChannelAccount> channelAccount = await connector.Conversations.GetConversationMembersAsync(message.Conversation.Id);
                        IEnumerable<TeamsChannelAccount> members = channelAccount.AsTeamsChannelAccounts();

                        // Send a OneToOne message to each other
                        foreach (TeamsChannelAccount member in members)
                        {
                            await MessageHelpers.SendOneToOneWelcomeMessage(connector, channelData, botAccount, member, tenantId);
                        }
                    }
                    else
                    {
                        // Send the OneToOne message to new members
                        foreach (TeamsChannelAccount member in message.MembersAdded.AsTeamsChannelAccounts())
                        {
                            await MessageHelpers.SendOneToOneWelcomeMessage(connector, channelData, botAccount, member, tenantId);
                        }
                    }
                    break;
                case TeamEventType.MembersRemoved:
                    break;
                case TeamEventType.ChannelCreated:
                    break;
                case TeamEventType.ChannelDeleted:
                    break;
                case TeamEventType.ChannelRenamed:
                    break;
                case TeamEventType.TeamRenamed:
                    break;
                default:
                    break;
            }

            return Ok();
        }
    }
}