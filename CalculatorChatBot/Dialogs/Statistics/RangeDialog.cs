﻿namespace CalculatorChatBot.Dialogs.Statistics
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Serializable]
    public class RangeDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public RangeDialog(Activity incomingActivity)
        {
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // Setting the properties accordingly
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

            var operationType = CalculationTypes.Statistical;

            if (InputInts.Length >= 2)
            {
                var inputIntMax = InputInts.Max();

                var inputIntMin = InputInts.Min();

                // Conduct the range calculation as max - min
                var range = inputIntMax - inputIntMin;

                var successResType = ResultTypes.Range;
                var successResult = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = range.ToString(),
                    OutputMsg = $"Given the list: {InputString}; the range = {range}",
                    OperationType = operationType.GetDescription(),
                    ResultType = successResType.GetDescription()
                };

                #region Having the adaptive card created
                IMessageActivity reply = context.MakeMessage();
                var resultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(successResult);
                reply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(resultsAdaptiveCard)
                    }
                };
                #endregion

                // Sending out the reply with the card
                await context.PostAsync(reply);
            }
            else
            {
                var errorResType = ResultTypes.Error;
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = "The list may be too short, try again with more numbers.",
                    OperationType = operationType.GetDescription(),
                    ResultType = errorResType.GetDescription()
                };

                #region Having the adaptive card created
                IMessageActivity errorReply = context.MakeMessage();
                var errorReplyAdaptiveCard = OperationErrorAdaptiveCard.GetCard(errorResults);
                errorReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(errorReplyAdaptiveCard)
                    }
                };
                #endregion

                // Sending the error card
                await context.PostAsync(errorReply);
            }

            // Popping back to the root dialog
            context.Done<object>(null);
        }
    }
}