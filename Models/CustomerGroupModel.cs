using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOPHRMSystem.Models
{
    public class CustomerGroupModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeAndNarration { get; set; }
        public string Narration { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}