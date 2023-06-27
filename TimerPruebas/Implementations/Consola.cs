using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
//using TimerApp.Abstractions;

namespace TimerPruebas.Implementations
{
    // Console is just a thin wrapper for System.Console.
   // envoltura delgada
    public class Consola : IConsola
    {
        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
    }
}
