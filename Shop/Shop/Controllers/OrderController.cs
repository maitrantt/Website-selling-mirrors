using Newtonsoft.Json;
using Shop.Models;
using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        ShopEntities db = new ShopEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListCartTop()
        {
            //Session["UserId"] = 1;
            int idKh = (int)Session["UserId"];
            if (Session["UserId"] == null)
            {
                return PartialView();
            }
            else
            {
                var data = db.SanPham_Cart(idKh);
                float sum = 0;

                foreach (var i in data)
                {
                    sum += (int)i.SoLuong * (float)i.Tien * (100 - (int)i.GiamGia) / 100;
                }
                ViewBag.Sum = String.Format("{0:0,0.00}", sum);
                ViewBag.IdKh = idKh;
                return PartialView();
            }
        }
        public ActionResult ListCart()
        {
            // Session["UserId"] = 1;

            if (Session["UserId"] == null)
            {
                return PartialView();
            }
            else
            {
                int idKh = (int)Session["UserId"];
                var data = db.SanPham_Cart(idKh).ToList();
                float sum = 0;
                foreach (var i in data)
                {
                    sum += (int)i.SoLuong * (float)i.Tien * (100 - (int)i.GiamGia) / 100;
                }
                ViewBag.Sum = String.Format("{0:0,0.00}", sum);
                return PartialView(data);
            }
        }
        public ActionResult MainOrder()
        {
            return PartialView();
        }
        public ActionResult FormOrder()
        {
            Dictionary<string, string> TinhThanh = new Dictionary<string, string>()
            {
                { "Hà Nội","Hà Nội"},
                { "TP Hồ Chí Minh","TP Hồ Chí Minh"},
                { "Đà Nẵng","Đà Nẵng"}


            };
            ViewBag.TinhThanh = TinhThanh;
            return PartialView();
        }
        public JsonResult ValidateOrder(DatHang dh)
        {
            // Session["UserId"] = 1;
            if (Session["UserId"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();

                if (dh.Name == null || dh.Name == "")
                {
                    errors.Add("Name", "Vui long nhap ten");
                }
                if (dh.SDT == null || dh.Name == "")
                {
                    errors.Add("SDT", "Vui long nhap so dien thoai");
                }
                else
                {
                    string regex = "~[0-9]{10}~g";
                    if (Regex.IsMatch(dh.SDT.ToString(), regex)) { errors.Add("SDT", "SDT gom 10 so"); }
                }
                if (dh.DiaChi == null || dh.DiaChi == "")
                {
                    errors.Add("DiaChi", "Vui long nhap dai chi");
                }
                if (dh.TinhThanh == null || dh.TinhThanh == "-" || dh.QuanHuyen == "")
                {
                    errors.Add("TinhThanh", "Vui long chon tinh thanh");
                }
                if (dh.QuanHuyen == null || dh.QuanHuyen == "-" || dh.QuanHuyen == "")
                {
                    errors.Add("QuanHuyen", "Vui long chon quan huyen");
                }
                if (dh.PhuongXa == null || dh.PhuongXa == "-" || dh.PhuongXa == "")
                {
                    errors.Add("PhuongXa", "Vui long chon phuong xa");
                }
                if (errors.Count() == 0)
                {
                    int IdKH = Int32.Parse(Session["UserId"].ToString());
                    db.InsertDatHang(IdKH, dh.Name, dh.SDT, dh.DiaChi, dh.TinhThanh, dh.QuanHuyen, dh.PhuongXa, dh.GhiChu);
                    db.SaveChanges();

                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(errors, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }

}


