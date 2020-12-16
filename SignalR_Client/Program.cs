using Microsoft.AspNetCore.SignalR.Client;
using SignalR_Common;
using System;
using System.Threading.Tasks;

namespace SignalR_Client
{
    class Program
    {
        static HubConnection hubConnection;

        static async Task Main(string[] args)
        {
            await InitSignalRConnection();
            bool isExit = false;

            while (!isExit)
            {
                Console.WriteLine("Enter your message or command");
                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput)) 
                    continue;
                if(userInput == "exit")
                {
                    isExit = true;
                }
                else
                {
                    var message = new Message
                    {
                        Title = "Simple message",
                        Body = userInput
                    };

                    await hubConnection.SendAsync("SendMessage", message);
                    Console.WriteLine("Message send");
                }
            }
        }

        private static Task InitSignalRConnection()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:60960/notification")
                .Build();

            hubConnection.On<Message>("Send", message => { 
                Console.WriteLine("New message from server");
                Console.WriteLine($"Title: {message.Title}");
                Console.WriteLine($"Body: {message.Body}");
            });

            return hubConnection.StartAsync();
        }
    }
}
