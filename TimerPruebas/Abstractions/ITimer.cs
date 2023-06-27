using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TimerPruebas.Abstractions;

namespace TimerPruebas.Abstractions
{
    public delegate void TimerIntervalElapsedEventHandler(object sender, DateTime dateTime);

    public interface ITimer : IDisposable
    {
        event TimerIntervalElapsedEventHandler TimerIntervalElapsed;

        bool Enabled { get; set; }
        double Interval { get; set; }

        void Start();
        void Stop();
    }
}
//What we can notice here:

//We defined the new delegate TimerIntervalElapsedEventHandler.This delegate represents the event to be raised by our ITimer.
//You might argue that we don’t need this new delegate as we already have the native ElapsedEventHandler which is already used by System.Timers.Timer.
//Yes, this is true. However, you would notice that the ElapsedEventHandler event is providing ElapsedEventArgs as the event arguments.This ElapsedEventArgs has a private constructor and you would not be able to create your own instance. Additionally, the SignalTime property defined in the ElapsedEventArgs class is read - only.Therefore, you would not be able to override it in a child class.
//There is a change request ticket opened for Microsoft to update this class but up till the moment of writing this article no changes were applied.
//Also, note that ITimer extends the IDisposable.
//Lo que podemos notar aquí:

//Definimos el nuevo delegado TimerIntervalElapsedEventHandler.
//Este delegado representa el evento que será planteado por nuestro ITimer.
//Puede argumentar que no necesitamos este nuevo delegado ya que tenemos
//el nativo ElapsedEventHandlerque ya usa System.Timers.Timer.

//Sí, es cierto. Sin embargo,
//notará que el ElapsedEventHandlerevento se proporciona ElapsedEventArgs
//como argumentos del evento. Esto ElapsedEventArgs
//tiene un constructor privado y no podría crear su propia instancia.

//Además, la SignalTimepropiedad definida en la ElapsedEventArgsclase es de solo lectura. Por lo tanto, no podrá anularlo en una clase secundaria.
//Hay un ticket de solicitud de cambio abierto para que Microsoft actualice esta clase, pero hasta el momento de escribir este artículo no se aplicaron cambios.
//Además, tenga en cuenta que ITimerextiende el IDisposable.