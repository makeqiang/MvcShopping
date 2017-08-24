using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{

    // 订单结账
    [Authorize]
    public class OrderController : BaseController
    {

        //显示完成订单的窗体页面
        public ActionResult Complete()
        {
            ViewBag.Title = "我的订单";
            return View();
        }
        //将订单信息和购物信息写入数据库
        [HttpPost]
        public ActionResult Complete(OrderHeader form)
        {
            //TODO:将订单写入数据库

            //TODO:订单完成后必须清空购物车信息

            //TODO:订单完成后返回首页
            ViewBag.Title = "网站首页";
            var member = db.Members.Where(p => p.Email == User.Identity.Name).FirstOrDefault();
            if (member == null) return RedirectToAction("Index", "Home");
            if (cart == null) return RedirectToAction("Index", "Cart");
            OrderHeader oh = new OrderHeader()
            {
                Member = member,
                ContactName = form.ContactName,
                ContactAddress = form.ContactAddress,
                ContactPhoneNo = form.ContactPhoneNo,
                ByOn = DateTime.Now,
                Memo = form.Memo,
                OrderDatailItems = new List<OrderDetail>()
            };
            int total_price = 0;
            foreach (var item in cart)
            {
                var product = db.Products.Find(item.Product.Id);
                if (product == null) return RedirectToAction("Index", "Cart");
                total_price += item.Product.Price * item.Amount;
                oh.OrderDatailItems.Add(new OrderDetail() { Product = product, Price = product.Price, Amount = item.Amount });
            }
            oh.TotalPrice = total_price;
            db.Orders.Add(oh);
            db.SaveChanges();
            cart.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}