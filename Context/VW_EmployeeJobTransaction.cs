//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WOPHRMSystem.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class VW_EmployeeJobTransaction
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_EmployeeId { get; set; }
        public string JobCode { get; set; }
        public string CustomerCode { get; set; }
        public string EmployeeName { get; set; }
        public string LocationCode { get; set; }
        public string WorkTypeCode { get; set; }
        public string LocationName { get; set; }
        public string Narration { get; set; }
        public int Fk_LocationId { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public int Fk_WorkTypeId { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsDelete { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Edit_By { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Delete_By { get; set; }
        public Nullable<System.DateTime> Delete_Date { get; set; }
        public bool IsApplyTravelingCost { get; set; }
    }
}
