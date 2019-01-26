using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Web
{
    public static class RadioBoxListHelper
    {
        public static MvcHtmlString RadioBoxList(this HtmlHelper helper, string name)
        {
            return RadioBoxList(helper, name, helper.ViewData[name] as SelectList, new { });
        }

        public static MvcHtmlString RadioBoxList(this HtmlHelper helper, string name, SelectList selectList)
        {
            return RadioBoxList(helper, name, selectList, new { });
        }

        public static MvcHtmlString RadioBoxList(this HtmlHelper helper, string name, SelectList selectList, object htmlAttributes)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            HtmlAttributes.Add("type", "radio");
            HtmlAttributes.Add("name", name);

            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            int j = 0;
            foreach (SelectListItem selectItem in selectList.Items)
            {
                string id = string.Format("{0}{1}", name, j++);
                
                IDictionary<string, object> newHtmlAttributes = HtmlAttributes.DeepCopy();
                newHtmlAttributes.Add("value", selectItem.Value);
                newHtmlAttributes.Add("id", id);
                //var selectedValue = (selectList as SelectList).SelectedValue;
                //if (selectedValue == null)
                //{
                //    if (i++ == 0)
                //        newHtmlAttributes.Add("checked", null);
                //}
                //else if (selectItem.Value == selectedValue.ToString())
                //{
                //    newHtmlAttributes.Add("checked", null);
                //}
                if(selectItem.Selected)
                    newHtmlAttributes.Add("checked", null);

                TagBuilder tagBuilder = new TagBuilder("input");
                tagBuilder.MergeAttributes<string, object>(newHtmlAttributes);
                string inputAllHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
                stringBuilder.AppendFormat(@"<label for='{2}'>{0}{1}</label>",
                   inputAllHtml, selectItem.Text, id);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());

        }
        private static IDictionary<string, object> DeepCopy(this IDictionary<string, object> ht)
        {
            Dictionary<string, object> _ht = new Dictionary<string, object>();

            foreach (var p in ht)
            {
                _ht.Add(p.Key, p.Value);
            }
            return _ht;
        } 
    }

}

