
using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        ShopEntities db = new ShopEntities();
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult HeaderNav()
        {
            if (Session["UserId"] != null)
            {
                int idKh = Int32.Parse(Session["UserId"].ToString());
                int cartCount = db.Carts.Where(s => s.IdKH == idKh).ToList().Count();
                ViewBag.count1 = cartCount;

                var data = db.LoaiSanPhams.AsEnumerable();
                ViewBag.count = data.Count();
                return PartialView(data);

            }
            else
            {
                ViewBag.count1 = 0;
                var data = db.LoaiSanPhams.AsEnumerable();
                ViewBag.count = data.Count();
                return PartialView(data);
            }

        }
        [ChildActionOnly]
        public ActionResult HeaderToggle()
        {
            var data = db.LoaiSanPhams.AsEnumerable();
            ViewBag.count = data.Count();
            return PartialView(data);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {

            return PartialView();
        }


        public ActionResult Slider()
        {
            return PartialView();
        }

        public ActionResult Banner()
        {
            return PartialView();
        }
        public ActionResult StandingMirror()
        {
            var data = db.SanPhams.OrderByDescending(s => s.Create_date).Take(10).Where(s => s.IdLoaiSp == 1 && s.SoLuong > 0);
            ViewBag.idlsp = 1;


            return PartialView(data);
        }
        public ActionResult HangingMirror()
        {
            var data = db.SanPhams.OrderByDescending(s => s.Create_date).Take(10).Where(s => s.IdLoaiSp == 2 && s.SoLuong > 0);
            ViewBag.idlsp = 2;


            return PartialView(data);
        }

        public ActionResult QuickViewProduct(int idSp)
        {
            var data = db.SanPhams.Where(s => s.IdSp == idSp).FirstOrDefault();
            var arrImage = db.SanPham_Anh.Where(s => s.IdSp == idSp).Select(s => s.Image);
            var arrSize = db.ChiTietSps.Where(s => s.IdSp == idSp && s.SoLuong > 0);


            ViewBag.data = data;
            ViewBag.arrImage = arrImage;
            ViewBag.arrSize = arrSize;
            return PartialView();
        }
        public ActionResult ProductHot()
        {
            var data = db.Product_Hot().Take(8);
            return PartialView(data);
        }

        public ActionResult Search()
        {
            return PartialView();
        }

        public ActionResult SearchProduct(string a = "")
        {
            var data = db.SanPhams.Where(s => s.TenSanPham.Contains(a) == true).AsEnumerable();
            return PartialView(data);
        }
    }
}

