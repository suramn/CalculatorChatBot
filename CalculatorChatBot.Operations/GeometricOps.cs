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

                double r1, r2, discriminant;
                int m;

                discriminant = Math.Pow(b, 2) - (4 * a * c);

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
                        resultString = "Not a quadratic equation, linear equation";
                        break;
                    case 2:
                        r1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                        r2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                        resultString = $"{r1.ToString()}, {r2.ToString()}";
                        break;
                    case 3:
                        r1 = r2 = (-b) / (2 * a);
                        resultString = r1.ToString();
                        break;
                    case 4:
                        r1 = (-b) / (2 * a);
                        r2 = Math.Sqrt(-discriminant) / (2 * a);
                        resultString = string.Format("{0:#.##} + i {1:#.##}", r1, r2) + "," + string.Format("{0:#.##} - i {1:#.##}", r1, r2);
                        break;
                    default:
                        resultString = "ERROR";
                        break;
                }
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