using Abp.Web.Mvc.Views;

namespace GmailAPIDemo.Web.Views
{
    public abstract class GmailAPIDemoWebViewPageBase : GmailAPIDemoWebViewPageBase<dynamic>
    {

    }

    public abstract class GmailAPIDemoWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected GmailAPIDemoWebViewPageBase()
        {
            LocalizationSourceName = GmailAPIDemoConsts.LocalizationSourceName;
        }
    }
}