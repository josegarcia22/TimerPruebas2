using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TimerPruebas.Abstractions;

namespace TimerPruebas.Implementations
{
    public class Publisher : IPublisher
    {


            private readonly ITimer m_Timer;
            private readonly IConsola m_Console;

            public Publisher(ITimer timer, IConsola console)
            {
                m_Timer = timer;
                m_Timer.Enabled = true;
                m_Timer.Interval = 1000;
                m_Timer.TimerIntervalElapsed += Handler;

                m_Console = console;
            }

            public void StartPublishing()
            {
                m_Timer.Start();
            }

            public void StopPublishing()
            {
                m_Timer.Stop();
            }

            private void Handler(object sender, DateTime dateTime)
            {
                m_Console.WriteLine(dateTime);
        }


        //Es casi igual que el anterior Publisherexcepto por pequeños cambios.
        //    Ahora tenemos el ITimer
        //    definido como una dependencia que se inyecta
        //    a través del constructor.El resto del código sería el mismo.

        //-------antes...
        //private readonly Timer m_Timer;
        //private readonly IConsola m_Console; // IConsola---> void WriteLine(object value);//?
        //public Publisher(IConsola consola)
        //{
        //    m_Timer = new Timer();
        //    m_Timer.Enabled = true;
        //    m_Timer.Interval = 1000;
        //    m_Timer.Elapsed += Handler;

        //    m_Console = consola;
        //}
        //public void StartPublishing()
        //{
        //    m_Timer.Start();
        //}


        //public void StopPublishing()
        //{
        //    m_Timer.Stop();
        //}

        //private void Handler(object sender, ElapsedEventArgs args)
        //{
        //    m_Console.WriteLine(args.SignalTime);
        //}

    }
}
