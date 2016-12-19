using Abp.Application.Navigation;
using Abp.Localization;
using GmailAPIDemo.Authorization;

namespace GmailAPIDemo.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class GmailAPIDemoNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", GmailAPIDemoConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "#tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        L("Users"),
                        url: "#users",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "About",
                        new LocalizableString("About", GmailAPIDemoConsts.LocalizationSourceName),
                        url: "#/about",
                        icon: "fa fa-info"
                        )
                ).AddItem(
                        new MenuItemDefinition(
                        "Mails",
                        new LocalizableString("Mails", GmailAPIDemoConsts.LocalizationSourceName),
                        url: "#/mails",
                        icon: "fa fa-envelope"
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GmailAPIDemoConsts.LocalizationSourceName);
        }
    }
}
