using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class DepartmentThirdModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public string NarrationFirst { get; set; }
        public string CodeFirst { get; set; }
        public string CodeSecond { get; set; }
        public string NarrationSecond { get; set; }
        public string CodeAndNarration { get; set; }
        public int Fk_DepartmentIdFirst { get; set; }
        public int Fk_DepartmentIdSecond { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


        public SelectList SelectListItems { get; set; }
        public SelectList SelectSecondListItems { get; set; }
    }
}