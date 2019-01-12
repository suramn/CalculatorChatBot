namespace CalculatorChatBot.Models
{
    public enum CalculationTypes
    {
        Arithmetic, 
        Statistical, 
        Geometric
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
        EquationRoots,
        Error,
        Discriminant
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