namespace CalculatorChatBot.Dialogs
{
    using CalculatorChatBot.BotHelpers;
    using Microsoft.Bot.Builder.Dialogs;
    using CalculatorChatBot.Dialogs.Arithmetic;
    using CalculatorChatBot.Dialogs.Statistics;
    using Microsoft.Bot.Builder.Scorables;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Connector.Teams.Models;
    using System;
    using System.Threading.Tasks;
    using CalculatorChatBot.Dialogs.Geometry;

    [Serializable]
    public class RootDialog : DispatchDialog
    {
        #region Hello World like functionality
        [RegexPattern(DialogMatches.HelloDialogMatch)]
        [RegexPattern(DialogMatches.HiDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunHelloDialog(IDialogContext context, IActivity activity)
        {
            var helloResult = activity as Activity;
            context.Call(new HelloDialog(helloResult), EndDialog);
        }

        [RegexPattern(DialogMatches.GreetEveryoneDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunGreetDialog(IDialogContext context, IActivity activity)
        {
            var channelData = context.Activity.GetChannelData<TeamsChannelData>();
            if (channelData.Team != null)
            {
                context.Call(new GreetDialog(), EndDialog);
            }
            else
            {
                await context.PostAsync("I'm sorry, you can only do this from within a Team.");
                context.Done<object>(null);
            }
        }
        #endregion

        #region Arithmetic Operations
        [RegexPattern(DialogMatches.AddDialogMatch)]
        [RegexPattern(DialogMatches.AdditionDialogMatch)]
        [RegexPattern(DialogMatches.SumDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunAddDialog(IDialogContext context, IActivity activity)
        {
            var result = activity as Activity;
            context.Call(new AddDialog(result), EndDialog);
        }

        [RegexPattern(DialogMatches.SubtractDialogMatch)]
        [RegexPattern(DialogMatches.SubtractionDialogMatch)]
        [RegexPattern(DialogMatches.DifferenceDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunSubtractDialog(IDialogContext context, IActivity activity)
        {
            var result = activity as Activity;
            context.Call(new SubtractDialog(result), EndDialog);
        }

        [RegexPattern(DialogMatches.ProductDialogMatch)]
        [RegexPattern(DialogMatches.MultiplicationDialogMatch)]
        [RegexPattern(DialogMatches.MultiplyDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunMultiplyDialog(IDialogContext context, IActivity activity)
        {
            var multiResult = activity as Activity;
            context.Call(new MultiplyDialog(multiResult), EndDialog);
        }

        [RegexPattern(DialogMatches.DivideDialogMatch)]
        [RegexPattern(DialogMatches.DivisionDialogMatch)]
        [RegexPattern(DialogMatches.QuotientDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunDivideDialog(IDialogContext context, IActivity activity)
        {
            var divideResult = activity as Activity;
            context.Call(new DivideDialog(divideResult), EndDialog);
        }

        [RegexPattern(DialogMatches.RemainderDialogMatch)]
        [RegexPattern(DialogMatches.ModuloDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunModuloDialog(IDialogContext context, IActivity activity)
        {
            var modResult = activity as Activity;
            context.Call(new ModuloDialog(modResult), EndDialog);
        }
        #endregion

        #region Statistical Operations
        /// <summary>
        /// This function calls the dialog to calculate the mean/average
        /// </summary>
        [RegexPattern(DialogMatches.AverageDialogMatch)]
        [RegexPattern(DialogMatches.MeanDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunAverageDialog(IDialogContext context, IActivity activity)
        {
            var averageActivity = activity as Activity;
            context.Call(new AverageDialog(averageActivity), EndDialog);
        }

        /// <summary>
        /// This will call the dialog to calculate the median
        /// </summary>
        [RegexPattern(DialogMatches.MedianDialogMatch1)]
        [RegexPattern(DialogMatches.MedianDialogMatch2)]
        [ScorableGroup(1)]
        public async Task RunMedianDialog(IDialogContext context, IActivity activity)
        {
            var medianActivity = activity as Activity;
            context.Call(new MedianDialog(medianActivity), EndDialog);
        }

        /// <summary>
        /// This function will call the dialog to calculate the mode
        /// </summary>
        [RegexPattern(DialogMatches.ModeDialogMatch1)]
        [RegexPattern(DialogMatches.ModeDialogMatch2)]
        [ScorableGroup(1)]
        public async Task RunModeDialog(IDialogContext context, IActivity activity)
        {
            var modeActivity = activity as Activity;
            context.Call(new ModeDialog(modeActivity), EndDialog); 
        }

        [RegexPattern(DialogMatches.RangeDialogMatch1)]
        [RegexPattern(DialogMatches.RangeDialogMatch2)]
        [ScorableGroup(1)]
        public async Task RunRangeDialog(IDialogContext context, IActivity activity)
        {
            var rangeActivity = activity as Activity;
            context.Call(new RangeDialog(rangeActivity), EndDialog);
        }

        [RegexPattern(DialogMatches.VarianceDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunVarianceDialog(IDialogContext context, IActivity activity)
        {
            var varianceActivity = activity as Activity;
            context.Call(new VarianceDialog(varianceActivity), EndDialog);
        }

        [RegexPattern(DialogMatches.StandardDeviationDialogMatch1)]
        [RegexPattern(DialogMatches.StandardDeviationDialogMatch2)]
        [ScorableGroup(1)]
        public async Task RunStandardDeviationDialog(IDialogContext context, IActivity activity)
        {
            var standardDevActivity = activity as Activity;
            context.Call(new StandardDeviationDialog(standardDevActivity), EndDialog); 
        }

        [RegexPattern(DialogMatches.GeometricAverageDialogMatch)]
        [RegexPattern(DialogMatches.GeometricMeanDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunGeometricMeanDialog(IDialogContext context, IActivity activity)
        {
            var geometricMeanActivity = activity as Activity;
            context.Call(new GeometricMeanDialog(geometricMeanActivity), EndDialog); 
        }

        [RegexPattern(DialogMatches.RootMeanSquareDialogMatch)]
        [RegexPattern(DialogMatches.RmsDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunRmsDialog(IDialogContext context, IActivity activity)
        {
            var rmsActivity = activity as Activity;
            context.Call(new RmsDialog(rmsActivity), EndDialog);
        }
        #endregion

        #region Geometric Operations
        [RegexPattern(DialogMatches.PythagorasDialogMatch)]
        [RegexPattern(DialogMatches.PythagoreanDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunPythagoreanDialog(IDialogContext context, IActivity activity)
        {
            var pythagResult = activity as Activity;
            context.Call(new PythagoreanDialog(pythagResult), EndDialog);
        }

        [RegexPattern(DialogMatches.NumberOfRootsDialogMatch)]
        [RegexPattern(DialogMatches.DiscriminantDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunDiscriminantDialog(IDialogContext context, IActivity activity)
        {
            var discrimResult = activity as Activity;
            context.Call(new DiscriminantDialog(discrimResult), EndDialog);
        }

        [RegexPattern(DialogMatches.EquationRootsDialogMatch)]
        [RegexPattern(DialogMatches.QuadraticSolverDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunQuadraticSolverDialog(IDialogContext context, IActivity activity)
        {
            var quadSolverResult = activity as Activity;
            context.Call(new QuadraticSolverDialog(quadSolverResult), EndDialog);
        }

        [RegexPattern(DialogMatches.MidPointDialogMatch)]
        [RegexPattern(DialogMatches.MiddleOfLineSegmentDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunMidpointDialog(IDialogContext context, IActivity activity)
        {
            var midPointResult = activity as Activity;
            context.Call(new MidpointDialog(midPointResult), EndDialog); 
        }

        [RegexPattern(DialogMatches.DistanceDialogMatch)]
        [RegexPattern(DialogMatches.DistanceFormulaDialogMatch)]
        [ScorableGroup(1)]
        public async Task RunDistanceDialog(IDialogContext context, IActivity activity)
        {
            var distanceResult = activity as Activity;
            context.Call(new DistanceDialog(distanceResult), EndDialog); 
        }
        #endregion

        #region Generic help
        [RegexPattern(DialogMatches.HelpDialogMatch)]
        [ScorableGroup(1)]
        public async Task GetHelp(IDialogContext context, IActivity activity)
        {
            // Send the generic help message
            await context.PostAsync(MessageHelpers.CreateHelpMessage(""));
            context.Done<object>(null);
        } 
        #endregion

        [MethodBind]
        [ScorableGroup(2)]
        public async Task Default(IDialogContext context, IActivity activity)
        {
            // Send message
            await context.PostAsync("I'm sorry, but I didn't understand.");
            context.Done<object>(null); 
        }

        /// <summary>
        /// This method would always ensure that the RootDialog would be popped off
        /// the DialogStack whenever the bot is shutting down
        /// </summary>
        public async Task EndDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}