namespace CalculatorChatBot.Dialogs.Statistics
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
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
            if (InputInts.Length >= 2)
            {
                var inputIntMax = InputInts.Max();

                var inputIntMin = InputInts.Min();

                // Conduct the range calculation as max - min
                var range = inputIntMax - inputIntMin;

                var successResult = new OperationResults()
                {
                    Input = InputString,
                    Output = range.ToString(),
                    OutputMsg = $"Given the list: {InputString}; the range = {range}",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Range.ToString()
                };

                #region Having the adaptive card created
                IMessageActivity reply = context.MakeMessage();
                reply.Attachments = new List<Attachment>();

                var operationResultsCard = new OperationResultsCard(successResult);
                reply.Attachments.Add(operationResultsCard.ToAttachment());
                #endregion

                // Sending out the reply with the card
                await context.PostAsync(reply);
            }
            else
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    Output = "0",
                    OutputMsg = "The list may be too short, try again with more numbers.",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                #region Having the adaptive card created
                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorCard.ToAttachment());
                #endregion

                // Sending the error card
                await context.PostAsync(errorReply);
            }

            // Popping back to the root dialog
            context.Done<object>(null);
        }
    }
}