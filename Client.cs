using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Net_Test
{
    internal class Client
    {
        private static Client client;
        private Socket clientsocket;
        public static Client GetAClient(string IP_Client, string Port_Client)
        {
            if (client == null)
            {
                client = new Client(IP_Client, Port_Client);
            }// 单例化Client
            return client;
        }
        private Client(string Ip_Client, string Port_Client)
        {
            clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Client_Start(Ip_Client, Port_Client);
        }
        private void Client_Start(string IP_Client, string Port_Client)
        {
            try
            {
                clientsocket.Connect(new IPEndPoint(IPAddress.Parse(IP_Client), int.Parse(Port_Client)));
                Console.WriteLine("客户端启动成功");
                Thread ReciveThread = new Thread(Recive_Client)
                {
                    IsBackground = true
                };
                ReciveThread.Start();//启动接收线程
                Console.WriteLine("接收线程启动成功，客户端主线程继续运行");
            }
            catch (SocketException e)
            {
                Console.WriteLine("socket异常");
                Console.WriteLine(e.Message);
            }
            catch
            {
                Console.WriteLine("连接失败");
            }

        }
        private void Recive_Client()
        {
            try
            {
                while (true)
                {
                    byte[] Buffer_Recived_Client = new byte[1024];
                    clientsocket.Receive(Buffer_Recived_Client);
                    Console.WriteLine("收到消息");
                    if (Buffer_Recived_Client.Length > 0)
                    {
                        Console.WriteLine(Encoding.UTF8.GetString(Buffer_Recived_Client));
                    }
                }
            }
            catch
            {
                Console.WriteLine("接收消息时发生错误");
            }
        }
        public void Send_Client()
        {
            try
            {
                byte[] Buffer_Send_Client = new byte[1024];
                string Message_Send_Client = "This message comes from client!";
                Buffer_Send_Client = Encoding.UTF8.GetBytes(Message_Send_Client);
                clientsocket.Send(Buffer_Send_Client);
                Console.WriteLine("发送完成");
            }
            catch
            {
                Console.WriteLine("发送消息时发生错误");
            }
        }
    }
}
