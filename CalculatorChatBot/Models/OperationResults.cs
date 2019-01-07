namespace CalculatorChatBot.Models
{
    public enum CalculationTypes
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Modulo,
        Mean,
        Median,
        Mode,
        Range,
        Pythagorean
    }

    public enum ResultTypes
    {
        Sum, 
        Difference, 
        Product, 
        Quotient, 
        Remainder,
        Average,
        Median, 
        Mode,
        Range,
        Hypotenuse,
        Error
    }

    public class OperationResults
    {
        public string OperationType { get; set; }
        
        public string Input { get; set; }

        public string Output { get; set; }

        public string OutputMsg { get; set; }

        public string ResultType { get; set; }
    }
}