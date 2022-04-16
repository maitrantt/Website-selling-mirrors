using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class CartProduct
    {
        public int IdKH { get; set; }
        public int IdSp { get; set; }
        public string Size { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> GiamGia { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<decimal> Tien { get; set; }
        public string Image { get; set; }
        public string HinhDang { get; set; }
    }
}