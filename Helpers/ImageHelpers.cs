using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcShopping.Helpers
{
    public static class ImageHelpers
    {
        /// <summary>
        /// 返回LinkImg
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionName">方法名称</param>
        /// <param name="imgUrl">图片地址</param>
        /// <param name="altText">alt显示地址</param>
        /// <param name="routeValue">路由值</param>
        /// <param name="linkHtmlAttr"></param>
        /// <param name="imgHtmlAttr"></param>
        /// <returns></returns>
        public static MvcHtmlString Img(this HtmlHelper helper,string actionName,string imgUrl,string altText,object routeValue,object linkHtmlAttr,object imgHtmlAttr)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName,routeValue);
            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href",url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttr));
            var imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src",imgUrl);
            imgTagBuilder.MergeAttribute("alt",altText);
            imgTagBuilder.MergeAttribute("tittle",altText);
            imgTagBuilder.MergeAttributes(new RouteValueDictionary(imgHtmlAttr));
            linkTagBuilder.InnerHtml = imgTagBuilder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(linkTagBuilder.ToString());
        }
    }
}