namespace CalculatorChatBot
{
    using CalculatorChatBot.BotMiddleware;
    using CalculatorChatBot.Dialogs;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Diagnostics;
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
        public async Task<IHttpActionResult> PostAsync([FromBody]Activity activity)
        {
            // Confirmation check - if activity is null, do nothing
            if (activity == null)
            {
                return Ok(); 
            }

            // Message activities are generally text messages that are sent from a user.
            if (activity.Type == ActivityTypes.Message)
            {
                return await HandleTextMessageAsync(activity); 
            }
            else
            {
                // This is used to handle many other (some unsupported) types of messages
                return await HandleSystemMessageAsync(activity); 
            }
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
            if (message.Type == ActivityTypes.ConversationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.MessageReaction)
            {
            }

            return Ok();
        }
    }
}