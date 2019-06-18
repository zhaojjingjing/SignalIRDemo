using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Instance
{
    /// <summary>
    /// 消息列表单例类
    /// </summary>
    public class MsgListSingle
    {
        private static MsgListSingle _instance;
        public static MsgListSingle Instance => _instance ?? (_instance = new MsgListSingle());
        public ObservableCollection<ChatInfoDto> chatMsgList;
        private MsgListSingle()
        {
            chatMsgList = new ObservableCollection<ChatInfoDto>();
        }
    }
}
