using Microsoft.Bot.Builder.Dialogs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector; 

namespace CalculatorChatBot.BotHelpers
{
    public static class MessageHelpers
    {
        public static async Task SendMessage(IDialogContext context, string message)
        {
            await context.PostAsync(message); 
        }

        public static string CreateHelpMessage(string firstLine)
        {
            var sb = new StringBuilder();
            sb.AppendLine(firstLine);
            sb.AppendLine();
            sb.AppendLine("# Here's what I can help you do:");
            sb.AppendLine();
            sb.AppendLine("* Add numbers");
            sb.AppendLine("* Subtract numbers");
            sb.AppendLine("* Multiply numbers");
            sb.AppendLine("* Divide numbers");
            sb.AppendLine();
            sb.AppendLine("# Future functionality would include the computation of basic statistical measures");
            return sb.ToString();
        }
    }
}