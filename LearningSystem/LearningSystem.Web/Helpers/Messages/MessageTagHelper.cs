namespace LearningSystem.Web.Helpers.Messages
{
    using System.Text;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class MessageTagHelper : TagHelper
    {
        public MessageType Type { get; set; }

        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var result = new StringBuilder();

            result
                .Append($"<div class=\"alert alert-{this.Type.ToString().ToLower()}\">")
                .Append("<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>")
                .Append(this.Message)
                .Append("</div>");

            output.Content.SetHtmlContent(result.ToString());
        }
    }
}