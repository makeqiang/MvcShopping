using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MvcShopping.Controllers
{
    public class HomeController : BaseController
    {
        // 商品首页
        public ActionResult Index()
        {
            ViewBag.Title = "商城首页";
            var data = db.ProductCategorys.ToList();
            return View(data);
        }

        //商品列表页
        public ActionResult ProductList(int id,int p=1)
        {
            ViewBag.Title = "商品列表";
            var productCategory = db.ProductCategorys.Find(id);
            if (productCategory!=null)
            {
                var data = productCategory.Products.ToList();
                var dataPage = data.ToPagedList(pageNumber: p, pageSize: 2);
                return View(dataPage);
            }
            else
            {
                return HttpNotFound();
            }

        }

        //商品信息页
        public ActionResult ProductDetail(int id)
        {
            ViewBag.Title = "商品详细";
            var data = db.Products.Find(id);
            return View(data);
        }
    }
}