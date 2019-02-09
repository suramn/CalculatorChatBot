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

        public string CalculateQuadraticRoots(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            var resultString = "";

            if (inputIntsArr.Length != 3)
            {
                resultString = "ERROR";
            }
            else
            {
                int a = inputIntsArr[0];
                int b = inputIntsArr[1];
                int c = inputIntsArr[2];

                // TODO: Make sure to complete this here
                // 1. Have the necessary logic to calculate the discriminant
                // 2. Have the necessary logic to use the switch mechanism to return the roots
            }

            return resultString; 
        }

        public string CalculatePythagoreanTriple(string inputString)
        {
            string[] inputStringArr = inputString.Split(',');
            int[] inputIntsArr = Array.ConvertAll(inputStringArr, int.Parse);

            var resultString = "";

            if (inputIntsArr.Length == 2)
            {
                int a = inputIntsArr[0];
                int b = inputIntsArr[1];

                double c = CalculateHypotenuse(a, b);

                resultString = $"{a}, {b}, {decimal.Round(decimal.Parse(c.ToString()), 2)}";
            }
            else
            {
                resultString = "ERROR";
            }

            return resultString;
        }

        private double CalculateHypotenuse(int a, int b)
        {
            var cSquared = Math.Pow(a, 2) + Math.Pow(b, 2);
            double c = Math.Sqrt(cSquared);

            return c;
        }
    }
}