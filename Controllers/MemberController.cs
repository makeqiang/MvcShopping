using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcShopping.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Text;

namespace MvcShopping.Controllers
{
    public class MemberController : BaseController
    {
        // 会员注册页
        public ActionResult Register()
        {
           
            ViewBag.Title = "新用户注册";
            return View();
        }

        //注册页
        [HttpPost]
        public ActionResult Register([Bind(Exclude = "RegisterOn,AuthCode")]Member member)//bind是不允许绑定的东西
        {
            ViewBag.Title = "新用户注册";
            if(ModelState.IsValid)
            {
                member.RegisterOn = DateTime.Now;
                member.AuthCode = Guid.NewGuid().ToString();
                db.Members.Add(member);
                db.SaveChanges();
                SendAuthCodeToMember(member);
                TempData["EmailUrl"] = GetEmailUrl(member);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }

        //发送邮件验证用户有效性
        private void SendAuthCodeToMember(Member member)
        {
            string mailBody = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MemberRegisterEmailTemplate.html"));
            mailBody = mailBody.Replace("{{Name}}",member.Name);
            mailBody = mailBody.Replace("{{RegisterOn}}",member.RegisterOn.ToString("F"));
            var auth_url = new UriBuilder(Request.Url)
            {
                Path=Url.Action("ValidateRegister",new { id=member.AuthCode}),
                Query=""
            };
            mailBody = mailBody.Replace("{{AUTH_URL}}", auth_url.ToString());
            try
            {
                SmtpClient stmpServer = new SmtpClient("smtp.qq.com");
                stmpServer.EnableSsl = true;
                stmpServer.Credentials = new System.Net.NetworkCredential("1490529827", "qumwqwectiaigddc");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("1490529827@qq.com");
                mail.To.Add(member.Email);
                mail.Subject = "电商网站注册验证";
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                stmpServer.Send(mail);

            }
            catch (Exception)
            {
                throw;
            }
        }

        //显示会员登录页
        public ActionResult Login (string returnUrl)
        {
            ViewBag.Title = "用户登录";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //会员登录
        [HttpPost]
        public ActionResult Login(string email,string password,string returnUrl)
        {
            ViewBag.Title = "用户登录";
            if (ValidateUser(email,password))
            {
                FormsAuthentication.SetAuthCookie(email, false);//添加到cookie
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            return View();
        }

        //验证账号密码
        private bool ValidateUser(string email,string password)
        {
            ViewBag.Title = "用户登录";
            var member = (from p in db.Members where p.Email==email && p.Password==password select p).FirstOrDefault();
            if (member != null)
            {
                if (member.AuthCode==null)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("","你还没通过邮箱验证,请进邮箱验证");
                    return false;
                }
            }
            else
            {
                ModelState.AddModelError("", "您输入的账号或者密码错误");
                return false;
            }
        }
        
        //判断用户是否通过验证
        public ActionResult ValidateRegister(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();
            var member = db.Members.Where(p=>p.AuthCode==id).FirstOrDefault();
            if (member!=null)
            {
                TempData["LastTempMessage"] = "会员验证成功";
                member.AuthCode = null;
                db.SaveChanges();
            }
            else
            {
                TempData["LastTempMessage"] = "没有查到此验证码,您可能已经通过验证了";
            }
            return RedirectToAction("Login","Member");
        }

        //退出登录,并且清除所有Cookie
        public ActionResult Logout()
        {
            ViewBag.Title = "网站首页";
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        //判断用户使用的是哪一个邮箱
        public string GetEmailUrl(Member member)
        {
            string emailUrl="";
            string email = member.Email;
            int i = email.IndexOf("@");
            int j = email.IndexOf(".");
            string res = email.Substring(i,j-i);
            switch (res)
            {
                case "@qq":
                    emailUrl = "https://mail.qq.com/";
                    break;
                case "@163":
                    emailUrl = "http://mail.163.com/";
                    break;
            }
            return emailUrl;
        }

        //远程验证邮箱是否注册
        [OutputCache]
        public JsonResult CheckDup(string Email)
        {
            var member = db.Members.Where(p=>p.Email == Email).FirstOrDefault();
            if (member!= null)
            {
                return Json(false,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true,JsonRequestBehavior.AllowGet);
            }
        }
    }
}