using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace meetupSignalRCore2.NETClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*************** Implementing a .NET client ***************");
            Console.Write("Press a key to start listening: ");
            Console.ReadKey();

            var connection = new HubConnectionBuilder()
                 .WithUrl("http://localhost:51668/Hello")
                 .Build();

            connection.On<string>("HelloFrom", (result) => {
                Console.Write("Text received: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(result);
                Console.ResetColor();
            });

            connection.StartAsync().GetAwaiter().GetResult();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Listening. Press a key to quit");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("*************** Process finished ***************");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}