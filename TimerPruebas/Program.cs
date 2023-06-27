
using TimerPruebas.Implementations;

//2
namespace TimerPruebas
{
    
    public class Program 
    {
        //El código proporcionado es una implementación de C# de una clase de temporizador.
        //Repasemos el código para entender su funcionalidad:

        //La clase Timer implementa la interfaz ITimer.
        //Tiene un campo privado m_Handlers,-->DICCIONARIO QUE ASIGNA A TIMERINTERVALELAPSEDEVENTHANDLER DELEGADOS

        //Que es un diccionario que asigna delegados TimerIntervalElapsedEventHandler
        //                                           a listas de delegados ElapsedEventHandler.
        //
        //Este diccionario se utiliza para realizar un seguimiento de...
        //los controladores de eventos asociados con el evento TimerIntervalElapsed.

        //La clase también tiene un campo booleano m_IsDisposed,
        //que se utiliza para indicar si el temporizador se ha eliminado o no.

        //Hay un campo privado m_Timer de tipo System.Timers.Timer,
        //que es el temporizador real que se usa internamente.

        //El constructor inicializa el campo m_Timer creando una nueva instancia de System.Timers.Timer.
        //El evento TimerIntervalElapsed se implementa mediante los accesores de agregar y quitar.
        //Cuando se agrega un controlador de eventos (agregar),
        //se crea un controlador de eventos interno correspondiente

        //y se agrega al diccionario m_Handlers. El controlador de eventos interno simplemente invoca el controlador de eventos original con los argumentos apropiados. El controlador de eventos interno también está suscrito al evento Elapsed de m_Timer. Cuando se elimina (eliminar) un controlador de eventos, se cancela la suscripción del controlador de eventos interno correspondiente del evento Elapsed y se elimina del diccionario m_Handlers.
        //La propiedad Enabled obtiene o establece el valor que indica si el temporizador está habilitado o no.
        //La propiedad Interval obtiene o establece el intervalo en el que el temporizador genera el evento TimerIntervalElapsed.
        
        //El método Start() inicia el temporizador.
        //El método Stop() detiene el temporizador.
        
        //El método Dispose() se utiliza para liberar cualquier recurso utilizado por el temporizador. Elimina el m_Timer y cancela la suscripción de los controladores de eventos internos del evento Elapsed. También borra el diccionario m_Handlers.
        //El método Dispose(bool disposing) es llamado por el método Dispose() y es responsable de eliminar los recursos administrados. Comprueba si el temporizador ya se ha eliminado y si hay algún controlador de eventos presente. Si hay controladores de eventos, los cancela del evento Elapsed y elimina el m_Timer.
        
        //El recolector de basura llama al finalizador (~Timer()) y llama al método Dispose(false) para liberar recursos no administrados.
        //En general, este código proporciona una implementación de temporizador que puede generar el evento TimerIntervalElapsed en un intervalo específico. Admite la adición y eliminación de controladores de eventos, el inicio y la detención del temporizador y la eliminación adecuada de los recursos cuando ya no se necesita el temporizador.


        static void Main(string[] args)
        {
            var timer = new Timer();
            IPublisher publisher = new Publisher(timer, new Implementations.Consola());
            publisher.StartPublishing();
            System.Console.ReadLine();
            publisher.StopPublishing();
            timer.Dispose();
        }
    }
}
//Here we are still not doing much. It is almost the same as the old solution.

//Running this should end up with something like this: