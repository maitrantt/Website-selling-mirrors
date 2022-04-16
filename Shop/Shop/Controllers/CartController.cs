using Shop.Models;
using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        ShopEntities db = new ShopEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListCart()
        {

            if (Session["UserId"] == null)
            {
                return PartialView();
            }
            else
            {
                int idKh = (int)Session["UserId"];
                var data = db.SanPham_Cart(idKh);
                return PartialView(data);
            }
        }
        public ActionResult CartSum()
        {

            if (Session["UserId"] == null)
            {
                return PartialView();
            }
            else
            {
                int idKh = (int)Session["UserId"];
                var data = db.SanPham_Cart(idKh);
                float sum = 0;
                foreach (var p in data)
                {
                    sum = (float)(sum + (float)p.Tien * (100 - p.GiamGia) / 100 * p.SoLuong);
                }
                ViewBag.sum = sum;
                return PartialView();
            }
        }
        public JsonResult AddProduct(Cart cart)
        {
            if (Session["UserId"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                cart.IdKH = Int32.Parse(Session["UserId"].ToString());
                var giamgia = db.ChiTietSps.Where(s => s.IdSp == cart.IdSp && s.Size == cart.Size).Select(s => s.GiamGia).FirstOrDefault();


                var checkCart = db.Carts.Where(s => s.IdKH == cart.IdKH && s.IdSp == cart.IdSp && s.Size == cart.Size);

                if (checkCart.Count() > 0)
                {
                    var c = db.Carts.Single(p => p.IdKH == cart.IdKH && p.IdSp == cart.IdSp && p.Size == cart.Size);
                    c.SoLuong = c.SoLuong + cart.SoLuong;
                    db.SaveChanges();
                    var check = db.Carts.Where(s => s.IdKH == cart.IdKH && s.IdSp == cart.IdSp && s.Size == cart.Size);
                    return check.Count() > 0 ? Json("Update", JsonRequestBehavior.AllowGet) : Json("Faile", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    var check = db.Carts.Where(s => s.IdKH == cart.IdKH && s.IdSp == cart.IdSp && s.Size == cart.Size);
                    return check.Count() > 0 ? Json("Insert", JsonRequestBehavior.AllowGet) : Json("Faile", JsonRequestBehavior.AllowGet);
                }

            }
        }
        public JsonResult UpdateQuantity(Cart cart)
        {
            if (Session["UserId"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                cart.IdKH = Int32.Parse(Session["UserId"].ToString());
                var check = db.Carts.Single(p => p.IdKH == cart.IdKH && p.IdSp == cart.IdSp && p.Size == cart.Size);
                check.SoLuong = cart.SoLuong;
                db.SaveChanges();
                return Json("Update", JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult RemoveCart(int IdSp, string size)
        {
            if (Session["UserId"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                int idKh = Int32.Parse(Session["UserId"].ToString());
                var product = db.Carts.Where(s => s.IdKH == idKh && s.IdSp == IdSp && s.Size == size).FirstOrDefault();

                db.Carts.Remove(product);
                db.SaveChanges();

                var check = db.Carts.Where(s => s.IdKH == idKh && s.IdSp == IdSp && s.Size == size).FirstOrDefault();
                if (check != null)
                {
                    return Json("Faile", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }

            }


        }
    }
}

