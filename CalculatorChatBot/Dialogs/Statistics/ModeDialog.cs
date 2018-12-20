namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using CalculatorChatBot.Models;
    using CalculatorChatBot.Cards;

    public class ModeDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public ModeDialog(Activity incomingActivity)
        {
            // Extract the incoming text/message
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            // What is the properties to be set for the necessary 
            // operation to be performed
            if (!string.IsNullOrEmpty(incomingInfo[1]))
            {
                InputString = incomingInfo[1];

                InputStringArray = InputString.Split(',');

                InputInts = Array.ConvertAll(InputStringArray, int.Parse);
            }
        }

        public async Task StartAsync(IDialogContext context)
        {
            // Again making sure to conduct the appropriate things
            if (InputInts.Length > 2)
            {
                List<int> originalList = new List<int>(InputInts);
                List<int> modesList = new List<int>();

                // Using LINQ in the lines below
                var query = from numbers in originalList
                            group numbers by numbers
                                into groupedNumbers
                            select new
                            {
                                Number = groupedNumbers.Key,
                                Count = groupedNumbers.Count()
                            };

                int max = query.Max(g => g.Count);

                if (max == 1)
                {
                    int mode = 0;
                    modesList.Add(mode); 
                }
                else
                {
                    modesList = query.Where(x => x.Count == max).Select(x => x.Number).ToList();
                }

                var outputArray = modesList.ToArray();

                #region Having the results object
                var successResult = new OperationResults()
                {
                    Input = InputString, 
                    Output = outputArray.Length > 1 ? string.Join(",", outputArray) : outputArray[0].ToString(), 
                    OutputMsg = $"Given the list: {InputString}; the mode = {(outputArray.Length > 1 ? string.Join(",", outputArray) : outputArray[0].ToString())}",
                    OperationType = CalculationTypes.Mode.ToString()
                };

                IMessageActivity successReply = context.MakeMessage();
                successReply.Attachments = new List<Attachment>();

                var successCard = new OperationResultsCard(successResult);
                successReply.Attachments.Add(successCard.ToAttachment());
                #endregion

                await context.PostAsync(successReply);
            }
            else
            {
                // Building out the error object, reply and card
                var errorResult = new OperationResults()
                {
                    Input = InputString,
                    Output = "",
                    OutputMsg = $"Please check your input list: {InputString} and try again later",
                    OperationType = CalculationTypes.Mode.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorCard = new OperationErrorCard(errorResult);
                errorReply.Attachments.Add(errorCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            // Returning back to the RootDialog - popping this child dialog off the stack
            context.Done<object>(null);
        }
    }
}