using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public SelectList CustomerLists { get; set; }
    }
}