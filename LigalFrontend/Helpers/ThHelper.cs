using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LigalFrontend.Helpers
{
    public static class ThHelper
    {
        public static MvcHtmlString CustomThFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Boolean etiquetaA = true)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            TagBuilder th = new TagBuilder("th");
            th.Attributes.Add("scope", "col");

            if (etiquetaA)
            {
                TagBuilder a = new TagBuilder("a");
                a.Attributes["id"] = "thFiltro." + name;
                string titulo = (string.IsNullOrEmpty(metadata.DisplayName)) ? metadata.PropertyName : metadata.DisplayName;
                a.InnerHtml = titulo;

                th.InnerHtml = a.ToString();
            }else
            {
                TagBuilder span = new TagBuilder("span");
                string titulo = (string.IsNullOrEmpty(metadata.DisplayName)) ? metadata.PropertyName : metadata.DisplayName;
                span.InnerHtml = titulo;

                th.InnerHtml = span.ToString();
            }


            return new MvcHtmlString(th.ToString());
        }

        public static MvcHtmlString CustomTdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            TagBuilder td = new TagBuilder("td");

            TagBuilder a = new TagBuilder("a");
            //a.AddCssClass("tablasTrabajo");
            //a.Attributes["id"] = name.Replace("item.","");
            var valor = (metadata.Model != null) ? metadata.Model.ToString() : "";
            a.InnerHtml = valor;
            td.InnerHtml = a.ToString();

            return new MvcHtmlString(td.ToString());
        }

        public static MvcHtmlString Custom_Checkbox<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            //Creating input control using TagBuilder class.
            TagBuilder checkbox = new TagBuilder("input");

            //Setting its type attribute to checkbox to render checkbox control.
            checkbox.Attributes.Add("type", "checkbox");

            string name = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            string id = name.Replace(".", "_");
            checkbox.Attributes.Add("name", name);
            checkbox.Attributes.Add("id", id);

            //MemberExpression memberExpression = expression.Body as MemberExpression;
            //string parameterName = memberExpression.Member.Name;

            //var valor = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var propName = metadata.PropertyName;

            MemberExpression memberExpression = (MemberExpression)expression.Body;
            var member = memberExpression.Member as System.Reflection.PropertyInfo;

            string expressionText = ExpressionHelper.GetExpressionText(expression);

            if (member.PropertyType.Equals(typeof(bool)))
            {
                bool model = (bool) metadata.Model;

                if(model)
                    checkbox.Attributes.Add("checked", "checked");

            }
            else if ((member.PropertyType.Equals(typeof(Nullable<Int32>))))
            {
                var model = metadata.Model;
                int val = 0;
                if(model != null)
                {
                    var valor = model.ToString();
                    
                    if (!String.IsNullOrEmpty(valor) && valor != "0")
                    {
                        checkbox.Attributes.Add("checked", "checked");
                        val = 1;
                    }
                }
                checkbox.Attributes.Add("value", val.ToString());
            }          

            return MvcHtmlString.Create(checkbox.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DateHelperFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            //TagBuilder div1 = new TagBuilder("div");
            //div1.AddCssClass("form-group");
            

            TagBuilder div2 = new TagBuilder("div");
            div2.AddCssClass("input-group clasePrueba");

            string name = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            string id = name.Replace(".", "_");
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var valor = (metadata.Model != null) ? metadata.Model.ToString() : "";

            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("id", id);
            input.Attributes.Add("name", name);
            input.Attributes.Add("value", valor);
            input.AddCssClass("form-control");
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes);
            }

            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("input-group-addon");

            TagBuilder span2 = new TagBuilder("span");
            span2.AddCssClass("glyphicon glyphicon-calendar");

            span.InnerHtml = span2.ToString();
            input.InnerHtml = span.ToString();
            div2.InnerHtml = input.ToString();
            //div1.InnerHtml = div2.ToString();

            return new MvcHtmlString(div2.ToString());


            //    <div class="input-group clasePrueba">
            //        @Html.EditorFor(vista => vista.inspeccion.FechaSiguienteVisita, new { htmlAttributes = new { @class = "form-control" }})
            //        <span class="input-group-addon">
            //            <span class="glyphicon glyphicon-calendar"></span>
            //        </span>
            //    </div>
            
        }
    }
}
