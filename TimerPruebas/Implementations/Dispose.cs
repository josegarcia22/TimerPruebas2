using TimerPruebas.Abstractions;

//namespace TimerPruebas.Implementations
//{

//El método Dispose() se utiliza para liberar cualquier recurso utilizado por el temporizador.
//Elimina el m_Timer y cancela la suscripción de los controladores de eventos internos del evento Elapsed.
//También borra el diccionario m_Handlers.

//El método Dispose(bool disposing) es llamado por el método Dispose()
//y es responsable de eliminar los recursos administrados.
//Comprueba si el temporizador ya se ha eliminado
//y si hay algún controlador de eventos presente.
//Si hay controladores de eventos, los cancela del evento Elapsed y elimina el m_Timer.

//protected virtual void Dispose(bool disposing)
//    {
//        if (m_IsDisposed) return;

//        if (disposing && m_Handlers != null)
//        {
//            // Iteramos sobre los valores del diccionario m_Handlers
//            foreach (var internalHandlers in m_Handlers.Values)
//            {
//                // Verificamos si la lista de internalHandlers no es nula y tiene elementos
//                if (internalHandlers != null && internalHandlers.Any())
//                {
//                    // Desvinculamos cada internalHandler del evento Elapsed del m_Timer
//                    foreach (var handler in internalHandlers)
//                    {
//                        m_Timer.Elapsed -= handler;
//                    }
//                }
//            }

//            // Liberamos los recursos del m_Timer
//            m_Timer.Dispose();
//            m_Timer = null;

//            // Limpiamos el diccionario m_Handlers
//            m_Handlers.Clear();
//            m_Handlers = null;
//        }

//        m_IsDisposed = true;
//    }
//}



//protected virtual void Dispose(bool disposing)
//{
//    if (m_IsDisposed) return;

//    if (disposing && m_Handlers.Any())
//    {
//        foreach (var internalHandlers in m_Handlers.Values)
//        {
//            if (internalHandlers?.Any() ?? false)
//            {
//                internalHandlers.ForEach(handler => m_Timer.Elapsed -= handler);
//            }
//        }

//        m_Timer.Dispose();
//        m_Timer = null;
//        m_Handlers.Clear();
//        m_Handlers = null;
//    }

//    m_IsDisposed = true;
//}
//We are unsubscribing all the remaining handlers from the Internal Timer, 
//    disposing the Internal Timer, and clearing the m_Handlers dictionary.

