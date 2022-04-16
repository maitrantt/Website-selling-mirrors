using Shop.Models;
using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductItemController : Controller
    {
        // GET: ProductItem
        ShopEntities db = new ShopEntities();
        public ActionResult Index(int id)
        {
            //idSp = 11;

            var sp = db.SanPhams.Where(s => s.IdSp == id).Select(s => new
            {
                idlsp = s.IdLoaiSp,
                name = s.TenSanPham,
                hinhdang = s.HinhDang
            }).FirstOrDefault();
            string TenLsp = db.LoaiSanPhams.Where(s => s.IdLoaisp == sp.idlsp).Select(s => s.TenLsp).FirstOrDefault();
            ViewBag.idSp = id;
            ViewBag.tenlsp = TenLsp;
            ViewBag.tenSp = sp.name;
            ViewBag.idlsp = sp.idlsp;
            ViewBag.hinhdang = sp.hinhdang;
            return View();
        }
        public ActionResult productItem(int id)
        {
            var data = db.SanPhams.Where(s => s.IdSp == id).FirstOrDefault();
            var arrImage = db.SanPham_Anh.Where(s => s.IdSp == id).Select(s => s.Image);
            var arrSize = db.ChiTietSps.Where(s => s.IdSp == id && s.SoLuong > 0);


            ViewBag.data = data;
            ViewBag.arrImage = arrImage;
            ViewBag.arrSize = arrSize;

            return PartialView();
        }
        public ActionResult Description(int id)
        {
            var data = db.SanPhams.Where(s => s.IdSp == id).FirstOrDefault();
            return PartialView(data);
        }
        public ActionResult ProductReviews(int id)
        {

            var a = db.BinhLuans.Join
               (db.KhachHangs, bl => bl.IdKh,
               kh => kh.IdKh, (bl, kh) => new
               {
                   TenKh = kh.FirstName + " " + kh.LastName,
                   text = bl.Text,
                   ngay = bl.Create_date,
                   idbl = bl.IdBinhLuan,
                   idkh = bl.IdKh,
                   idSp = bl.IdSp,
                   sao = bl.sao,
                   titleText = bl.TitleText
               }).Where(bl => bl.idSp == id).OrderByDescending(bl => bl.ngay).ToList();

            List<ReviewProduct> data = new List<ReviewProduct>();
            float sumSao = 0;
            foreach (var i in a)
            {
                ReviewProduct z = new ReviewProduct();

                z.idKh = (int)i.idkh;
                z.idbl = (int)i.idbl;
                z.idSp = (int)i.idSp;
                z.TenKh = (string)i.TenKh;
                z.Text = (string)i.text;
                z.ngay = (DateTime)i.ngay;
                int sao = (int)i.sao;
                z.sao = sao;
                z.TitleText = (string)i.titleText;
                sumSao += (int)i.sao;

                data.Add(z);

            }
            ViewBag.sumSao = sumSao / data.Count();
            ViewBag.idSp = id;
            return PartialView(data);
        }
        public ActionResult WriteReview(int? idSp)
        {
            Session["UserId"] = 1;
            ViewBag.idSp = idSp;
            return PartialView();
        }
        //[HttpPost]

        public JsonResult Write(BinhLuan data)
        {

            int idKh = Int32.Parse(Session["UserId"].ToString());
            var kh = db.KhachHangs.Where(s => s.IdKh == idKh).Select(s => new
            {
                FirstName = s.FirstName,
                LastName = s.LastName
            }).FirstOrDefault();
            data.IdKh = idKh;
            data.Create_date = DateTime.Now;
            var c = db.BinhLuans.Add(data);
            db.SaveChanges();

            try
            {
                string TenKh = kh.FirstName + " " + kh.LastName;
                ReviewProduct write = new ReviewProduct();
                write.ngay = data.Create_date;
                write.TenKh = TenKh;
                write.idbl = c.IdBinhLuan;
                write.idSp = (int)data.IdSp;

                return Json(write, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ProductSimilar(int idlsp, string hinhdang)
        {
            List<SanPham> data = new List<SanPham>();

            if (hinhdang == null || hinhdang == "")
            {
                data = db.SanPhams.Where(s => s.IdLoaiSp == idlsp).OrderByDescending(s => s.Tien).ToList();
            }
            else
            {
                data = db.SanPhams.Where(s => s.IdLoaiSp == idlsp).OrderByDescending(s => s.Tien).ToList();
            }
            if (data.Count() < 8 && (hinhdang == null || hinhdang == ""))
            {
                var da = db.SanPhams.Where(s => s.HinhDang != hinhdang && s.IdLoaiSp == idlsp).Take(8 - data.Count());
                foreach (var sp in da)
                {
                    data.Add(sp);
                }
            }
            if (data.Count() < 8 && (hinhdang == null || hinhdang == ""))
            {
                var da = db.SanPhams.Where(s => s.HinhDang != hinhdang && s.IdLoaiSp != idlsp).Take(8 - data.Count());
                foreach (var sp in da)
                {
                    data.Add(sp);
                }
            }
            return PartialView(data);


        }

    }
}