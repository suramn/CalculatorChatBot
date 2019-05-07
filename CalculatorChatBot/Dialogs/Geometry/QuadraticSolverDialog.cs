namespace CalculatorChatBot.Dialogs.Geometry
{
    using CalculatorChatBot.Cards;
    using CalculatorChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Serializable]
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
                var errorResults = new OperationResults()
                {
                    Input = InputString,
                    NumericalResult = "0",
                    OutputMsg = "Your list may be too large to calculate the roots. Please try again later!",
                    OperationType = CalculationTypes.Geometric.ToString(), 
                    ResultType = ResultTypes.Error.ToString()
                };

                IMessageActivity errorListReply = context.MakeMessage();
                errorListReply.Attachments = new List<Attachment>();

                var errorListCard = new OperationErrorCard(errorResults);
                errorListReply.Attachments.Add(errorListCard.ToAttachment());

                await context.PostAsync(errorListReply);
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
                        var opsError = new OperationResults()
                        {
                            Input = InputString,
                            NumericalResult = "0",
                            OutputMsg = "The information provided may lead to a linear equation!",
                            OperationType = CalculationTypes.Geometric.ToString(),
                            ResultType = ResultTypes.Error.ToString()
                        };

                        IMessageActivity opsErrorReply = context.MakeMessage();
                        opsErrorReply.Attachments = new List<Attachment>();

                        var opsErrorCard = new OperationErrorCard(opsError);
                        opsErrorReply.Attachments.Add(opsErrorCard.ToAttachment());

                        await context.PostAsync(opsErrorReply);
                        break;
                    case 2:
                        r1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                        r2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

                        var successOps = new OperationResults()
                        {
                            Input = InputString,
                            NumericalResult = $"{r1}, {r2}",
                            OutputMsg = $"The roots are Real and Distinct - Given the list of: {InputString}, the roots are [{r1}, {r2}]", 
                            OperationType = CalculationTypes.Geometric.ToString(),
                            ResultType = ResultTypes.Error.ToString()
                        };

                        IMessageActivity opsSuccessReply = context.MakeMessage();
                        opsSuccessReply.Attachments = new List<Attachment>();

                        var opsSuccessCard = new OperationResultsCard(successOps);
                        opsSuccessReply.Attachments.Add(opsSuccessCard.ToAttachment());

                        await context.PostAsync(opsSuccessReply);
                        break;
                    case 3:
                        r1 = r2 = (-b) / (2 * a);
                        
                        var successOpsOneRoot = new OperationResults()
                        {
                            Input = InputString,
                            NumericalResult = $"{r1}, {r2}",
                            OutputMsg = $"The roots are Real and Distinct - Given the list of: {InputString}, the roots are [{r1}, {r2}]",
                            OperationType = CalculationTypes.Geometric.ToString(),
                            ResultType = ResultTypes.Error.ToString()
                        };

                        IMessageActivity opsSuccessOneRoot = context.MakeMessage();
                        opsSuccessOneRoot.Attachments = new List<Attachment>();

                        var opsSuccessOneRootCard = new OperationResultsCard(successOpsOneRoot);
                        opsSuccessOneRoot.Attachments.Add(opsSuccessOneRootCard.ToAttachment());

                        await context.PostAsync(opsSuccessOneRoot);
                        break;
                    case 4:
                        var rootsDesc = "Roots are imaginary";
                        r1 = (-b) / (2 * a);
                        r2 = Math.Sqrt(-discriminant) / (2 * a);

                        var root1Str = string.Format("First root is {0:#.##} + i {1:#.##}", r1, r2);
                        var root2Str = string.Format("Second root is {0:#.##} - i {1:#.##}", r1, r2);

                        var root1 = string.Format("{0:#.##} + i {1:#.##}", r1, r2);
                        var root2 = string.Format("{0:#.##} - i {1:#.##}", r1, r2);

                        var opsSuccessImaginRootsResults = new OperationResults()
                        {
                            Input = InputString,
                            NumericalResult = $"{root1}, {root2}",
                            OutputMsg = rootsDesc + " " + root1Str + " " + root2Str,
                            OperationType = CalculationTypes.Geometric.ToString(), 
                            ResultType = ResultTypes.EquationRoots.ToString()
                        };

                        IMessageActivity opsSuccessImagReply = context.MakeMessage();
                        opsSuccessImagReply.Attachments = new List<Attachment>();

                        var opsSuccessImaginRootCard = new OperationResultsCard(opsSuccessImaginRootsResults);
                        opsSuccessImagReply.Attachments.Add(opsSuccessImaginRootCard.ToAttachment());

                        await context.PostAsync(opsSuccessImagReply);
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