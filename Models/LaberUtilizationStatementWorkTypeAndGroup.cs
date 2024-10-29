using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class LaberUtilizationStatementWorkTypeAndGroup
    {
        public Nullable<int> TransactionId { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public Nullable<int> Fk_JobMasterId { get; set; }
        public Nullable<int> Fk_EmployeeId { get; set; }
        public string Narration { get; set; }
        public string JobCode { get; set; }
        public Nullable<bool> IsApplyTravelingCost { get; set; }
        public Nullable<int> Fk_LocationId { get; set; }
        public Nullable<decimal> WorkingHours { get; set; }
        public Nullable<int> Fk_WorkTypeId { get; set; }
        public Nullable<int> Fk_CustomerId { get; set; }
        public string Groups { get; set; }
        public string Worktypes { get; set; }
        public string EmployeeName { get; set; }
    }

    public class LaberUtilizationStatementWorkTypeAndGroupReportModel
    {
        public Nullable<System.DateTime> FromDate { get; set; }// ToView
        public Nullable<System.DateTime> ToDate { get; set; }// ToView

        public SelectList EmployeeList { get; set; }// ToView
        public int FkFromEmployeeId { get; set; } // ToView
        public int FkToEmployeeId { get; set; } // ToView


        public SelectList JObList { get; set; }// ToView
        public int FkFromJObId { get; set; } // ToView
        public int FkToJobId { get; set; } // ToView

        public bool IsGroupType { get; set; }
        public bool IsWorkType { get; set; }
    }


    public partial class VW_LaberUtilizationJObWiseModel
    {
        public Nullable<int> TransactionId { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public Nullable<int> Fk_JobMasterId { get; set; }
        public Nullable<int> Fk_EmployeeId { get; set; }
        public string Narration { get; set; }
        public string JobCode { get; set; }
        public Nullable<bool> IsApplyTravelingCost { get; set; }
        public Nullable<int> Fk_LocationId { get; set; }
        public Nullable<decimal> WorkingHours { get; set; }
        public Nullable<int> Fk_WorkTypeId { get; set; }
        public Nullable<int> Fk_CustomerId { get; set; }
        public string Groups { get; set; }
        public string Worktypes { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }


        public Nullable<System.DateTime> FromDate { get; set; }// ToView
        public Nullable<System.DateTime> ToDate { get; set; }// ToView

        public SelectList EmployeeList { get; set; }// ToView
        public int FkFromEmployeeId { get; set; } // ToView
        public int FkToEmployeeId { get; set; } // ToView


        public SelectList JObList { get; set; }// ToView
        public int FkFromJObId { get; set; } // ToView
        public int FkToJobId { get; set; } // ToView

        public bool IsGroupType { get; set; }
        public bool IsWorkType { get; set; }
    }
}
