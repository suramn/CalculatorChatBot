namespace CalculatorChatBot.Operations
{
    using System;

    public class GeometricOps
    {
        public int CalculateDiscriminant(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            int a = inputIntsArr[0];
            int b = inputIntsArr[1];
            int c = inputIntsArr[2];

            int discriminant = (int)Math.Pow(b, 2) - (4 * a * c);

            return discriminant;
        }

        private int CalculateDiscriminant(int a, int b, int c)
        {
            var result = Math.Pow(b, 2) - (4 * a * c);
            return (int)result;
        }

        public string CalculateQuadraticRoots(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            var resultString = "";

            return resultString; 
        }

        public string CalculatePythagoreanTriple(string inputString)
        {
            var resultString = "bleh";
            return resultString; 
        }
    }
}