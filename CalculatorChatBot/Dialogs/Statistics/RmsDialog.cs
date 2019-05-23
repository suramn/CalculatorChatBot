namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CalculatorChatBot.Models;
    using CalculatorChatBot.Cards;
    using Newtonsoft.Json;

    [Serializable]
    public class RmsDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public RmsDialog(Activity incomingActivity)
        {
            string[] incomingInfo = incomingActivity.Text.Split(' ');

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
            if (InputInts.Length > 1)
            {
                var sumOfSquares = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    sumOfSquares += (int)Math.Pow(InputInts[i], 2);
                }

                var calculatedResult = Math.Sqrt(sumOfSquares / InputInts.Length);

                var successResType = ResultTypes.RootMeanSquare;
                var success = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = calculatedResult.ToString(),
                    OutputMsg = $"Given the list: {InputString}, the RMS = {calculatedResult}", 
                    OperationType = operationType.GetDescription(), 
                    ResultType = successResType.GetDescription()
                };

                IMessageActivity successReply = context.MakeMessage();
                var resultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(success);
                successReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(resultsAdaptiveCard)
                    }
                };
                await context.PostAsync(successReply);
            }
            else
            {
                var errorResType = ResultTypes.Error;
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0", 
                    OutputMsg = "Your list may be too small to calculate the root mean square. Please try again later", 
                    OperationType = operationType.GetDescription(), 
                    ResultType = errorResType.GetDescription()
                };

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
                await context.PostAsync(errorReply);
            }

            context.Done<object>(null);
        }
    }
}