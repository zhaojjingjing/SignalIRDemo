using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace ChatBox
{
    [ModuleExport(typeof(ChatBoxModule))]
    public class ChatBoxModule :IModule
    {
        private IRegionManager _regionManager;  // 当Prism加载该模块时，它将通过MEF实例化该类，MEF将注入一个Region Manager实例
        [ImportingConstructor]
        public ChatBoxModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }


        /// <summary>
        /// 该方法将为模块启动提供一个代码入口点, 我们将把MEF容器里的ChatBox视图注入到MainWindow界面定义的MainRegion中
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.ChatBox));
        }
    }
}
