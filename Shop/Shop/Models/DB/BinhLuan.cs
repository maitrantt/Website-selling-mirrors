//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class BinhLuan
    {
        public int IdBinhLuan { get; set; }
        public string TitleText { get; set; }
        public string Text { get; set; }
        public Nullable<int> sao { get; set; }
        public Nullable<System.DateTime> Create_date { get; set; }
        public Nullable<int> IdKh { get; set; }
        public Nullable<int> IdSp { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
