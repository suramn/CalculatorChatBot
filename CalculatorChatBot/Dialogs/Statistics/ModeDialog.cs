namespace CalculatorChatBot.Dialogs.Statistics
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

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

                var outputArray = modesList.ToArray().ToString();

                await context.PostAsync($"Given the list: {InputString}; the mode = {outputArray}");
            }
            else
            {
                await context.PostAsync($"Please check your input list: {InputString} and try again later");
            }

            // Returning back to the RootDialog - popping this child dialog off the stack
            context.Done<object>(null);
        }
    }
}