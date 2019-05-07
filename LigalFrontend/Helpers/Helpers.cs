using System.Web.Mvc;

namespace LigalFrontend.Helpers
{
    public static class MailToHelper
    {
        public static object Mailto(this HtmlHelper helper, string email, string name)
        {
            TagBuilder etiqueta = new TagBuilder("a");
            string ruta = "mailto:" + email;
            etiqueta.MergeAttribute("href", ruta);
            etiqueta.SetInnerText(name);

            return MvcHtmlString.Create(etiqueta.ToString(TagRenderMode.Normal));
        }
    }
}