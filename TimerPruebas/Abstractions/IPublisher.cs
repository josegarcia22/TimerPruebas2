using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerPruebas
{
    public interface IPublisher
    {

        //A consola de momento, despues..

        //When using System.Timers.Timer in your.NET C# application,
        //you might face problems with abstracting 
        //it and being able to cover your modules ---> with Unit Tests.
        //In this article,
        //we would be discussing the Best Practices
        //on how to conquer these challenges and by the end
        //you would be able to achieve 100% coverage of your modules.


        // date and time every one second.
        // System.Timers.Timer
        //focus on covering the module using System.Timers.
        //  Timer with unit tests.
        //the rest of the solution would not be covered with unit tests.


        //If you would like to know more about this, you can check the article How to Fully Cover. <----
        //NET C# Console Application With Unit Tests.
        //I would rather follow a native simple design than depending on a whole big third party library.


        //2. Bad Solution
        //In this solution, we would directly <--- use System.Timers. ---> (Timer without providing a layer of abstraction)

        //I intentionally invested some time and effort in  --> abstracting System.Console 
        //into --> IConsole to prove that this would not solve our problem with the Timer.

        //------------------------------------

        // public class Timer
        //{

        //a CONSOLA
        //    namespace TimerApp.Abstractions
        //{
   
            void StartPublishing();
            void StopPublishing();
 



    }
}
