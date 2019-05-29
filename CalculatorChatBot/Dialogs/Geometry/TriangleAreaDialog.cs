namespace CalculatorChatBot.Dialogs.Geometry
{
    using System;
    using System.Threading.Tasks;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using CalculatorChatBot.Cards;

    [Serializable]
    public class TriangleAreaDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public TriangleAreaDialog(Activity incomingActivity)
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

            var operationType = CalculationTypes.Geometric;
            if (InputInts.Length > 2 && (InputInts[0] == 0 || InputInts[1] == 0))
            {
                var errorResultType = ResultTypes.Error;
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = $"The input list: {InputString} may not be valid. Please try again",
                    OperationType = operationType.GetDescription(),
                    ResultType = errorResultType.GetDescription()
                };

                IMessageActivity errorReply = context.MakeMessage();
                var errorResultsAdaptiveCard = OperationErrorAdaptiveCard.GetCard(errorResults);
                errorReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(errorResultsAdaptiveCard)
                    }
                };

                await context.PostAsync(errorReply);
            }
            else
            {
                var triangleAreaResult = Convert.ToDecimal(0.5 * InputInts[0] * InputInts[1]);

                var successResultType = ResultTypes.TriangleArea;
                var successResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = decimal.Round(triangleAreaResult, 2).ToString(), 
                    OutputMsg = $"Given the inputs: {InputString}, the output = {decimal.Round(triangleAreaResult, 2).ToString()}",
                    OperationType = operationType.GetDescription(), 
                    ResultType = successResultType.GetDescription()
                };

                IMessageActivity successReply = context.MakeMessage();
                var successResultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(successResults);
                successReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(successResultsAdaptiveCard)
                    }
                };

                await context.PostAsync(successReply);
            }
        }
    }
}