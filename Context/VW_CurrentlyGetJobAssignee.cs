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
    
    public partial class VW_CurrentlyGetJobAssignee
    {
        public int Id { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_CustomerId { get; set; }
        public string TypeOfTable { get; set; }
        public int TypeOfTableId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<decimal> BudgetedHours { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Edit_By { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Delete_By { get; set; }
        public Nullable<System.DateTime> Delete_Date { get; set; }
        public string CombinedName { get; set; }
        public string CombinedCode { get; set; }
        public string DesignationCode { get; set; }
    }
}
