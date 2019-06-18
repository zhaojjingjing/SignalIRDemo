using System;
using System.Threading.Tasks;
using System.Windows;
using Common.Configs;
using Common.DTO;
using Common.Instance;
using Microsoft.AspNetCore.SignalR.Client;

namespace Common.SignalIRHubService
{
    public class HubService
    {
        private static readonly HubConnection _connection;
        static HubService()
        {
            _connection = new HubConnectionBuilder().WithUrl(SysConfig.SignalIRChatService).Build();  //创建HubConnectionBuilder，并调用Build
            _connection.Closed += (error) => {
                // Do your close logic.
                return Task.CompletedTask;
            };
            _connection.Closed += async (error) =>
            {  //连接关闭
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();  //开始连接服务
            };
        }

        /// <summary>
        /// 连接SignalIR服务器
        /// </summary>
        public static async void Connect()
        {
            _connection.On<ChatInfoDto>("ReceiveMsg", msg =>
            {  //接受服务器发送的消息 ReceiveMsg与服务器中对应
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MsgListSingle.Instance.chatMsgList.Add(msg);  //添加消息单例类
                });
            });

            try
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 向服务器发送消息
        /// </summary>
        /// <param name="chatInfoDto"></param>
        /// <returns></returns>
        public static async Task SendMsg(ChatInfoDto chatInfoDto)
        {
            try
            {
                await _connection.InvokeAsync("SendMsg",
                    chatInfoDto);  //向服务器发送的消息 SendMsg与服务器中对应
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
