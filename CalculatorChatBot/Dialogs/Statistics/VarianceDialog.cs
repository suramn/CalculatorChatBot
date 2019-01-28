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
    public class VarianceDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; } 
        #endregion

        public VarianceDialog(Activity incomingActivity)
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
                int sum = InputInts[0];
                foreach (int item in InputInts)
                {
                    sum += item;
                }

                double mean = Convert.ToDouble(sum) / InputInts.Length;

                decimal variance = CalculateVariance(mean, InputInts);

                #region Building the results 
                var results = new OperationResults()
                {
                    Input = InputString, 
                    Output = decimal.Round(variance, 2).ToString(),
                    OutputMsg = $"Given the list: {InputString}; the variance = {decimal.Round(variance, 2)}",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Variance.ToString()
                };

                IMessageActivity resultsReply = context.MakeMessage();
                resultsReply.Attachments = new List<Attachment>();

                var resultsCard = new OperationResultsCard(results);
                resultsReply.Attachments.Add(resultsCard.ToAttachment());
                #endregion

                await context.PostAsync(resultsReply);
            }
            else
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString, 
                    Output = "0",
                    OutputMsg = "Your list may be too small to calculate the variance. Please try again later.",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorOpsCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorOpsCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            // Popping back to the root dialog
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