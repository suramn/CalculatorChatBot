namespace CalculatorChatBot.Cards
{
    using CalculatorChatBot.Models;
    using CalculatorChatBot.Properties;
    using System.IO;
    using System.Web.Hosting;

    public class OperationResultsAdaptiveCard
    {
        private static readonly string CardTemplate;

        static OperationResultsAdaptiveCard()
        {
            var cardJsonFilePath = HostingEnvironment.MapPath("~/Cards/OperationResultsAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard(OperationResults opsResults)
        {
            var operationTypeText = string.Format(Resources.OperationTypeText, opsResults.OperationType);
            var inputLineText = string.Format(Resources.InputLineText, opsResults.Input);
            var outputResultText = ""; 

            return null;
        }
    }
}