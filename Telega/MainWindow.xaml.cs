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

namespace Telega
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int apiId = 0;
        string apiHash = "";

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
            var client = new TelegramClient(apiId, apiHash);
            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync("<user_number>");
            var code = "<code_from_telegram>"; // you can change code in debugger

            var user = await client.MakeAuthAsync("<user_number>", hash, code);
        }
    }
}
