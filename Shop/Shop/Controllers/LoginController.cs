using Shop.Models;
using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class LoginController : Controller
    {
        ShopEntities db = new ShopEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult FormLogin()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormLogin(KhachHang khach)
        {


            //if (ModelState.IsValid)
            //{
            khach.Password = Encyptor.MD5Hash(khach.Password);
            var check = db.KhachHangs.Where(s => s.Email == khach.Email && s.Password == khach.Password).FirstOrDefault();
            if (check != null)
            {
                Session["UserId"] = check.IdKh;
                return Redirect("~/Home/Index");
            }
            else
            {
                ViewBag.error = "Email hoac mat khau khong dung";
                return PartialView();
            }
            //}
            //else
            //{
            //    ViewBag.error = "Email hoac mat khau khong dung";
            //    return PartialView();
            //}
            //ViewBag.error = "Email hoac mat khau khong dung";
            //return PartialView();

        }
        //catch (Exception)
        //{
        //    ViewBag.error = "Email hoac mat khau khong dung";
        //    return PartialView();
        //}
        //}
    }
}