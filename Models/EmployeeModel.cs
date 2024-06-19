using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; } 
        public Nullable<System.DateTime> DateOfJoin { get; set; }
        public string Nic { get; set; }
        public string Email { get; set; }
        public bool IdManager { get; set; }
        public int Fk_TitleId { get; set; }
        public int Fk_DesginationId { get; set; }
        public int Fk_DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string DepartmentCode { get; set; }
        public string DesignationCode { get; set; }
        public string titleCode { get; set; }

        public SelectList TitileLists { get; set; }
        public SelectList DepartmentLists { get; set; }
        public SelectList Designationlists { get; set; }
    }
}