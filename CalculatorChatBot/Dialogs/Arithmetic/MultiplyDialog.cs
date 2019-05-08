namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Serializable]
    public class MultiplyDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public MultiplyDialog(Activity incomingActivity)
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
                int product = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    product *= InputInts[i];
                }

                var results = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = product.ToString(),
                    OutputMsg = $"Given the list of {InputString}; the product = {product}",
                    OperationType = CalculationTypes.Arithmetic.ToString(),
                    ResultType = ResultTypes.Product.ToString()
                };

                #region Creating the adaptive card
                IMessageActivity reply = context.MakeMessage();
                var resultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(results);
                reply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(resultsAdaptiveCard)
                    }
                };
                #endregion

                await context.PostAsync(reply);
            }
            else
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
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