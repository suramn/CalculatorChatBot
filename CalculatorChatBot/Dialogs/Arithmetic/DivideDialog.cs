namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Threading.Tasks;
    using System;
    using CalculatorChatBot.Models;
    using System.Collections.Generic;
    using CalculatorChatBot.Cards;
    using Newtonsoft.Json;

    [Serializable]
    public class DivideDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public DivideDialog(Activity incomingActivity)
        {
            // Parsing through the necessary incoming text
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

            var operationType = CalculationTypes.Arithmetic;
            decimal quotient = 0;
            if (InputInts.Length == 2 && InputInts[1] != 0)
            {
                quotient = Convert.ToDecimal(InputInts[0]) / InputInts[1];
                var resultsType = ResultTypes.Quotient;

                var results = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = decimal.Round(quotient, 2).ToString(),
                    OutputMsg = $"Given the list of {InputString}; the quotient = {decimal.Round(quotient, 2)}",
                    OperationType = operationType.GetDescription(),
                    ResultType = resultsType.GetDescription()
                };

                #region Creating the adaptive card
                IMessageActivity reply = context.MakeMessage();
                var resultsCard = OperationResultsAdaptiveCard.GetCard(results);
                reply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive", 
                        Content = JsonConvert.DeserializeObject(resultsCard)
                    }
                };
                #endregion

                await context.PostAsync(reply);
            }
            else
            {
                var errorType = ResultTypes.Error;
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = "The list may be too long, or one of the elements could be 0 - please try again later.",
                    OperationType = operationType.GetDescription(),
                    ResultType = errorType.GetDescription()
                };

                #region Creating the adaptive card
                IMessageActivity errorReply = context.MakeMessage();
                var errorResultsCard = OperationErrorAdaptiveCard.GetCard(errorResults);
                errorReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(errorResultsCard)
                    }
                };
                #endregion

                // Send the message that you need more elements to calculate the sum
                await context.PostAsync(errorReply);
            }

            // Return back to the root dialog - popping this child dialog from the dialog stack
            context.Done<object>(null);
        }
    }
}