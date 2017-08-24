using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MvcShopping.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //("~/bundles/jquery")这个地方随便写,但是要和View对应
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(

                       "~/Scripts/jquery-{version}.js",

                       "~/Scripts/jquery.unobtrusive-ajax.js",//这地方写需要引入的文件的路径

                       "~/Scripts/jquery.validate.js",

                       "~/Scripts/jquery.validate.unobtrusive.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                        "~/Content/PagedList.css"
                ));
        }
    }
}