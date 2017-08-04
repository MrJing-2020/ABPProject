using Abp.Application.Navigation;
using Abp.Localization;
using ABPProject.Authorization;

namespace ABPProject.Web
{
    /// <summary>
    /// 左侧导航栏菜单
    /// 菜单名（MenuItemDefinition第一个参数）需要和视图页ViewBag.ActiveMenu的名字一样
    /// </summary>
    public class ABPProjectNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Index",
                        L("HomePage"),
                        url: "",
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("CapitalPool"),
                        url: "Tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                     ).AddItem(
                            new MenuItemDefinition(
                                "Tenants",
                                L("Project"),
                                url: "Tenants",
                                icon: "fa fa-globe",
                                requiredPermissionName: PermissionNames.Pages_Tenants
                                )
                        ).AddItem(
                            new MenuItemDefinition(
                                "Tenants",
                                L("Capital"),
                                url: "Tenants",
                                icon: "fa fa-globe",
                                requiredPermissionName: PermissionNames.Pages_Tenants
                                )
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Roles",
                        L("Roles"),
                        url: "Roles",
                        icon: "fa fa-users",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        L("Users"),
                        url: "Users",
                        icon: "fa fa-user",
                        requiresAuthentication: true
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ABPProjectConsts.LocalizationSourceName);
        }
    }
}
