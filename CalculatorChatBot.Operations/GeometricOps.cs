namespace CalculatorChatBot.Operations
{
    using System;

    /// <summary>
    /// This class represents all of the possible operations that this bot could do
    /// that belong to the Geometric category
    /// </summary>
    public class GeometricOps
    {
        /// <summary>
        /// Calculates the discriminant given three integers, a value for A, a value for B, and 
        /// a value for C
        /// </summary>
        /// <param name="inputString">The three integers</param>
        /// <returns>An integer</returns>
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

        /// <summary>
        /// Given the values of A, B, and C this function will then calculate the necessary
        /// roots of for the equation Ax^2+Bx+C = 0
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Given the values of two legs in a right triangle, this function will actually
        /// calculate the value of the hypotenuse
        /// </summary>
        /// <param name="inputString">The values of the two legs</param>
        /// <returns>A string that would list out the Pythagorean Triple</returns>
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

        /// <summary>
        /// Method to calculate the hypotenuse
        /// </summary>
        /// <param name="a">First leg of the right triangle</param>
        /// <param name="b">Second leg of the right triangle</param>
        /// <returns>A double value that represents the hypotenuse</returns>
        private double CalculateHypotenuse(int a, int b)
        {
            var cSquared = Math.Pow(a, 2) + Math.Pow(b, 2);
            double c = Math.Sqrt(cSquared);

            return c;
        }
    }
}