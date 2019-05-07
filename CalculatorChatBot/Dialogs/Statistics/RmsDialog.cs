namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CalculatorChatBot.Models;
    using CalculatorChatBot.Cards;

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

            if (InputInts.Length > 1)
            {
                var sumOfSquares = InputInts[0];
                for (int i = 1; i < InputInts.Length; i++)
                {
                    sumOfSquares += (int)Math.Pow(InputInts[i], 2);
                }

                var calculatedResult = Math.Sqrt(sumOfSquares / InputInts.Length);

                var success = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = calculatedResult.ToString(),
                    OutputMsg = $"", 
                    OperationType = CalculationTypes.Statistical.ToString(), 
                    ResultType = ResultTypes.RootMeanSquare.ToString()
                };

                IMessageActivity successReply = context.MakeMessage();
                successReply.Attachments = new List<Attachment>();

                var successOpsCard = new OperationResultsCard(success);
                successReply.Attachments.Add(successOpsCard.ToAttachment());

                await context.PostAsync(successReply);
            }
            else
            {
                var error = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0", 
                    OutputMsg = "Your list may be too small to calculate the root mean square. Please try again later", 
                    OperationType = CalculationTypes.Statistical.ToString(), 
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorOpsCard = new OperationErrorCard(error);
                errorReply.Attachments.Add(errorOpsCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            context.Done<object>(null);
        }
    }
}