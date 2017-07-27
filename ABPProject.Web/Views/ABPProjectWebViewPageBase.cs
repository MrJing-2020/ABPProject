using Abp.Web.Mvc.Views;

namespace ABPProject.Web.Views
{
    public abstract class ABPProjectWebViewPageBase : ABPProjectWebViewPageBase<dynamic>
    {

    }

    public abstract class ABPProjectWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ABPProjectWebViewPageBase()
        {
            LocalizationSourceName = ABPProjectConsts.LocalizationSourceName;
        }
    }
}