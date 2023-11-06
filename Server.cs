using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Net_Test
{
    internal class Server
    {
        private static Server server;
        private Socket socket_Lis_Ser;
        private Socket socket_Com_Ser;
        private Thread ReciveThread;
        private Thread ListenThread;
        public static Server GetAServer(string IP_Server,string Port_Server)
        {
            if(server == null)
            {
                server = new Server(IP_Server,Port_Server);
            }// 单例化server
            return server;
        }
        private Server(string Ip_Server,string Port_Server)
        {
            socket_Lis_Ser = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server_Start(Ip_Server,Port_Server);
        }
        private void Server_Start(string IP_Server, string Port_Server)
        {
            try
            {
                socket_Lis_Ser.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP_Server), int.Parse(Port_Server)));
                socket_Lis_Ser.Listen(1);//1-1
                Console.WriteLine("服务器启动成功,等待连接");
                ListenThread = new Thread(Listener_Server)
                {
                    IsBackground = true
                };
                ListenThread.Start();//启动监听线程
                Console.WriteLine("监听线程启动成功，主线程继续运行");
            }
            catch(SocketException e)
            {
                Console.WriteLine("socket异常");
                Console.WriteLine(e.Message);
                ListenThread?.Abort();//关闭监听线程
            }
            catch
            {
                ListenThread?.Abort();//关闭监听线程
                Console.WriteLine("服务器启动失败");
            }

        }
        private void Listener_Server()
        {
            Console.WriteLine("监听线程运行中...");
            try
            {
                while (true)
                {
                    socket_Com_Ser = socket_Lis_Ser.Accept();//声明一个Socket类型的变量用于接收客户端信息
                    Console.WriteLine("客户端连接成功！");
                    Console.WriteLine("客户端信息：" + socket_Com_Ser.RemoteEndPoint.ToString());
                    ReciveThread = new Thread(Recive_Server)
                    {
                        IsBackground = true
                    };
                    ReciveThread.Start(socket_Com_Ser);//启动接收线程
                }
            }
            catch
            {
                Console.WriteLine("监听线程异常");
                ReciveThread?.Abort();//关闭接收线程
            }
        }
        private void Recive_Server(object Com_Soc)
        {
            //Socket clientsocket = Com_Soc as Socket;//声明一个Socket类型的变量用于接收客户端信息
            try
            {
                while (true)
                {
                    byte[] Buffer_Recived_Server = new byte[1024];
                    socket_Com_Ser.Receive(Buffer_Recived_Server);
                    if (Buffer_Recived_Server.Length > 0)
                    {
                        Console.WriteLine("Server收到消息:");
                        Console.WriteLine(Encoding.UTF8.GetString(Buffer_Recived_Server));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("接收消息时发生错误:");
                Console.WriteLine(e.Message);
            }
        }
        public void Send_Server()
        {
            try
            {
                byte[] Buffer_Send_Server = new byte[1024];
                string Message_Send_Server = "This message comes from Server!";

                Buffer_Send_Server = Encoding.UTF8.GetBytes(Message_Send_Server);
                socket_Com_Ser.Send(Buffer_Send_Server);
                Console.WriteLine("发送完成");
            }
            catch
            {
                Console.WriteLine("发送消息时发生错误");
            }
        }
        public void Close_Server()
        {
            socket_Lis_Ser.Close();
            Console.WriteLine("服务器已关闭");
        }
    }
}
