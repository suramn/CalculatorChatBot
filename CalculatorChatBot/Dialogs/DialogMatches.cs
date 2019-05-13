namespace CalculatorChatBot.Dialogs
{
    public static class DialogMatches
    {
        public const string HelpDialogMatch = "help";

        #region Hello World Dialog Matches
        public const string HelloDialogMatch = "hello";
        public const string HiDialogMatch = "hi";
        public const string GreetEveryoneDialogMatch = "greet everyone";
        #endregion

        #region Arithmetical Dialog Matches
        public const string AdditionDialogMatch = "addition";
        public const string AddDialogMatch = "add";
        public const string SumDialogMatch = "sum";

        public const string SubtractDialogMatch = "subtract";
        public const string SubtractionDialogMatch = "subtraction";
        public const string DifferenceDialogMatch = "difference";

        public const string MultiplyDialogMatch = "multiply";
        public const string MultiplicationDialogMatch = "multiplication";
        public const string ProductDialogMatch = "product";

        public const string QuotientDialogMatch = "quotient";
        public const string DivisionDialogMatch = "division";
        public const string DivideDialogMatch = "divide";

        public const string RemainderDialogMatch = "remainder";
        public const string ModuloDialogMatch = "modulo";
        public const string ModulusDialogMatch = "modulus";
        #endregion

        #region Statistical Dialog Matches
        public const string AverageDialogMatch = "average";
        public const string MeanDialogMatch = "mean";
        public const string GeometricMeanDialogMatch = "geomean";
        public const string MedianDialogMatch1 = "median";
        public const string ModeDialogMatch1 = "mode";
        public const string RangeDialogMatch1 = "range";
        public const string VarianceDialogMatch = "variance";
        public const string StandardDeviationDialogMatch1 = "stddev";
        public const string RmsDialogMatch = "rms";
        #endregion

        #region Geometrical Dialog Matches
        public const string PythagorasDialogMatch = "pythagoras";
        public const string PythagoreanDialogMatch = "pythagorean";
        public const string NumberOfRootsDialogMatch = "numRoots";
        public const string DiscriminantDialogMatch = "discriminant";
        public const string EquationRootsDialogMatch = "roots";
        public const string QuadraticSolverDialogMatch = "quadratic";
        public const string MidPointDialogMatch = "midpoint";
        public const string DistanceDialogMatch = "distance";
        #endregion
    }
}