using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class FibonacciCalc
    {
        public int RecurrenceCalc(int i)
        {
            if (i == 1)
            {
                return 1;
            }
            else if (i == 2)
            {
                return 1;
            }
            else
            {
                return RecurrenceCalc(i - 1) + RecurrenceCalc(i - 2);
            }
        }
    }
}
