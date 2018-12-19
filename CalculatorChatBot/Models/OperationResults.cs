namespace CalculatorChatBot.Models
{
    public enum CalculationTypes
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Mean,
        Median,
        Mode
    }

    public class OperationResults
    {
        public string OperationType { get; set; }
        
        public string Input { get; set; }

        public string Output { get; set; }

        public string OutputMsg { get; set; }
    }
}