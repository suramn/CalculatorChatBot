namespace CalculatorChatBot.Cards
{
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Connector;
    using System.Collections.Generic;
    using AdaptiveCards;

    public class OperationErrorCard
    {
        private OperationResults results;

        public OperationErrorCard(OperationResults results)
        {
            this.results = results;
        }

        public Attachment ToAttachment()
        {
            var card = new AdaptiveCard
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Text = "Error",
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
                        Color = AdaptiveTextColor.Attention,
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
                        Text = $"Output Message: {results.OutputMsg}",
                        Size = AdaptiveTextSize.Default,
                        Wrap = true
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