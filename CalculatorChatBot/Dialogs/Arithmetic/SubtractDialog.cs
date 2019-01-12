namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    /// <summary>
    /// This class will produce the overall difference of a list of numbers. If the list is too short, the 
    /// bot will reply with an appropriate message.
    /// </summary>
    [Serializable]
    public class SubtractDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }

        public string[] InputStringArray { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public SubtractDialog(Activity incomingActivity)
        {
            // Extract the incoming text/message
            string[] incomingInfo = incomingActivity.Text.Split(' ');

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
                int diff = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    diff -= InputInts[i];
                }

                var results = new OperationResults()
                {
                    Input = InputString,
                    Output = diff.ToString(),
                    OutputMsg = $"Given the list of {InputString}; the difference = {diff}",
                    OperationType = CalculationTypes.Arithmetic.ToString(),
                    ResultType = ResultTypes.Difference.ToString()
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
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    Output = "0",
                    OutputMsg = $"The input list: {InputString} is too short - please provide more numbers",
                    OperationType = CalculationTypes.Arithmetic.ToString(),
                    ResultType = ResultTypes.Error.ToString()
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