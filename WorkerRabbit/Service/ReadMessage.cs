using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRabbit.Service
{
    public class ReadMessage : IReadMessage
    {
        public void Read()
        {
            // Definition of Connection 
            // Obviously in a real project we mustn't put here the user and password...   
            var _rabbitMQServer = new ConnectionFactory() { HostName = "localhost", Password = "guest", UserName = "guest" };

            using var connection = _rabbitMQServer.CreateConnection();

            using var channel = connection.CreateModel();

            StartReading(channel, "TestWorkService");
        }

        private void StartReading(IModel channel, string queueName)
        {
            //conectar com a fila
            channel.QueueDeclare(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            //Definição do consumo
            var consumer = new EventingBasicConsumer(channel);

            //Definição do evento quando o Consumidor recebe a mensagem
            consumer.Received += (sender, e) =>
            {
                ManageMessage(e);
            };

            //Inicia a publicação das mensagens para o nosso consumidor
            channel.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Consumidor está em execução!");
            Console.ReadLine();
        }

        private void ManageMessage(BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);

            //Precisamos atualizar a mensagem para um arquivo .txt
            using StreamWriter file = new StreamWriter("MessagesRead.txt", append: true);
            file.Write(message);
        }
    }
}
