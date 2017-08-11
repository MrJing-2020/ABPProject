using ABPProject.Sessions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABPProject.Web.SignalHub
{
    public class ConnUserViewModel
    {
        public UserLoginInfoDto UserInfo { get; set; }
        public string ConnectionId { get; set; }
    }
}