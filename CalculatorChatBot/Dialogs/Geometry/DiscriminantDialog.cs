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
    public class DiscriminantDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public DiscriminantDialog(Activity incomingActivity)
        {
            // Parsing through the incoming information
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // Setting all of the properties
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
            if (InputInts.Length > 3)
            {
                var errorListTooLongResType = ResultTypes.Error;
                // Error condition here
                var errorListTooLongResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "DNE",
                    OutputMsg = $"The input list: {InputString} could be too long - there needs to be 3 numbers exactly",
                    OperationType = operationType.GetDescription(), 
                    ResultType = errorListTooLongResType.GetDescription()
                };

                IMessageActivity errorListTooLongReply = context.MakeMessage();
                var errorListTooLongAdaptiveCard = OperationErrorAdaptiveCard.GetCard(errorListTooLongResults);
                errorListTooLongReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive", 
                        Content = JsonConvert.DeserializeObject(errorListTooLongAdaptiveCard)
                    }
                };
                await context.PostAsync(errorListTooLongReply);
            }
            else if (InputInts.Length < 3)
            {
                var errorListTooShortResType = ResultTypes.Error;
                var errorListTooShortResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "DNE",
                    OutputMsg = $"The input list: {InputString} could be too short - there needs to be 3 numbers exactly",
                    OperationType = operationType.GetDescription(),
                    ResultType = errorListTooShortResType.GetDescription()
                };

                IMessageActivity errorListTooShortReply = context.MakeMessage();
                var errorListTooShortAdaptiveCard = OperationErrorAdaptiveCard.GetCard(errorListTooShortResults);
                errorListTooShortReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(errorListTooShortAdaptiveCard)
                    }
                };
                await context.PostAsync(errorListTooShortReply);
            }
            else
            {
                int a = InputInts[0];
                int b = InputInts[1];
                int c = InputInts[2];

                int discriminantValue = FindDiscriminant(a, b, c);
                var resultMsg = "";
                var resultsType = ResultTypes.Discriminant;

                if (discriminantValue > 0)
                {
                    resultMsg = $"Given your values: a = {a}, b = {b}, c = {c} - the discriminant = {discriminantValue} which means there are 2 roots";
                }
                else if (discriminantValue == 0)
                {
                    resultMsg = $"Given your values: a = {a}, b = {b}, c = {c} - the discriminant = {discriminantValue} which means there is 1 root";
                }
                else
                {
                    resultMsg = $"Given your values: a = {a}, b = {b}, c = {c} - the discriminant = {discriminantValue} which means there are no real roots";
                }

                #region Generate the reply, operation results, card, and send out the message
                var discrimResults = new OperationResults()
                {
                    Input = InputString,
                    OperationType = operationType.GetDescription(),
                    OutputMsg = resultMsg,
                    NumericalResult = discriminantValue.ToString(),
                    ResultType = resultsType.GetDescription()
                };

                IMessageActivity discrimReply = context.MakeMessage();
                var resultsAdaptiveCard = OperationResultsAdaptiveCard.GetCard(discrimResults);
                discrimReply.Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(resultsAdaptiveCard)
                    }
                };

                await context.PostAsync(discrimReply);
                #endregion
            }

            context.Done<object>(null);
        }

        private static int FindDiscriminant(int a, int b, int c)
        {
            int disc = (int)Math.Pow(b, 2) - (4 * a * c);
            return disc;
        }
    }
}