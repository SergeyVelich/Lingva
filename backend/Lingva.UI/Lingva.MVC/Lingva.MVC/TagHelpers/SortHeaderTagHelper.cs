using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lingva.MVC.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        public string Property { get; set; }
        public string Current { get; set; }
        public string Action { get; set; }
        public bool Up { get; set; }

        private readonly IUrlHelperFactory urlHelperFactory;
        public SortHeaderTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string Order = "Desc";
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";

            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (Up == true)
                {
                    tag.AddCssClass("glyphicon-chevron-up");
                }
                else
                {
                    tag.AddCssClass("glyphicon-chevron-down");
                    Order = "Asc";
                }

                output.PreContent.AppendHtml(tag);
            }
            string url = urlHelper.Action(Action, new { sortProperty = Property, sortOrder = Order });
            output.Attributes.SetAttribute("href", url);
        }
    }
}
