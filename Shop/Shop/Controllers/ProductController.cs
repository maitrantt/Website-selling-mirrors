using Shop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ShopEntities db = new ShopEntities();
        public ActionResult Index(int? id)
        {

            if (id == null)
            {
                ViewBag.tenlsp = "Mirro";
                return View();
            }
            else
            {
                var data = db.LoaiSanPhams.Where(s => s.IdLoaisp == id).FirstOrDefault();
                ViewBag.tenlsp = data.TenLsp;
                ViewBag.idlsp = id;
                return View();
            }
        }
        public ActionResult ListProduct(int? id)
        {

            if (id != null)
            {
                var data = db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.Create_date).Take(5);
                return PartialView(data);
            }
            else
            {

                var data = db.SanPhams.OrderByDescending(s => s.Create_date).AsEnumerable().Take(5);
                return PartialView(data);
            }

        }
        public ActionResult ToolbarFiter()
        {
            var size = db.ChiTietSps.Select(s => s.Size);
            var hd = db.SanPhams.Select(s => s.HinhDang);
            ViewBag.size = size;
            ViewBag.hd = hd;
            return PartialView();
        }
        public ActionResult Pagination(int id)
        {
            int count = db.SanPhams.Where(s => s.IdLoaiSp == id).Count();
            ViewBag.count = count;
            return PartialView();
        }
        // loi phan trang
        public ActionResult Page(int? page, int? id, string name = "")
        {
            int pagesize = 5;

            if (page == null)
            {
                page = 1;
            }

            int start = (int)((page - 1) * pagesize);
            int end = start + pagesize;


            switch (name)
            {
                case "A-Z":
                    var az = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderBy(s => s.TenSanPham).ToList() :
                        db.SanPhams.OrderBy(s => s.TenSanPham).ToList();
                    if (start >= az.Count())
                    {
                        return PartialView();
                    }
                    end = end >= az.Count() ? az.Count() - 1 : end;
                    List<SanPham> a = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a.Add(az[i]);
                    }
                    return PartialView(a);
                case "Z-A":
                    var za = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.TenSanPham).ToList() :
                        db.SanPhams.OrderByDescending(s => s.TenSanPham).ToList();
                    if (start >= za.Count())
                    {
                        return PartialView();
                    }
                    end = end >= za.Count() ? za.Count() - 1 : end;
                    List<SanPham> a1 = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a1.Add(za[i]);
                    }
                    return PartialView(a1);
                case "Price-asc":
                    var pa = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderBy(s => s.Tien).ToList() :
                        db.SanPhams.OrderBy(s => s.Tien).ToList();
                    if (start >= pa.Count())
                    {
                        return PartialView();
                    }
                    end = end >= pa.Count() ? pa.Count() - 1 : end;
                    List<SanPham> a2 = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a2.Add(pa[i]);
                    }
                    return PartialView(a2);
                case "Price-desc":
                    var pd = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.Tien).ToList() :
                        db.SanPhams.OrderByDescending(s => s.Tien).ToList();
                    if (start >= pd.Count())
                    {
                        return PartialView();
                    }
                    end = end >= pd.Count() ? pd.Count() - 1 : end;
                    List<SanPham> a3 = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a3.Add(pd[i]);
                    }
                    return PartialView(a3);
                case "date-asc":
                    var da = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderBy(s => s.Create_date).ToList() :
                        db.SanPhams.OrderBy(s => s.Create_date).ToList();
                    if (start >= da.Count())
                    {
                        return PartialView();
                    }
                    end = end >= da.Count() ? da.Count() - 1 : end;
                    List<SanPham> a4 = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a4.Add(da[i]);
                    }
                    return PartialView(a4);
                case "date-desc":
                    var dd = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.Create_date).ToList() :
                        db.SanPhams.OrderByDescending(s => s.Create_date).ToList();
                    if (start >= dd.Count())
                    {
                        return PartialView();
                    }
                    end = end >= dd.Count() ? dd.Count() - 1 : end;
                    List<SanPham> a5 = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a5.Add(dd[i]);
                    }
                    return PartialView(a5);
                default:
                    var data = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).ToList() :
                db.SanPhams.ToList();
                    if (start >= data.Count())
                    {
                        return PartialView();
                    }
                    end = end >= data.Count() ? data.Count() - 1 : end;

                    List<SanPham> a6 = new List<SanPham>();
                    for (int i = start; i <= end; i++)
                    {
                        a6.Add(data[i]);
                    }
                    return PartialView(a6);
            }
        }
        public ActionResult FeaturedSort(string name, int? id)
        {
            switch (name)
            {
                case "A-Z":
                    var az = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderBy(s => s.TenSanPham).Take(5) : db.SanPhams.OrderBy(s => s.TenSanPham).Take(5);
                    return PartialView(az);
                case "Z-A":
                    var za = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.TenSanPham).Take(5) : db.SanPhams.OrderByDescending(s => s.TenSanPham).Take(5);
                    return PartialView(za);
                case "Price-asc":
                    var pa = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderBy(s => s.Tien).Take(5) : db.SanPhams.OrderBy(s => s.Tien).Take(5);
                    return PartialView(pa);
                case "Price-desc":
                    var pd = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.Tien).Take(5) : db.SanPhams.OrderByDescending(s => s.Tien).Take(5);
                    return PartialView(pd);
                case "date-asc":
                    var da = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderBy(s => s.Create_date).Take(5) : db.SanPhams.OrderBy(s => s.Create_date).Take(5);
                    return PartialView(da);
                case "date-desc":
                    var dd = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).OrderByDescending(s => s.Create_date).Take(5) : db.SanPhams.OrderByDescending(s => s.Create_date).Take(5);
                    return PartialView(dd);
                default:
                    var data = id != -1 ? db.SanPhams.Where(s => s.IdLoaiSp == id).Take(5) : db.SanPhams.Take(5);
                    return PartialView(data);
            }

        }

    }
}