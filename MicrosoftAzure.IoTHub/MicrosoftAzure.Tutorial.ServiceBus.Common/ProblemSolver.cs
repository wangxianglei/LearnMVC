using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftAzure.Tutorial.ServiceBus.Common
{
    public class ProblemSolver: IProblemSolver
    {
        public int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
