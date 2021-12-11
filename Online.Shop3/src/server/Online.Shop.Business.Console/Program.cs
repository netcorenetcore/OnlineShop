using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Online.Shop.Business.Console
{

    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                AutomaticRecoveryEnabled = true,
                HostName = "localhost",
                Port = 5672,
                DispatchConsumersAsync = true
            };

            var connection = connectionFactory.CreateConnection();
            

            var channel = connection.CreateModel();

            var basicConsumer = new AsyncEventingBasicConsumer(channel);
            basicConsumer.Received += BasicConsumerOnReceived;
            channel.QueueDeclare("shopprice", true, false, false, null);
            
            channel.BasicConsume("queename", true, basicConsumer);
            System.Console.ReadLine();
        }


        private static Task BasicConsumerOnReceived(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body;
            var str = Encoding.UTF8.GetString(body.ToArray());
            System.Console.WriteLine(str);
            File.AppendAllLines("log.txt",new [] { str});
            return Task.CompletedTask;
        }
    }
}
