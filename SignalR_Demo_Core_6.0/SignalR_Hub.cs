using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace SignalR_Demo_Core_6._0
{
    public class SignalR_Hub : Hub
    {
        //该方法的名称对应着客户端发送消息时调用的方法名。
        //里面的参数也与客户端发送消息时，发送的参数个数一致。
        public async Task SendMessage(string user, string message)
        {
            //官方教程中就这一行代码即可让Hub工作。
            //await Clients.All.SendAsync("ReceiveMessage", user, message);

            //获取客户端的ID
            string connectId = this.Context.ConnectionId;
            Console.WriteLine(connectId);

            //第一个参数是客户端接收消息时调用的函数名。
            //该方法后面可以最多跟10个Object类型的自定义参数。
            await Clients.All.SendAsync("ClientReceiveMessage", user, connectId, message);
        }
    }
}
