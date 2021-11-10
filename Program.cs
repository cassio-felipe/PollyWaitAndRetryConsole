using System;
using Polly;

namespace PollyWaitAndRetryConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var retryPolicy = Polly.Policy.Handle<DivideByZeroException>(e =>
                {
                    
                    Console.WriteLine("Handling Devide by zero exception");
                    return true;
                }).WaitAndRetry(4, retryAttempt =>
                    {
                        Console.WriteLine($"Retrying attempt {retryAttempt}");
                        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));//tempo exponencial entre execuções para esperar.
                    }
                );

                retryPolicy.Execute(Divide);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handling the exception {ex.ToString()}");
            }
            Console.ReadKey();
        }
        
        private static void Divide()
        {
            int a = 10;
            int b = 0;
            var c = a/b; 
        }
    }
}