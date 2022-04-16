using Shop.Models;
using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class RegisterController : Controller
    {

        ShopEntities db = new ShopEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FormRegister()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormRegister(KhachHang khachHang)
        {

            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.Where(s => s.Email == khachHang.Email);
                if (check.Count() == 0)
                {
                    if (khachHang.Password != "")
                    {
                        khachHang.Password = Encyptor.MD5Hash(khachHang.Password);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.KhachHangs.Add(khachHang);
                        db.SaveChanges();
                        Session["UserId"] = db.KhachHangs.Where(s => s.Email == khachHang.Email && s.Password == khachHang.Password).Select(s => s.IdKh);
                        return Redirect("~/Home/Index");
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                else
                {
                    ViewBag.error = "Email da ton tai";
                    return PartialView();
                }

            }
            else
            {
                return PartialView();
            }
        }
    }
}