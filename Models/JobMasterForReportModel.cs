using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class JobMasterForReportModel
    {
        public int PartnerTableId { get; set; }
        public int Fk_JobMasterId { get; set; }
        public Nullable<decimal> BudgetedHours { get; set; }
        public int Fk_CustomerId { get; set; }
        public string TypeOfTable { get; set; }
        public int TypeOfTableId { get; set; }
        public string ManagerCode { get; set; }
        public string ManagerName { get; set; }
        public string PartnerCode { get; set; }
        public string PartnerName { get; set; }
        public bool PartnersIsDelete { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int JobmasterId { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
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
        public bool ISPending { get; set; }
        public Nullable<System.DateTime> ReportGenaratedDate { get; set; }

        public bool IsManager { get; set; }

        public bool IsPartner { get; set; }
        public bool AllProject { get; set; }

        public SelectList JObList { get; set; }// ToView
        public int FkFromJObId { get; set; } // ToView
        public int FkToJobId { get; set; } // ToView
    }


    public partial class VW_DataEntryEmployeesWiseModel
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_EmployeeId { get; set; }
        public string Narration { get; set; }
        public bool IsApplyTravelingCost { get; set; }
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
        public string locationsName { get; set; }
        public string WorkName { get; set; }
        public string CustomerName { get; set; }
        public string JobCode { get; set; }

        public SelectList EmployeeList { get; set; }// ToView
        public int FkFromEmployeeId { get; set; } // ToView
        public int FkToEmployeeId { get; set; } // ToView
    }


    public partial class VW_DataEntryDetailsModel
    {
        public Nullable<System.DateTime> ToDate { get; set; }// ToView
        public Nullable<System.DateTime> FromDate { get; set; }// ToView
        public bool IsManager { get; set; }

        public bool IsPartner { get; set; }


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
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }
        public bool IsActive { get; set; }
        public bool JobmasterIsDelete { get; set; }
        public string CombinedName { get; set; }
        public string CombinedCode { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public string TransNarration { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public string LocationsName { get; set; }
        public string WorkName { get; set; }
    }


    public class StaffUtilizationStatementEmployeeWiseJobModel
    {
        public Nullable<System.DateTime> ToDate { get; set; }// ToView
        public Nullable<System.DateTime> FromDate { get; set; }// ToView

        public SelectList EmployeeList { get; set; }// ToView
        public int FkFromEmployeeId { get; set; } // ToView
        public bool EmployeeWise { get; set; }// ToView
        public bool JobWise { get; set; }// ToView
        public List<EmployeeSectionList> EmployeeSectionLists { get; set; }

        public SelectList JObList { get; set; }// ToView
        public int FkFromJObId { get; set; } // ToView
    }

    public class EmployeeSectionList
    {
        public string Name { get; set; } // ToView
        public int FkFromEmployeeId { get; set; } // ToView
    }

    public class JobSectionList
    {
        
        public int FkFromJObId { get; set; } // ToView
        public string Name { get; set; } // ToView
        
    }
}
