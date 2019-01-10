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

            if (InputInts.Length > 3)
            {
                // Error condition here
                var errorListTooLong = new OperationResults()
                {
                    Input = InputString, 
                    Output = "DNE",
                    OutputMsg = $"The input list: {InputString} could be too long - there needs to be 3 numbers exactly",
                    OperationType = CalculationTypes.Discriminant.ToString(), 
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorListTooLongReply = context.MakeMessage();
                errorListTooLongReply.Attachments = new List<Attachment>();

                var errorListTooLongCard = new OperationErrorCard(errorListTooLong);
                errorListTooLongReply.Attachments.Add(errorListTooLongCard.ToAttachment());

                await context.PostAsync(errorListTooLongReply);
            }
            else if (InputInts.Length < 3)
            {
                var errorListTooShort = new OperationResults()
                {
                    Input = InputString,
                    Output = "DNE",
                    OutputMsg = $"The input list: {InputString} could be too short - there needs to be 3 numbers exactly",
                    OperationType = CalculationTypes.Discriminant.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorListTooShortReply = context.MakeMessage();
                errorListTooShortReply.Attachments = new List<Attachment>();

                var errorListTooShortCard = new OperationErrorCard(errorListTooShort);
                errorListTooShortReply.Attachments.Add(errorListTooShortCard.ToAttachment());

                await context.PostAsync(errorListTooShortReply);
            }
            else
            {

            }

            context.Done<object>(null);
        }
    }
}