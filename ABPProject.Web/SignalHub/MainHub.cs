using Abp.Dependency;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Threading;
using ABPProject.Sessions;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ABPProject.Web.SignalHub
{
    public class MainHub: Hub, ITransientDependency
    {
        public IAbpSession AbpSession { get; set; }
        private readonly ICacheManager _cacheManager;
        private readonly ISessionAppService _sessionAppService;

        public ILogger Logger { get; set; }

        public MainHub(ISessionAppService sessionAppService, ICacheManager cacheManager)
        {
            _sessionAppService = sessionAppService;
            _cacheManager = cacheManager;
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
        }

        public void SendMessage(long id, string message)
        {
            var fromUser = _cacheManager.GetCache("connectionUser").GetOrDefault(AbpSession.UserId.ToString()) as ConnUserViewModel;
            var toUser = _cacheManager.GetCache("connectionUser").GetOrDefault(id.ToString()) as ConnUserViewModel;
            Clients.Client(toUser.ConnectionId).sendMessage(fromUser.UserInfo.UserName, message);
        }

        public async override Task OnConnected()
        {
            var loginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());
            _cacheManager.GetCache("connectionUser").Remove(loginInformations.User.Id.ToString());
            _cacheManager.GetCache("connectionUser").Set(loginInformations.User.Id.ToString(), new ConnUserViewModel { ConnectionId = Context.ConnectionId, UserInfo = loginInformations.User });
            await base.OnConnected();
            //Logger.Debug("A client connected to MyChatHub: " + Context.ConnectionId);
        }

        public async override Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
            //Logger.Debug("A client disconnected from MainHub: " + Context.ConnectionId);
        }
    }
}