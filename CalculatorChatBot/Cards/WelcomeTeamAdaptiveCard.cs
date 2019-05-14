namespace CalculatorChatBot.Cards
{
    using System.IO;
    using System.Web.Hosting;
    using System.Collections.Generic;
    using CalculatorChatBot.Properties;
    using Microsoft.Azure;
    using System;

    public class WelcomeTeamAdaptiveCard
    {
        private static readonly string CardTemplate;

        static WelcomeTeamAdaptiveCard()
        {
            var cardJsonFilePath = HostingEnvironment.MapPath("~/Cards/WelcomeTeamAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard(string teamName, string botDisplayName)
        {
            var welcomeTeamCardTitleText = Resources.WelcomeTeamCardTitleText;
            var welcomeTeamCardIntroPart1 = string.Format(Resources.WelcomeTeamCardIntroPart1, botDisplayName, teamName);
            var welcomeTeamCardIntroPart2 = Resources.WelcomeTeamCardIntroPart2;
            var tourButtonText = Resources.TourButtonText;
            var welcomeTourTitle = Resources.WelcomeTourTitle;

            var baseDomain = CloudConfigurationManager.GetSetting("AppBaseDomain");
            var htmlUrl = Uri.EscapeDataString($"https:{baseDomain}/Content/tour.html?theme={{theme}}");
            var manifestAppId = CloudConfigurationManager.GetSetting("ManifestAppId");
            var tourUrl = $"https://teams.microsoft.com/l/task/{manifestAppId}?url={htmlUrl}&height=533px&width=600px&title={welcomeTourTitle}";

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeTeamCardTitleText", welcomeTeamCardTitleText },
                { "welcomeTeamCardIntroPart1", welcomeTeamCardIntroPart1 },
                { "welcomeTeamCardIntroPart2", welcomeTeamCardIntroPart2 },
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