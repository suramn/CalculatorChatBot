namespace CalculatorChatBot.Cards
{
    using CalculatorChatBot.Properties;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;

    public class WelcomeUserAdaptiveCard
    {
        private static readonly string CardTemplate;

        static WelcomeUserAdaptiveCard()
        {
            var cardJsonFilePath = HostingEnvironment.MapPath("~/Cards/WelcomeUserAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard()
        {
            var welcomeUserCardTitleText = Resources.WelcomeUserCardTitleText;
            var welcomeUserCardIntroPart1 = Resources.WelcomeUserCardIntroPart1;
            var welcomeUserCardIntroPart2 = Resources.WelcomeUserCardIntroPart2;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeUserCardTitleText", welcomeUserCardTitleText },
                { "welcomeUserCardIntroPart1", welcomeUserCardIntroPart1 },
                { "welcomeUserCardIntroPart2", welcomeUserCardIntroPart2 }
            };

            var cardBody = CardTemplate;
            foreach (var kvp in variablesToValues)
            {
                cardBody = cardBody.Replace($"%{kvp.Key}%", kvp.Value);
            }

            return cardBody;
        }
    }
}