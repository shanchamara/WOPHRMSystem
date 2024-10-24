using System;
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
        public bool IsManager { get; set; }
        public bool IsPartner { get; set; }
        public string JObPrefixCode { get; set; }
        public int Fk_TitleId { get; set; }
        public int Fk_DesginationId { get; set; }
        public int Fk_DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string DepartmentCode { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationCode { get; set; }
        public string titleCode { get; set; }

        public SelectList TitileLists { get; set; }
        public SelectList DepartmentLists { get; set; }
        public SelectList Designationlists { get; set; }
    }

    public partial class VW_EmployeeVisitingRatesModel
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public string EmployeeName { get; set; }
        public int Fk_EmployeeId { get; set; }
        public bool IsApplyTravelingCost { get; set; }
        public int Fk_LocationId { get; set; }
        public bool IsDelete { get; set; }
        public string LocationName { get; set; }
        public decimal Rate { get; set; }

        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }

        public SelectList EmployeeList { get; set; }// ToView
        public int FkFromEmployeeId { get; set; } // ToView
        public int FkToEmployeeId { get; set; } // ToView
    }



}