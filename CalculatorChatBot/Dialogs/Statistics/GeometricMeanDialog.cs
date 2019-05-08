namespace CalculatorChatBot.Dialogs.Statistics
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
    public class GeometricMeanDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public GeometricMeanDialog(Activity incomingActivity)
        {
            // Extract the incoming text/message
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // Setting all of the dialog properties
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
                    if (InputInts[i] != 0)
                    {
                        product *= InputInts[i];
                    }
                    else
                    {
                        break;
                    }
                }

                // Calculating the Geometric mean here
                var geometricMean = Math.Pow(product, 1 / InputInts.Length);

                var results = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = geometricMean.ToString(),
                    OutputMsg = $"Given the list: {InputString}; the geometric mean = ${geometricMean.ToString()}",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.GeometricMean.ToString()
                };

                IMessageActivity opsReply = context.MakeMessage();
                var resultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(results);
                opsReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(resultsAdaptiveCard)
                    }
                };
                await context.PostAsync(opsReply); 
            }
            else
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0", 
                    OutputMsg = "Your list may be too small to calculate the geometric mean. Please try again later",
                    OperationType = CalculationTypes.Statistical.ToString(), 
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorOpsCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorOpsCard.ToAttachment());

                await context.PostAsync(errorReply); 
            }

            // Returning back to the RootDialog
            context.Done<object>(null); 
        }
    }
}