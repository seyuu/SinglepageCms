//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SinglePageCMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuItem
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int No { get; set; }
        public string Title { get; set; }
        public bool Blank { get; set; }
        public string Url { get; set; }
    
        public virtual Menu Menu { get; set; }
    }
}