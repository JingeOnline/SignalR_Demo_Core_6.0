using Microsoft.AspNetCore.SignalR.Client;
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

namespace Wpf_Core_SignalR_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            //配置connection连接。如果是Web客户端，还需要启动CORS跨域允许。
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5035/ChatHub")
                .Build();

            #region snippet_ClosedRestart
            //当连接断开时会触发该事件
            connection.Closed += async (error) =>
            {
                //等待2秒后重连
                this.Dispatcher.Invoke(() =>
                {
                    messagesList.Items.Add(error?.Message); //通过传入的error可以获得连接断开的原因和信息。
                    messagesList.Items.Add("Connection closed. Re-start...");
                });
                await Task.Delay(2000);
                await connection.StartAsync();
            };
            #endregion
        }
        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            #region snippet_ConnectionOn 
            //配置接收消息的事件处理。
            //接收函数的方法名和参数个数与服务器中Hub类的 Clients.All.SendAsync() 方法要一致。
            connection.On<string, string, string>("ClientReceiveMessage", (user, id, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"[{id}] {user}: {message}";
                    messagesList.Items.Add(newMessage);
                });
            });
            #endregion

            try
            {
                //启动连接。如果连接失败会抛出异常。
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            #region snippet_ErrorHandling
            try
            {
                #region snippet_InvokeAsync
                //发送消息的方法名就是服务器Hub类中的方法名，参数也一致。
                await connection.InvokeAsync("SendMessage", userTextBox.Text, messageTextBox.Text);
                #endregion
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
            #endregion
        }
    }
}
