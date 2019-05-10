namespace CalculatorChatBot.Cards
{
    using System.IO;
    using System.Web.Hosting;
    using System.Collections.Generic;
    using CalculatorChatBot.Properties;

    public class WelcomeTeamAdaptiveCard
    {
        private static readonly string CardTemplate;

        static WelcomeTeamAdaptiveCard()
        {
            var cardJsonFilePath = HostingEnvironment.MapPath("~/Cards/WelcomeTeamAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard()
        {
            var welcomeTeamCardTitleText = Resources.WelcomeTeamCardTitleText;
            var welcomeTeamCardIntroPart1 = Resources.WelcomeTeamCardIntroPart1;
            var welcomeTeamCardIntroPart2 = Resources.WelcomeTeamCardIntroPart2;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeTeamCardTitleText", welcomeTeamCardTitleText },
                { "welcomeTeamCardIntroPart1", welcomeTeamCardIntroPart1 },
                { "welcomeTeamCardIntroPart2", welcomeTeamCardIntroPart2 }
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