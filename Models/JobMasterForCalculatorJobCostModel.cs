using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class JobMasterForCalculatorJobCostModel
    {
        public int PartnerTableId { get; set; }
        public int Fk_JobMasterId { get; set; }
        public Nullable<decimal> BudgetedHours { get; set; }
        public int Fk_CustomerId { get; set; }
        public string TypeOfTable { get; set; }
        public int TypeOfTableId { get; set; }
        public bool PartnersIsDelete { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int JobmasterId { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }  // FromDate
        public Nullable<System.DateTime> DueDate { get; set; }   // ToDate  
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }
        public bool IsActive { get; set; }
        public bool JobmasterIsDelete { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Edit_By { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Delete_By { get; set; }
        public Nullable<System.DateTime> Delete_Date { get; set; }
        public string CombinedName { get; set; }
        public string CombinedCode { get; set; }
        public Nullable<decimal> ActualValue { get; set; }
        public Nullable<decimal> BudgetValue { get; set; }
        public int VarianceValue { get; set; }
        public Nullable<System.DateTime> ReportGenaratedDate { get; set; }

        public int PartnerFrom { get; set; } // ToView
        public int PartnerTo { get; set; } // ToView
        public string PartnerName { get; set; } // ToView
        public SelectList PartnerList { get; set; }
    }
}