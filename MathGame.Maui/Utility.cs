using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame.Maui
{
    class Utility
    {
        internal static int[] GetDivisionNumbers()
        {
            var random = new Random();
            int firstNumber, secondNumber;

            do
            {
                firstNumber = random.Next(1, 99);
                secondNumber = random.Next(1, 99);
            } while (firstNumber % secondNumber != 0 || firstNumber < secondNumber);

            return new int[] { firstNumber, secondNumber };
        }

    }
}
