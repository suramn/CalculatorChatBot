namespace CalculatorChatBot.Dialogs.Geometry
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Serializable]
    public class MidpointDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public MidpointDialog(Activity incomingActivity)
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

            if (InputInts.Length > 1 && InputInts.Length == 4)
            {
                int x1 = InputInts[0];
                int y1 = InputInts[1];
                int x2 = InputInts[2];
                int y2 = InputInts[3];

                var midX = (x1 + x2) / 2;
                var midY = (y1 + y2) / 2;

                // Successful midpoint calculation results
                var successResults = new OperationResults()
                {
                    Input = InputString, 
                    Output = $"{midX}, {midY}",
                    OutputMsg = $"Given the list of integers: {InputString}, the midpoint = ({midX}, {midY})", 
                    OperationType = CalculationTypes.Geometric.ToString(), 
                    ResultType = ResultTypes.Midpoint.ToString()
                };

                IMessageActivity successReply = context.MakeMessage();
                successReply.Attachments = new List<Attachment>();

                var successOpsCard = new OperationResultsCard(successResults);
                successReply.Attachments.Add(successOpsCard.ToAttachment());

                await context.PostAsync(successReply);
            }
            else
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    Output = "0",
                    OutputMsg = "There needs to be exactly 4 elements to calculate the midpoint. Please try again later",
                    OperationType = CalculationTypes.Geometric.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorOpsCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorOpsCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            // Returning back to the main root dialog stack
            context.Done<object>(null);
        }
    }
}