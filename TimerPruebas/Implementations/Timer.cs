using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TimerPruebas.Abstractions;

namespace TimerPruebas.Implementations
{


    //El código proporcionado es una implementación de C# de una clase de temporizador. Repasemos el código para entender su funcionalidad:

    //La clase Timer implementa la interfaz ITimer.
    //Tiene un campo privado m_Handlers, que es un diccionario que asigna delegados TimerIntervalElapsedEventHandler a listas de delegados ElapsedEventHandler. Este diccionario se utiliza para realizar un seguimiento de los controladores de eventos asociados con el evento TimerIntervalElapsed.
    //La clase también tiene un campo booleano m_IsDisposed,
    //que se utiliza para indicar si el temporizador se ha eliminado o no.
    //Hay un campo privado m_Timer de tipo System.Timers.Timer, que es el temporizador real que se usa internamente.
    //El constructor inicializa el campo m_Timer creando una nueva instancia de System.Timers.Timer.
    //El evento TimerIntervalElapsed se implementa mediante los accesores de agregar y quitar. Cuando se agrega un controlador de eventos (agregar), se crea un controlador de eventos interno correspondiente y se agrega al diccionario m_Handlers. El controlador de eventos interno simplemente invoca el controlador de eventos original con los argumentos apropiados. El controlador de eventos interno también está suscrito al evento Elapsed de m_Timer. Cuando se elimina (eliminar) un controlador de eventos, se cancela la suscripción del controlador de eventos interno correspondiente del evento Elapsed y se elimina del diccionario m_Handlers.
    //La propiedad Enabled obtiene o establece el valor que indica si el temporizador está habilitado o no.
    //La propiedad Interval obtiene o establece el intervalo en el que el temporizador genera el evento TimerIntervalElapsed.
    //El método Start() inicia el temporizador.
    //El método Stop() detiene el temporizador.
    //El método Dispose() se utiliza para liberar cualquier recurso utilizado por el temporizador. Elimina el m_Timer y cancela la suscripción de los controladores de eventos internos del evento Elapsed. También borra el diccionario m_Handlers.
    //El método Dispose(bool disposing) es llamado por el método Dispose() y es responsable de eliminar los recursos administrados. Comprueba si el temporizador ya se ha eliminado y si hay algún controlador de eventos presente. Si hay controladores de eventos, los cancela del evento Elapsed y elimina el m_Timer.
    //El recolector de basura llama al finalizador (~Timer()) y llama al método Dispose(false) para liberar recursos no administrados.
    //En general, este código proporciona una implementación de temporizador que puede generar el evento TimerIntervalElapsed en un intervalo específico. Admite la adición y eliminación de controladores de eventos, el inicio y la detención del temporizador y la eliminación adecuada de los recursos cuando ya no se necesita el temporizador.


    //---------------------------------------------------------------


    
    //Internally we are using System.Timers.Timer.
    //We applied the IDisposable design pattern.
    //That’s why you can see the private bool m_IsDisposed,
    //public void Dispose(), protected virtual void Dispose(bool disposing), and ~Timer().


    //En la versión 7 de C#, la inicialización de una colección vacía no es compatible
    //directamente en la declaración de una variable.
    //Puedes solucionar este problema inicializando el diccionario en el constructor de la clase
    //en lugar de hacerlo directamente en la declaración del campo.
    //En esta versión, se elimina la inicialización directa del diccionario en la declaración del campo
    //y se realiza en el constructor de la clase.
    //Al hacerlo de esta manera,
    //se evita el error relacionado con la inicialización de una colección vacía en la declaración del camp.

    //Aquí tienes una versión modificada del código para que sea compatible con C# 7:

    public class Timer : ITimer
    {
        private Dictionary<TimerIntervalElapsedEventHandler, List<ElapsedEventHandler>> m_Handlers;
        private bool m_IsDisposed;
        private System.Timers.Timer m_Timer;

        public Timer()
        {//diccionario q asigna delegate intervalos a TImerinterval
            m_Handlers = new Dictionary<TimerIntervalElapsedEventHandler, List<ElapsedEventHandler>>();
            m_Timer = new System.Timers.Timer();//INICIALIZA TIMER (refer to this as the Internal Timer in the rest of the steps)
        }


        //PUNTO 17 --> TIMERINTERVALELAPSED.CS (error por cambiar e inicializar anterior en el constructor dictionary)

        //  And that’s why you can see this private Dictionary<TimerIntervalElapsedEventHandler,
        //  List<ElapsedEventHandler>> m_Handlers = new();.
        //---------------------------------------------------------------------------------------------------
        //private Dictionary<TimerIntervalElapsedEventHandler, List<ElapsedEventHandler>> m_Handlers = new();
        //private bool m_IsDisposed;
        //private System.Timers.Timer m_Timer;

        //public Timer()
        //{
        //    m_Timer = new System.Timers.Timer();
        //}


        //this is the most important part so let’s analyze it step by step.
        public event TimerIntervalElapsedEventHandler TimerIntervalElapsed
        {
            add
            {
                var internalHandler =
                    (ElapsedEventHandler)((sender, args) => { value.Invoke(sender, args.SignalTime); });

                if (!m_Handlers.ContainsKey(value))
                {
                    m_Handlers.Add(value, new List<ElapsedEventHandler>());
                }

                m_Handlers[value].Add(internalHandler);

                m_Timer.Elapsed += internalHandler;
            }

            remove
            {
                m_Timer.Elapsed -= m_Handlers[value].Last();

                m_Handlers[value].RemoveAt(m_Handlers[value].Count - 1);

                if (!m_Handlers[value].Any())
                {
                    m_Handlers.Remove(value);
                }
            }
        }



        //LEER terminar
        //What we need to do with this event, is to handle when someone subscribes/unsubscribes to it from outside. In this case, we want to mirror this to the Internal Timer.
        //In other words, if someone from outside is having an instance of our ITimer, he should be able to do something like this t.TimerIntervalElapsed += (sender, dateTime) => { //do something }.
        //At this moment, what we should do is internally do something like m_Timer.Elapsed += (sender, elapsedEventArgs) => { //do something }.
        //However, we need to keep in mind that the two handlers are not the same as they are actually of different types; TimerIntervalElapsedEventHandler and ElapsedEventHandler.
        //....
        //https://levelup.gitconnected.com/best-practice-for-using-system-timers-timer-in-net-c-867ab6b5027








        // we are just delegating the implementation to the Internal Timer.
        public bool Enabled
        {
            get => m_Timer.Enabled;
            set => m_Timer.Enabled = value;
        }

        public double Interval
        {
            get => m_Timer.Interval;
            set => m_Timer.Interval = value;
        }

        public void Start()
        {
            m_Timer.Start();
        }

        public void Stop()
        {
            m_Timer.Stop();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //El método Dispose() se utiliza para liberar cualquier recurso utilizado por el temporizador.
//Elimina el m_Timer y cancela la suscripción de los controladores de eventos internos del evento Elapsed.
//También borra el diccionario m_Handlers.

//El método Dispose(bool disposing) es llamado por el método Dispose()
//y es responsable de eliminar los recursos administrados.
//Comprueba si el temporizador ya se ha eliminado
//y si hay algún controlador de eventos presente.
//Si hay controladores de eventos, los cancela del evento Elapsed y elimina el m_Timer.
        protected virtual void Dispose(bool disposing)
        {
            if (m_IsDisposed) return;

            if (disposing && m_Handlers.Any())
            {
                foreach (var internalHandlers in m_Handlers.Values)
                {
                    if (internalHandlers?.Any() ?? false)
                    {
                        internalHandlers.ForEach(handler => m_Timer.Elapsed -= handler);
                    }
                }

                m_Timer.Dispose();
                m_Timer = null;
                m_Handlers.Clear();
                m_Handlers = null;
            }

            m_IsDisposed = true;
        }

        ~Timer()
        {
            Dispose(false);
        }
    }
}


//El código proporcionado es una implementación de C# de una clase de temporizador. Repasemos el código para entender su funcionalidad:

//La clase Timer implementa la interfaz ITimer.
//Tiene un campo privado m_Handlers, que es un diccionario que asigna delegados TimerIntervalElapsedEventHandler a listas de delegados ElapsedEventHandler. Este diccionario se utiliza para realizar un seguimiento de los controladores de eventos asociados con el evento TimerIntervalElapsed.
//La clase también tiene un campo booleano m_IsDisposed, que se utiliza para indicar si el temporizador se ha eliminado o no.
//Hay un campo privado m_Timer de tipo System.Timers.Timer, que es el temporizador real que se usa internamente.
//El constructor inicializa el campo m_Timer creando una nueva instancia de System.Timers.Timer.
//El evento TimerIntervalElapsed se implementa mediante los accesores de agregar y quitar. Cuando se agrega un controlador de eventos (agregar), se crea un controlador de eventos interno correspondiente y se agrega al diccionario m_Handlers. El controlador de eventos interno simplemente invoca el controlador de eventos original con los argumentos apropiados. El controlador de eventos interno también está suscrito al evento Elapsed de m_Timer. Cuando se elimina (eliminar) un controlador de eventos, se cancela la suscripción del controlador de eventos interno correspondiente del evento Elapsed y se elimina del diccionario m_Handlers.
//La propiedad Enabled obtiene o establece el valor que indica si el temporizador está habilitado o no.
//La propiedad Interval obtiene o establece el intervalo en el que el temporizador genera el evento TimerIntervalElapsed.
//El método Start() inicia el temporizador.
//El método Stop() detiene el temporizador.
//El método Dispose() se utiliza para liberar cualquier recurso utilizado por el temporizador. Elimina el m_Timer y cancela la suscripción de los controladores de eventos internos del evento Elapsed. También borra el diccionario m_Handlers.
//El método Dispose(bool disposing) es llamado por el método Dispose()

//y es responsable de eliminar los recursos administrados.
//
//
//Comprueba si el temporizador ya se ha eliminado y si hay algún controlador de eventos presente.
//Si hay controladores de eventos, los cancela del evento Elapsed y elimina el m_Timer.
//El recolector de basura llama al finalizador (~Timer())
//y llama al método Dispose(false) para liberar recursos no administrados.
//En general, este código proporciona
//una implementación de temporizador que puede generar el evento TimerIntervalElapsed
//en un intervalo específico. Admite la adición y eliminación de controladores de eventos,
//el inicio y la detención del temporizador y la eliminación adecuada de los recursos
//cuando ya no se necesita el temporizador.























//Aquí es donde sucede casi toda la magia.

//Lo que podemos notar aquí:

//Internamente estamos usando System.Timers.Timer.
//Aplicamos el patrón de diseño IDisposable .
//Es por eso que puede ver private bool m_IsDisposed, public void Dispose(),

//    protected virtual void Dispose(bool disposing)y ~Timer().
//En el constructor estamos inicializando una nueva instancia de System.Timers.Timer. 
//    Nos referiremos a esto como el temporizador interno en el resto de los pasos.
//Para public bool Enabled, public double Interval, public void Start()y public void Stop(), 
//    solo estamos delegando la implementación al temporizador interno.
//Para public event TimerIntervalElapsedEventHandler TimerIntervalElapsed, esta es la parte más importante así que analicémosla paso a paso.
//Lo que tenemos que hacer con este evento es controlar cuándo alguien se suscribe o cancela su suscripción desde el exterior. 
//    En este caso, queremos reflejar esto en el temporizador interno.
//In other words, if someone from outside is having an instance of our ITimer, 
//he should be able to do something like this t.TimerIntervalElapsed += (sender, dateTime) => { //do something }.
//    At this moment, what we should do is internally do something like m_Timer.Elapsed += (sender, elapsedEventArgs) => { //do something }.
//    However, we need to keep in mind that the two handlers are not the same as they are actually of different types; TimerIntervalElapsedEventHandler and ElapsedEventHandler.
//    Therefore, what we need to do is to wrap the coming in TimerIntervalElapsedEventHandler into a new internal ElapsedEventHandler.This is something we can do.
//However, we also need to keep in mind that at some point 
//someone might need to unsubscribe a handler from the TimerIntervalElapsedEventHandler event.
//This means that at this moment, we need to be able to know which ElapsedEventHandler handler 
//corresponds to that TimerIntervalElapsedEventHandler handler so that we can unsubscribe it from the Internal Timer.

//The only way to achieve this is through keeping track of each TimerIntervalElapsedEventHandler handler and the newly created ElapsedEventHandler handler in a dictionary. This way, by knowing the passed in TimerIntervalElapsedEventHandler handler, we can know the corresponding ElapsedEventHandler handler.
//However, we also need to keep in mind that from outside, someone might subscribe the same TimerIntervalElapsedEventHandler handler more than once.
//Yes, this is not logical, but still it is doable. Therefore, for the sake of completeness, for each TimerIntervalElapsedEventHandler handler we would keep a list of ElapsedEventHandler handlers.
//In most of the cases, this list would have only one entry unless in case of a duplicate subscription.
//And that’s why you can see this private Dictionary<TimerIntervalElapsedEventHandler, List<ElapsedEventHandler>> m_Handlers = new();.
