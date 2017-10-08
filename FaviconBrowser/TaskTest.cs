using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaviconBrowser
{
    public class TaskTest
    {
        public async Task<int> TaskMethod()
        {
            int foo = 3;

            await Task.Delay(3000);

            return foo;
        }
    }
}
