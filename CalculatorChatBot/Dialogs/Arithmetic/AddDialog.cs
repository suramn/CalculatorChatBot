namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;
    using CalculatorChatBot.Models;
    using AdaptiveCards;
    using System.Diagnostics;
    using System.Collections.Generic;
    using CalculatorChatBot.Cards;

    /// <summary>
    /// This class will produce the overall sum of a list of numbers. If the list is too short, the 
    /// bot will reply with an appropriate message.
    /// </summary>
    [Serializable]
    public class AddDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; } 
        #endregion

        public AddDialog(Activity result)
        {
            // Extract the incoming text/message
            string[] incomingInfo = result.Text.Split(' ');

            // What is the properties to be set for the necessary 
            // operation to be performed
            if (!string.IsNullOrEmpty(incomingInfo[1]))
            {
                InputString = incomingInfo[1];

                InputStringArray = InputString.Split(',');

                InputInts = Array.ConvertAll(InputStringArray, int.Parse); 
            }
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (InputInts.Length > 1)
            {
                #region Before using adaptive cards
                int sum = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    sum += InputInts[i];
                }
                #endregion

                var results = new OperationResults()
                {
                    Input = InputString,
                    Output = sum.ToString(),
                    OutputMsg = $"Given the list of {InputString}; the sum = {sum}",
                    OperationType = CalculationTypes.Addition.ToString()
                };

                #region Creating the adaptive card
                IMessageActivity reply = context.MakeMessage();
                reply.Attachments = new List<Attachment>();

                var operationResultsCard = new OperationResultsCard(results);
                reply.Attachments.Add(operationResultsCard.ToAttachment());  
                #endregion

                await context.PostAsync(reply);
            }
            else
            {
                // Building the error results object for the creation of the error card
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    Output = "0", 
                    OutputMsg = $"The input list: {InputString} is too short - please provide more numbers",
                    OperationType = CalculationTypes.Addition.ToString()
                };

                #region Creating the adaptive card
                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorCard.ToAttachment());
                #endregion

                // Send the message that you need more elements to calculate the sum
                await context.PostAsync(errorReply);
            }

            // Return back to the RootDialog - popping this child dialog off the stack
            context.Done<object>(null); 
        }
    }
}