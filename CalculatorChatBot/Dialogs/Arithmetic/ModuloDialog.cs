﻿namespace CalculatorChatBot.Dialogs.Arithmetic
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
    public class ModuloDialog : IDialog<object>
    {
        #region Dialog Properties
        public string InputString { get; set; }

        public string[] InputStringArray { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public ModuloDialog(Activity result)
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

            var operationType = CalculationTypes.Arithmetic;
            if (InputInts.Length == 2 && InputInts[1] != 0)
            {
                int remainder = InputInts[0] % InputInts[1];

                var results = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = remainder.ToString(), 
                    OutputMsg = $"Given the list {InputString}; the remainder = {remainder}",
                    OperationType = operationType.GetDescription(),
                    ResultType = ResultTypes.Remainder.ToString()
                };

                // Building up the adaptive card
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
           
                await context.PostAsync(reply);
            }
            else
            {
                var errorResultType = ResultTypes.Error;
                // Building the error results object
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = $"The list: {InputString} may be invalid for this operation. Please double check, and try again",
                    OperationType = operationType.GetDescription(),
                    ResultType = errorResultType.GetDescription()
                };

                // Now having the card
                IMessageActivity errorReply = context.MakeMessage();
                var errorAdaptiveCard = OperationErrorAdaptiveCard.GetCard(errorResults);
                errorReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(errorAdaptiveCard)
                    }
                };
                await context.PostAsync(errorReply);
            }

            // Return to the RootDialog - making sure to pop this child dialog off the stack
            context.Done<object>(null);
        }
    }
}