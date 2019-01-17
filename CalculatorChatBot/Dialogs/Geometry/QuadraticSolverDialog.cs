namespace CalculatorChatBot.Dialogs.Geometry
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    public class QuadraticSolverDialog : IDialog<object>
    {
        #region Dialog properties
        public string[] InputStringArray { get; set; }

        public string InputString { get; set; }

        public int[] InputInts { get; set; }
        #endregion

        public QuadraticSolverDialog(Activity incomingActivity)
        {
            string[] incomingInfo = incomingActivity.Text.Split(' ');

            if (!string.IsNullOrEmpty(incomingInfo[1]))
            {
                InputString = incomingInfo[1];

                InputStringArray = InputString.Split(',');

                InputInts = Array.ConvertAll(InputStringArray, int.Parse);
            }
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (InputInts.Length > 3)
            {
                await context.PostAsync("Please check your list of numbers - it may be too large");
            }
            else
            {
                double a = Convert.ToDouble(InputInts[0]);
                double b = Convert.ToDouble(InputInts[1]);
                double c = Convert.ToDouble(InputInts[2]);

                // The two roots of the quadratic equation
                double r1, r2;

                var discriminant = Math.Pow(b, 2) - (4 * a * c);

                int m;

                if (a == 0)
                {
                    m = 1;
                }
                else if (discriminant > 0)
                {
                    m = 2;
                }
                else if (discriminant == 0)
                {
                    m = 3;
                }
                else
                {
                    m = 4;
                }

                switch (m)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        await context.PostAsync("Sorry I'm not sure what is going on here");
                        break;
                }
            }

            context.Done<object>(null);
        }
    }
}