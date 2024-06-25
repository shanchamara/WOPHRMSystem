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
    
    public partial class TblJobMaster
    {
        public TblJobMaster()
        {
            this.TblJobMasterLocations = new HashSet<TblJobMasterLocation>();
            this.TblJobMasterPartners = new HashSet<TblJobMasterPartner>();
            this.TblJobTransactions = new HashSet<TblJobTransaction>();
        }
    
        public int Id { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Edit_By { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Delete_By { get; set; }
        public Nullable<System.DateTime> Delete_Date { get; set; }
    
        public virtual TblCustomer TblCustomer { get; set; }
        public virtual ICollection<TblJobMasterLocation> TblJobMasterLocations { get; set; }
        public virtual ICollection<TblJobMasterPartner> TblJobMasterPartners { get; set; }
        public virtual ICollection<TblJobTransaction> TblJobTransactions { get; set; }
    }
}