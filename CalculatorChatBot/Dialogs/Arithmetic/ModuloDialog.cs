namespace CalculatorChatBot.Dialogs.Arithmetic
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
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

            if (InputInts.Length == 2 && InputInts[1] != 0)
            {
                int remainder = InputInts[0] % InputInts[1];

                var results = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = remainder.ToString(), 
                    OutputMsg = $"Given the list {InputString}; the remainder = {remainder}",
                    OperationType = CalculationTypes.Arithmetic.ToString(),
                    ResultType = ResultTypes.Remainder.ToString()
                };

                // Building up the adaptive card
                IMessageActivity reply = context.MakeMessage();
                reply.Attachments = new List<Attachment>();

                var operationResultsCard = new OperationResultsCard(results);
                reply.Attachments.Add(operationResultsCard.ToAttachment());
                
                await context.PostAsync(reply);
            }
            else
            {
                // Building the error results object
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = $"The list: {InputString} may be invalid for this operation. Please double check, and try again",
                    OperationType = CalculationTypes.Arithmetic.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                // Now having the card
                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            // Return to the RootDialog - making sure to pop this child dialog off the stack
            context.Done<object>(null);
        }
    }
}