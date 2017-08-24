using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.UserCenter
{
    [AbpAuthorize]
    public class UserCenterAppService: ABPProjectAppServiceBase, IUserCenterAppService
    {
    }
}
