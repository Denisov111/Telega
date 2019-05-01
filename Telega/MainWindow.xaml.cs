using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TLSharp.Core;
using System.Net.Sockets;
using xNet.Net;
using System.IO;

namespace Telega
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int apiId = 753326;
        string apiHash = "78a12e22bd20d62c3b5759e3e60cb483";
        bool isCodeReady;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Go();
        }

        async private void Go()
        {
            string proxy = "5.101.82.125:8000:Bj9BvB:fGtkgw";

            TcpClient TcpHandler(string address, int port)
            {
                string[] split = proxy.Split(':');
                var socksProxy = new HttpProxyClient(split[0], int.Parse(split[1]), split[2], split[3]);
                var tcpClient = socksProxy.CreateConnection(address, port);
                return tcpClient;
            }

            if (File.Exists("session.dat"))
            {
                var client = new TelegramClient(apiId, apiHash, sessionUserId:"session", handler: TcpHandler);
                await client.ConnectAsync();
                FileSessionStore store = new FileSessionStore();
                TLSharp.Core.Session s = null; s = store.Load("session");
                //var hash = await client.SendCodeRequestAsync("79029828756");
                //string code = await GetCode();

                //var user = await client.MakeAuthAsync("79029828756", hash, code);
                var userByPhoneId = await client.SearchUserAsync("79029828756");
            }
            else
            {
                var client = new TelegramClient(apiId, apiHash, handler: TcpHandler);
                await client.ConnectAsync();

                var hash = await client.SendCodeRequestAsync("79029828756");
                string code = await GetCode();

                var user = await client.MakeAuthAsync("79029828756", hash, code);
            }

            
            
        }

        async private Task<string> GetCode()
        {
            while(!isCodeReady)
            {
                await Task.Delay(50);
            }
            return codeTextBox.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isCodeReady = true;
        }
    }
}
