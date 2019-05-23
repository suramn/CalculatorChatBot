namespace CalculatorChatBot.Dialogs.Geometry
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
    public class PythagoreanDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public PythagoreanDialog(Activity incomingActivity)
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
            if (InputInts.Length > 2)
            {
                var errorResultType = ResultTypes.Error;
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = $"The input list: {InputString} is too long. I need only 2 numbers to find the length of the hypotenuse",
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
                // Having the necessary calculations done
                double a = Convert.ToDouble(InputInts[0]);
                double b = Convert.ToDouble(InputInts[1]);

                var hypotenuseSqr = Math.Pow(a, 2) + Math.Pow(b, 2);

                double c = Math.Sqrt(hypotenuseSqr);

                var output = $"Given the legs of ${InputInts[0]} and ${InputInts[1]}, the hypotenuse of the right triangle is ${decimal.Round(decimal.Parse(c.ToString()), 2)}";

                var resultType = ResultTypes.Hypotenuse;
                var successResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = decimal.Round(decimal.Parse(c.ToString()), 2).ToString(), 
                    OutputMsg = output,
                    OperationType = operationType.GetDescription(),
                    ResultType = resultType.GetDescription()
                };

                IMessageActivity successReply = context.MakeMessage();
                var resultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(successResults);
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

            // Returning back to the root dialog
            context.Done<object>(null);
        }
    }
}