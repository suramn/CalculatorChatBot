namespace CalculatorChatBot.Dialogs.Statistics
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Serializable]
    public class StandardDeviationDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public StandardDeviationDialog(Activity incomingActivity)
        {
            // Parsing through the incoming message text
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // Which properties are being set for the operations to 
            // be performed
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
                int sum = InputInts[0];
                for (int i = 0; i < InputInts.Length; i++)
                {
                    sum += InputInts[i]; 
                }

                var mean = Convert.ToDouble(sum) / InputInts.Length;

                var variance = CalculateVariance(mean, InputInts);

                var standardDev = Math.Sqrt((double)variance);

                var results = new OperationResults()
                {
                    Input = InputString, 
                    Output = standardDev.ToString(),
                    OutputMsg = $"Given the list: {InputString}; the standard deviation = {standardDev}",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.StandardDeviation.ToString()
                };

                // TODO: Complete this code out
            }
            else
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    Output = "0",
                    OutputMsg = "Your list may be too small to calculate the standard deviation. Please try again later",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorOpsCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorOpsCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            // Popping back after the completion of this dialog
            context.Done<object>(null);
        }

        private decimal CalculateVariance(double mean, int[] inputInts)
        {
            double squareDiffs = 0;
            int N = inputInts.Length;

            for (int i = 0; i < inputInts.Length; i++)
            {
                squareDiffs += Math.Pow(Math.Abs(Convert.ToDouble(inputInts[i]) - mean), 2);
            }

            return Convert.ToDecimal(squareDiffs / N);
        }
    }
}