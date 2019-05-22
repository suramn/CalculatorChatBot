namespace CalculatorChatBot.Cards
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;
    using CalculatorChatBot.Properties;
    using Microsoft.Azure;

    public class WelcomeUserAdaptiveCard
    {
        private static readonly string CardTemplate;

        static WelcomeUserAdaptiveCard()
        {
            var cardJsonFilePath = HostingEnvironment.MapPath("~/Cards/WelcomeUserAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard(string teamName, string nameOfUserThatJustJoined, string botDisplayName)
        {
            var welcomeUserCardTitleText = Resources.WelcomeUserCardTitleText;
            var welcomeUserCardIntroPart1 = Resources.WelcomeUserCardIntroPart1;
            var welcomeUserCardIntroPart2 = Resources.WelcomeUserCardIntroPart2;
            var tourButtonText = Resources.TourButtonText;
            var welcomeTourTitle = Resources.WelcomeTourTitle;

            var baseDomain = CloudConfigurationManager.GetSetting("AppBaseDomain");
            var htmlUrl = Uri.EscapeDataString($"https:{baseDomain}/Content/tour.html?theme={{theme}}");
            var manifestAppId = CloudConfigurationManager.GetSetting("ManifestAppId");
            var tourUrl = $"https://teams.microsoft.com/l/task/{manifestAppId}?url={htmlUrl}&height=533px&width=600px&title={welcomeTourTitle}";

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeUserCardTitleText", welcomeUserCardTitleText },
                { "welcomeUserCardIntroPart1", welcomeUserCardIntroPart1 },
                { "welcomeUserCardIntroPart2", welcomeUserCardIntroPart2 },
                { "tourButtonText", tourButtonText },
                { "tourUrl", tourUrl }
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