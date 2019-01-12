namespace CalculatorChatBot.Dialogs.Statistics
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Serializable]
    public class MedianDialog : IDialog<object>
    {
        #region Dialog properties
        public string InputString { get; set; }
        public string[] InputStringArray { get; set; }
        public int[] InputInts { get; set; }
        #endregion

        public MedianDialog(Activity incomingActivity)
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
            // Performing some type of validation on the incoming data
            if (InputInts.Length > 2)
            {
                decimal median;
                int size = InputInts.Length;
                int[] copyArr = InputInts;

                // Sorting the array
                Array.Sort(copyArr);

                if (size % 2 == 0)
                {
                    median = Convert.ToDecimal(copyArr[size / 2 - 1] + copyArr[size / 2]) / 2;
                }
                else
                {
                    median = Convert.ToDecimal(copyArr[(size - 1) / 2]);
                }

                #region Building out the results object and the card
                var opsResult = new OperationResults()
                {
                    Input = InputString,
                    Output = decimal.Round(median, 2).ToString(), 
                    OutputMsg = $"Given the list: {InputString}; the median = {decimal.Round(median, 2)}",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Median.ToString()
                };

                IMessageActivity opsReply = context.MakeMessage();
                opsReply.Attachments = new List<Attachment>();


                var operationsCard = new OperationResultsCard(opsResult);
                opsReply.Attachments.Add(operationsCard.ToAttachment());
                #endregion

                await context.PostAsync(opsReply);
            }
            else
            {
                var errorResult = new OperationResults()
                {
                    Input = InputString, 
                    Output = "0", 
                    OutputMsg = $"Please double check the input: {InputString} and try again",
                    OperationType = CalculationTypes.Statistical.ToString(),
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorReply = context.MakeMessage();
                errorReply.Attachments = new List<Attachment>();

                var errorOpsCard = new OperationErrorCard(errorResult);
                errorReply.Attachments.Add(errorOpsCard.ToAttachment());

                await context.PostAsync(errorReply);
            }

            // Making sure to return back to the RootDialog
            context.Done<object>(null);
        }
    }
}