using System;
using DemoLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentProcessor paymentProcessor = new PaymentProcessor();
            for(int i = 0; i <= 10; i++)
            {

                try
                {
                    var result = paymentProcessor.MakePayment($"Demo{ i }", i);

                    Console.WriteLine(result.TransactionAmount);
                }
                catch(NullReferenceException)
                {
                    Console.WriteLine($"Null value for item {i}");
                }
                catch(IndexOutOfRangeException)
                {
                    Console.WriteLine("Skipped invalid record");
                }
                catch(FormatException ex) when(i != 5)
                {
                    if(ex.InnerException != null)
                        Console.WriteLine($"Formatting Issue {ex.InnerException.Message}");
                    else
                        Console.WriteLine("Formatting Issue");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Payment skipped for payment with {i} items");
                    //throw;

                }
            }
            Console.ReadLine();
        }
    }
}
