﻿using System;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class JobMasterModel
    {
        public int Id { get; set; }
        public string JobCode { get; set; }
        public string DefaultJobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public int Fk_CustomerId { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string JObPrefixCode { get; set; }
        public string CustomerName { get; set; }
        public SelectList CustomerSelectListItems { get; set; }

        public int PartnerId { get; set; }

        public SelectList PartnerSelectListItems { get; set; }

        public int ManagerId { get; set; }
        public SelectList ManagerSelectListItems { get; set; }

    }


    public partial class JObWiseCositingDetailsWithAssignEmployeeModel
    {
        public int PartnerTableId { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_CustomerId { get; set; }
        public string TypeOfTable { get; set; }
        public int EmployeeNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public Nullable<decimal> BudgetedHours { get; set; }
        public decimal BudgetedValue { get; set; }
        public decimal ActualHours { get; set; }
        public decimal ActualValue { get; set; }
        public decimal LocationRate { get; set; }
        public Nullable<decimal> EmployeeRateValue { get; set; }
        public decimal HoursVarianceValue { get; set; }
        public decimal VarianceValue { get; set; }

        public Nullable<System.DateTime> FromDate { get; set; }// ToView
        public SelectList JObList { get; set; }// ToView
        public bool IsPending { get; set; }
        public bool IsBODOffice { get; set; }
        public bool IsOtherCustomer { get; set; }


    }


    public partial class VW_WIPReportDailyAndMonthlyModel
    {
        public int PartnerTableId { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_CustomerId { get; set; }
        public string TypeOfTable { get; set; }
        public int EmployeeNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public string Month { get; set; }
        public string NarrationTrans { get; set; }
        public Nullable<decimal> BudgetedHours { get; set; }
        public decimal BudgetedValue { get; set; }
        public decimal ActualHours { get; set; }
        public decimal ActualValue { get; set; }
        public decimal LocationRate { get; set; }
        public Nullable<decimal> EmployeeRateValue { get; set; }
        public decimal HoursVarianceValue { get; set; }
        public decimal VarianceValue { get; set; }
        public SelectList JObList { get; set; }// ToView
    }


    public partial class VW_WIPReportMonthlyModel
    {
        public Nullable<decimal> TotalValue { get; set; }
        public int MonthNumber { get; set; }
        public string Month { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_EmployeeId { get; set; }
        public string Narration { get; set; }
        public decimal TotalHours { get; set; }
        public decimal EmployeeRateValue { get; set; }
        public string JobCode { get; set; }
        public string JobNarration { get; set; }
        public Nullable<int> Fk_CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string EmployeeName { get; set; }
        public string DesignationName { get; set; }

        public SelectList JObList { get; set; }// ToView
    }


    public partial class VW_JobMasterForReportModel
    {
        public int Id { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string JObPrefixCode { get; set; }
        public int Fk_MangerId { get; set; }
        public int Fk_PartnerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Edit_By { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Delete_By { get; set; }
        public Nullable<System.DateTime> Delete_Date { get; set; }
        public string Name { get; set; }
    }
}

