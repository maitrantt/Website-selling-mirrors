using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ReviewProduct
    {
        public int idKh { get; set; }
        public int idSp { get; set; }
        public int idbl { get; set; }
        public string TenKh { get; set; }
        public string TitleText { get; set; }
        public string Text { get; set; }
        public Nullable<System.DateTime> ngay { get; set; }
        public int sao { get; set; }


    }
}