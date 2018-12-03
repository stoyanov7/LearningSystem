namespace LearningSystem.Utilities.Helpers
{
    using System;
    using System.IO;
    using System.Linq.Expressions;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class FormGroupHelper
    {
        public static IHtmlContent FormGroupFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper
                    .LabelFor(expression, new { @class = "control-label col-sm-2" });

                var editor = htmlHelper
                    .EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });

                var validationMessage = htmlHelper
                    .ValidationMessageFor(expression, null, new { @class = "text-danger" });

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);

                writer.Write("<div class=\"col-sm-10\">");
                editor.WriteTo(writer, HtmlEncoder.Default);

                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }
    }
}