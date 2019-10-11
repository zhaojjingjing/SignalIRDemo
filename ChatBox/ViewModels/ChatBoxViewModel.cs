using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Configs;
using Common.DTO;
using Common.Instance;
using Common.SignalIRHubService;
using Prism.Commands;
using Prism.Mvvm;

namespace ChatBox.ViewModels
{
    [Export(typeof(ChatBoxViewModel))]
    public class ChatBoxViewModel : BindableBase
    {
        /// <summary>
        /// 消息实体
        /// </summary>
        private ChatInfoDto chatInfoDto;

        private string _title = "清空消息";
        public string Title
        {
            get { return _title; }
            // set { SetProperty(ref _title, value); }
            set { _title = value; RaisePropertyChanged("Title"); }
        }

        private string _sendMsgContentText;
        /// <summary>
        /// 发送消息文本框
        /// </summary>
        public string SendMsgContentText
        {
            get { return _sendMsgContentText; }
            set
            {
                if (_sendMsgContentText != value)
                {
                    _sendMsgContentText = value; RaisePropertyChanged("SendMsgContentText");
                }
            }
        }

        private ObservableCollection<ChatInfoDto> _chatMsgList;
        /// <summary>
        /// 显示的消息列表
        /// </summary>
        public ObservableCollection<ChatInfoDto> ChatMsgList
        {
            get { return _chatMsgList; }
            set
            {
                if (_chatMsgList != value)
                {
                    _chatMsgList = value; RaisePropertyChanged("ChatMsgList");
                }
            }
        }

        public DelegateCommand<string> SendMsgCommand { get; set; }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public async void SendMsgExcute(string msg)
        {
            chatInfoDto = new ChatInfoDto
            {
                UserName = SysConfig.UserName,
                Message = msg
            };
            try
            {
                SendMsgContentText = null;
                await HubService.SendMsg(chatInfoDto);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DelegateCommand CleanCommand { get; set; }
        /// <summary>
        /// 清理消息
        /// </summary>
        /// <param name="msg"></param>
        public void CleanExcute()
        {
            ChatMsgList.Clear();
        }

        public ChatBoxViewModel()
        {
            HubService.Connect();  //连接HubService
            ChatMsgList = MsgListSingle.Instance.chatMsgList;
            this.SendMsgCommand = new DelegateCommand<string>(SendMsgExcute);
            this.CleanCommand = new DelegateCommand(CleanExcute);
        }
    }
}
