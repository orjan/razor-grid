using System.Web.Mvc;
using System.Web.WebPages;

namespace RazorGrid.Example.App_Code
{
    /// <summary>
    /// This class is need to be able to use @Html in helpers
    /// http://stackoverflow.com/questions/4710853/using-mvc-htmlhelper-extensions-from-razor-declarative-views
    /// </summary>
    public class HelperPage : System.Web.WebPages.HelperPage
    {
        public new static HtmlHelper Html
        {
            get { return ((WebViewPage) WebPageContext.Current.Page).Html; }
        }
    }
}