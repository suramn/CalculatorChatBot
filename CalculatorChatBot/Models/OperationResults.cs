namespace CalculatorChatBot.Models
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Globalization;

    public static class Extensions
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));

                        if (memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null;
        }
    }

    public enum CalculationTypes
    {
        [Description("Arithmetic")]
        Arithmetic,
        [Description("Statistical")]
        Statistical,
        [Description("Geometric")]
        Geometric
    }

    public enum ResultTypes
    {
        [Description("Sum")]
        Sum,
        [Description("Difference")]
        Difference,
        [Description("Product")]
        Product,
        [Description("Quotient")]
        Quotient,
        [Description("Remainder")]
        Remainder,
        [Description("Average")]
        Average,
        [Description("Median")]
        Median,
        [Description("Mode")]
        Mode,
        [Description("Range")]
        Range,
        [Description("Hypotenuse")]
        Hypotenuse,
        [Description("Quadratic Roots")]
        EquationRoots,
        [Description("Error")]
        Error,
        [Description("Discriminant")]
        Discriminant,
        [Description("Variance")]
        Variance,
        [Description("Geometric Mean")]
        GeometricMean,
        [Description("Standard Deviation")]
        StandardDeviation,
        [Description("Root Mean Square")]
        RootMeanSquare,
        [Description("Midpoint")]
        Midpoint,
        [Description("Distance")]
        Distance
    }

    public class OperationResults
    {
        public string OperationType { get; set; }

        public string Input { get; set; }

        public string NumericalResult { get; set; }

        public string OutputMsg { get; set; }

        public string ResultType { get; set; }
    }
}