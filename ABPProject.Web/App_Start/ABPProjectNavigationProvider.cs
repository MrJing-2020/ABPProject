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
                        "Project",
                        L("Project"),
                        url: "Admin/Project",
                        icon: "fa fa-tags",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.Project
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Product",
                        L("Product"),
                        url: "Admin/Product",
                        icon: "fa fa-shopping-cart",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.Product
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "SalesOrder",
                        L("SalesOrder"),
                        url: "Admin/SalesOrder",
                        icon: "fa fa-arrow-circle-o-right",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.SalesOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "PurchaseOrder",
                        L("PurchaseOrder"),
                        url: "Admin/PurchaseOrder",
                        icon: "fa fa-arrow-circle-o-left",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.PurchaseOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "ClientPayment",
                        L("ClientPayment"),
                        url: "Admin/ClientPayment",
                        icon: "fa fa-money",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.PurchaseOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Receipt",
                        L("Receipt"),
                        url: "Admin/Receipt",
                        icon: "fa fa-money",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.PurchaseOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Deliver",
                        L("Deliver"),
                        url: "Admin/Deliver",
                        icon: "fa fa-truck",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.PurchaseOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Client",
                        L("Client"),
                        url: "Admin/Client",
                        icon: "fa fa-ticket",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.PurchaseOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Supplier",
                        L("Supplier"),
                        url: "Admin/Supplier",
                        icon: "fa fa-rocket",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.PurchaseOrder
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Settting",
                        L("Settting"),
                        icon: "fa fa-cog",
                        requiredPermissionName: PermissionNames.User
                     ).AddItem(
                            new MenuItemDefinition(
                                "Roles",
                                L("Roles"),
                                url: "Admin/Roles",
                                icon: "fa fa-users",
                                requiredPermissionName: PermissionNames.Role
                                )
                     ).AddItem(
                            new MenuItemDefinition(
                                "Users",
                                L("Users"),
                                url: "Admin/Users",
                                icon: "fa fa-user",
                                requiredPermissionName: PermissionNames.User
                                )
                        )
                )
                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ABPProjectConsts.LocalizationSourceName);
        }
    }
}
