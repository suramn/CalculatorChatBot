namespace CalculatorChatBot.Dialogs.Geometry
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using CalculatorChatBot.Models;
    using CalculatorChatBot.Cards;
    using Newtonsoft.Json;

    [Serializable]
    public class QuadrilateralPerimDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }
        public string InputString { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public QuadrilateralPerimDialog(Activity incomingActivity)
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
            if (InputInts.Length != 4)
            {
                var errorResultType = ResultTypes.Error;
                var errorResult = new OperationResults()
                {
                    Input = InputString, 
                    NumericalResult = "0", 
                    OutputMsg = $"Your input: {InputString} is not valid. Please try again later!",
                    OperationType = operationType.GetDescription(),
                    ResultType = errorResultType.GetDescription()
                };

                IMessageActivity errorReply = context.MakeMessage();
                var errorAdaptiveCard = OperationErrorAdaptiveCard.GetCard(errorResult);
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
            else
            {
                // Looking to see if all the values are the same
                var isSquare = InputInts[0] == InputInts[1] && InputInts[1] == InputInts[2] && InputInts[2] == InputInts[3];

                if (!isSquare)
                {
                    var totalPerimeter = InputInts.Sum();
                    var totalPerimResultType = ResultTypes.QuadPerimeter;

                    var totalPerimResult = new OperationResults()
                    {
                        Input = InputString,
                        NumericalResult = totalPerimeter.ToString(), 
                        OperationType = operationType.GetDescription(), 
                        OutputMsg = $"Given the input list: {InputString}, the perimeter = {totalPerimeter}",
                        ResultType = totalPerimResultType.GetDescription()
                    };

                    IMessageActivity perimeterReply = context.MakeMessage();
                    var totalPerimAdaptiveCard = OperationResultsAdaptiveCard.GetCard(totalPerimResult);
                    perimeterReply.Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = JsonConvert.DeserializeObject(totalPerimAdaptiveCard)
                        }
                    };

                    await context.PostAsync(perimeterReply);
                }
                else
                {
                    var squarePerimeter = 4 * InputInts[0];
                    var totalPerimResultType = ResultTypes.QuadPerimeter;

                    var squarePerimResult = new OperationResults()
                    {
                        Input = InputString,
                        NumericalResult = squarePerimeter.ToString(),
                        OperationType = operationType.GetDescription(),
                        OutputMsg = $"Given the input list: {InputString}, the perimeter = {squarePerimeter}",
                        ResultType = totalPerimResultType.GetDescription()
                    };

                    IMessageActivity squarePerimReply = context.MakeMessage();
                    var squarePerimAdaptiveCard = OperationResultsAdaptiveCard.GetCard(squarePerimResult);
                    squarePerimReply.Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = JsonConvert.DeserializeObject(squarePerimAdaptiveCard)
                        }
                    };

                    await context.PostAsync(squarePerimReply);
                }
            }
        }
    }
}