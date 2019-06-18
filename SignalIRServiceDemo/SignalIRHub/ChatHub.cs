using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalIRServiceDemo.DTO;

namespace SignalIRServiceDemo.SignalIRHub
{
    public class ChatHub :Hub
    {
        public async Task SendMsg(ChatMsgInfoDto chatMsgInfo)
        {
            await Clients.All.SendAsync("ReceiveMsg", chatMsgInfo);  //在客户端匹配ReceiverMsg
        }
    }
}