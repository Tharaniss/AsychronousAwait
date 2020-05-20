using System;
using System.Net;
using System.Threading.Tasks;
namespace AsychronousAwait
{
    class Program
    {
        static void Main(string[] args)
        {

            //Run Asynchronous code in Schedule way
            //Task taskSchedule = Task.Run(() => waitAsync());
            Task taskSchedule = new Task(waitSync);
            Console.WriteLine("Hello World Start");
            taskSchedule.Start();
            taskSchedule.Wait();
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            //------OutPut-----//
            //Hello World Start
            //Hello waitSync!!!
            //Hello World End


            //Run Task Automatically
            Task taskAuto = Task.Run(() => waitSync());
            Console.WriteLine("Hello World Start");
            taskAuto.Wait();
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            //------OutPut-----//
            //Hello World Start
            //Hello waitSync!!!
            //Hello World End



            //Run Code in Asynchronous Way
            Console.WriteLine("Hello World Start");
            waitAsync(); // After Complier watch Await keyword it return the context and Main thread gets executed and Http call with called in background by thread pool.
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            ////------OutPut-----//
            // Hello World Start
            //Hello waitAsync!!!
            //Hello World End



            //Run Code in Asynchronous Way and wait for Http call for Completed
            Console.WriteLine("Hello World Start");
            var waitResult = waitAsync(); // After Complier watch Await keyword it return the context and Main thread gets executed and task wait for http to completed.
            Console.WriteLine("Hello World Method Processing");
            waitResult.Wait();
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            ////------OutPut-----//
            //Hello World Start
            //Hello waitAsync!!!
            //Hello World Method Processing
            //Response
            //Hello World End

            //Run Code in Asynchronous Way and return value
            Console.WriteLine("Hello World Start");
            var waitTaskResult = waitAsyncResult(); // After Complier watch Await keyword it return the context and Main thread gets executed.
            Console.WriteLine("Hello World Method Processing");
            waitTaskResult.Wait();
            //or
            var number = waitTaskResult.Result;
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            ////------OutPut-----//
            //Hello World Start
            //Hello waitAsyncResult!!!
            //Hello World Method Processing
            //Hello World End

            //Run code with multiple Asynchronous meethod
            Console.WriteLine("Hello World Start");
            waitMultiAsync(); // After Complier watch Await keyword it return the context and Main thread gets executed.
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            ////------OutPut-----//
            //Hello World Start
            //Hello waitMultiAsync 1!!!
            //Hello waitMultiAsync 2!!!
            //Hello World End

            //Run Code in Synchronous Way
            Console.WriteLine("Hello World Start");
            waitAsyncWithoutAwait(); // Remove await keyword in wait function.
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            ////------OutPut-----//
            //Hello World Start
            //Hello waitAsync!!!
            //Response
            //Hello World End

            //To run Synchronous method in Asynchronous way
            Console.WriteLine("Hello World Start");
            Task task1 = Task.Run(() =>
            {
                waitSync(); // Return the context and Main thread gets executed.
            });
            Console.WriteLine("Hello World End");
            Console.ReadLine();

            ////------OutPut-----//
            //Hello World Start
            //Hello World End


            //https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
            //https://blog.stephencleary.com/2014/12/a-tour-of-task-part-6-results.html
            //https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html

            //Preventing the Deadlock
            //1. use ConfigureAwait(false) wherever possible
            //2. use async all the way down (i.e., make all method as async)
        }
        static async Task waitAsync()
        {
            Console.WriteLine("Hello waitAsync!!!");
            await Task.Delay(5000);
            var uriStaging = String.Format("https://localhost:44365/api/TradeHistory/openorders?coinPair={0}&buyer={1}&seller={2}", 1, 300, 230);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriStaging);
            using (HttpWebResponse response1 = (HttpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Response");
            }
        }

        static async Task waitAsyncWithoutAwait()
        {
            Console.WriteLine("Hello waitAsync!!!");
            var uriStaging = String.Format("https://localhost:44365/api/TradeHistory/openorders?coinPair={0}&buyer={1}&seller={2}", 1, 300, 230);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriStaging);
            using (HttpWebResponse response1 = (HttpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Response");
            }
        }


        static async Task<int> waitAsyncResult()
        {
            Console.WriteLine("Hello waitAsyncResult!!!");
            await Task.Delay(5000);
            return 1;
        }

        static void waitSync()
        {
            Console.WriteLine("Hello waitSync!!!");
        }

        static async Task waitMultiAsync()
        {
            Console.WriteLine("Hello waitMultiAsync 1!!!");
            await waitMultiAsync2();
        }

        static async Task waitMultiAsync2()
        {
            Console.WriteLine("Hello waitMultiAsync 2!!!");
            await Task.Delay(5000);
        }
    }
}
