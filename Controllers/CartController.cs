using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcShopping.Models;

namespace MvcShopping.Controllers
{
    public class CartController : BaseController
    {

      

        // 添加商品到购物车,没有传值的话,默认为1
        [HttpPost]
        public ActionResult AddToCart(int ProductId,int Amount=1)
        {
            var product = db.Products.Find(ProductId);
            if (product==null)
            {
                return HttpNotFound();
            }
            var existingCarts = cart.FirstOrDefault(p=>p.Product.Id==ProductId);
            if (existingCarts!=null)
            {
                existingCarts.Amount += 1;
            }
            else
            {
                cart.Add(new Cart() { Product=product,Amount=Amount});
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Created);
        }

        //显示当前购物车项目
        public ActionResult Index()
        {
            ViewBag.Title = "购物车";
            return View(cart);
        }

        //移除购物车商品
        [HttpPost]
        public ActionResult Remove(int ProductId)
        {
            ViewBag.Title = "购物车";
            var existerCarts = cart.FirstOrDefault(p=>p.Product.Id==ProductId);
            if (existerCarts!=null)
            {
                cart.Remove(existerCarts);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpPost]
        public ActionResult UpdateAmount(List<Cart> Carts)//选择select的时候,已经把值传给Cart.Amount了,只是页面没刷新过来
        {
            ViewBag.Title = "购物车";
            if (Carts!=null)
            {
                foreach (var item in Carts)
                {
                    var existerCrats = cart.FirstOrDefault(p => p.Product.Id == item.Product.Id);//先查一下商品的ID
                    if (existerCrats != null)
                    {
                        existerCrats.Amount = item.Amount;
                    }
                }
            }
            return RedirectToAction("Index","Cart");
        }
    }
}