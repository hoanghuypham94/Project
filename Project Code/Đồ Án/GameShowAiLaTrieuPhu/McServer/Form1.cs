using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(ConnectServer);
            thread.Start();
        }
        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();

        NetworkStream ns = null;
        void ConnectServer()
        {
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //int port = 5000;
            //TcpClient client = new TcpClient();
            //client.Connect(ip, port);
            //TcpListener ServerListener = new TcpListener(ip, port);
            //TcpClient client = default(TcpClient);
            //Console.WriteLine("client connected!!");
            //ns = client.GetStream();
            //Thread thread = new Thread(o => ReceiveData((TcpClient)o));
            //thread.Start(client);
            int count = 1;

            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
            ServerSocket.Start();

            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(count, client);
                //Console.WriteLine("Someone connected!!");

                Thread t = new Thread(handle_clients);
                t.Start(count);

                count++;
            }
            //string s;
            //while (!string.IsNullOrEmpty((s = Console.ReadLine())))
            {
                //byte[] buffer = Encoding.ASCII.GetBytes(s);
                //ns.Write(buffer, 0, buffer.Length);
            }

            //client.Client.Shutdown(SocketShutdown.Send);
            //thread.Join();
            //ns.Close();
            //client.Close();
        }

        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            while (true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);

                if (byte_count == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                broadcast(data);

                Console.WriteLine(data);
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public static void broadcast(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                foreach (TcpClient c in list_clients.Values)
                {
                    NetworkStream stream = c.GetStream();

                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }


        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string question = rtbQuestion.Text;
            string a = txtA.Text;
            string b = txtB.Text;
            string c = txtC.Text;
            string d = txtD.Text;

            string data = string.Format("{0}@@{1}@@{2}@@{3}@@{4}"
                , question, a, b, c, d);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            ns.Write(buffer, 0, buffer.Length);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
