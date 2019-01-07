namespace CalculatorChatBot.Dialogs.Geometry
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
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

            if (InputInts.Length > 2)
            {
                var errorResults = new OperationResults()
                {
                    Input = InputString, 
                    Output = "0",
                    OutputMsg = $"The input list: {InputString} is too long. I need only 2 numbers to find the length of the hypotenuse",
                    OperationType = CalculationTypes.Pythagorean.ToString(), 
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorCard = new OperationErrorCard(errorResults);
                errorReply.Attachments.Add(errorCard.ToAttachment());

                // Sending out the message with the error card
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

                var successResults = new OperationResults()
                {
                    Input = InputString, 
                    Output = decimal.Round(decimal.Parse(c.ToString()), 2).ToString(), 
                    OutputMsg = output,
                    OperationType = CalculationTypes.Pythagorean.ToString(),
                    ResultType = ResultTypes.Hypotenuse.ToString()
                };

                IMessageActivity successReply = context.MakeMessage();
                successReply.Attachments = new List<Attachment>();

                var successCard = new OperationResultsCard(successResults);
                successReply.Attachments.Add(successCard.ToAttachment());

                await context.PostAsync(successReply);
            }

            // Returning back to the root dialog
            context.Done<object>(null);
        }
    }
}