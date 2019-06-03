namespace CalculatorChatBot.Dialogs.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Newtonsoft.Json;

    [Serializable]
    public class TrianglePerimDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }
        public string InputString { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public TrianglePerimDialog(Activity incomingActivity)
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
            if (InputInts.Length != 3)
            {
                var errorResultType = ResultTypes.Error;
                var errorResult = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OperationType = operationType.GetDescription(), 
                    OutputMsg = $"Your input list: {InputString} is not valid, please check the input list and try again",
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
                var isEquilateral = InputInts[0] == InputInts[1] && InputInts[1] == InputInts[2] && InputInts[0] == InputInts[2];

                // Add all sides - for the scalene and isoceles cases
                if (!isEquilateral)
                {
                    var perimeter = InputInts[0] + InputInts[1] + InputInts[2];
                    var perimResultType = ResultTypes.TrianglePerimeter;
                    var perimResults = new OperationResults()
                    {
                        Input = InputString, 
                        NumericalResult = perimeter.ToString(),
                        OperationType = operationType.GetDescription(),
                        OutputMsg = $"Given the inputs: {InputString}, the perimeter = {perimeter}",
                        ResultType = perimResultType.GetDescription()
                    };

                    IMessageActivity perimSuccessReply = context.MakeMessage();
                    var perimSuccessAdaptiveCard = OperationResultsAdaptiveCard.GetCard(perimResults);
                    perimSuccessReply.Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = JsonConvert.DeserializeObject(perimSuccessAdaptiveCard)
                        }
                    };

                    await context.PostAsync(perimSuccessReply);
                }
                else
                {
                    var equiPerim = 3 * InputInts[0];
                    var perimResultType = ResultTypes.TrianglePerimeter;
                    var perimResults = new OperationResults()
                    {
                        Input = InputString,
                        NumericalResult = equiPerim.ToString(),
                        OperationType = operationType.GetDescription(),
                        OutputMsg = $"Given the inputs: {InputString}, the perimeter = {equiPerim}",
                        ResultType = perimResultType.GetDescription()
                    };

                    IMessageActivity equiPerimSuccessReply = context.MakeMessage();
                    var perimSuccessAdaptiveCard = OperationResultsAdaptiveCard.GetCard(perimResults);
                    equiPerimSuccessReply.Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = JsonConvert.DeserializeObject(perimSuccessAdaptiveCard)
                        }
                    };

                    await context.PostAsync(equiPerimSuccessReply);
                }
            }
        }
    }
}