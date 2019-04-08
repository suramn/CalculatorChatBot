namespace CalculatorChatBot.Cards
{
    using AdaptiveCards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Connector;
    using System.Collections.Generic;

    public class OperationResultsCard
    {
        private OperationResults results;

        public OperationResultsCard(OperationResults opResults)
        {
            this.results = opResults;
        }

        public Attachment ToAttachment()
        {
            var card = new AdaptiveCard
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Text = "Results",
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
                        Color = AdaptiveTextColor.Accent,
                        Weight = AdaptiveTextWeight.Bolder,
                        Separator = true
                    },
                    new AdaptiveTextBlock
                    {
                        Text = $"Operation: {results.OperationType}",
                        Size = AdaptiveTextSize.Default
                    },
                    new AdaptiveTextBlock
                    {
                        Text = $"Input: {results.Input}",
                        Size = AdaptiveTextSize.Default
                    },
                    new AdaptiveTextBlock
                    {
                        Text = $"{results.ResultType}: {results.Output}",
                        Size = AdaptiveTextSize.Default
                    }
                }
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
        }
    }
}