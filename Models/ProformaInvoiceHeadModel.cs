using System;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class ProformaInvoiceHeadModel
    {
        public int Id { get; set; }
        public string InvoiceNoProforma { get; set; }
        public int Fk_DepartmentIdOne { get; set; }
        public int Fk_DepartmentIdTwo { get; set; }
        public int Fk_DepartmentIdThird { get; set; }
        public string DocNo { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string OurReferance { get; set; }
        public string YourReferance { get; set; }
        public int Fk_WorkGroupId { get; set; }
        public int Fk_CustomerId { get; set; }
        public int Fk_PartnerOne { get; set; }
        public int Fk_PartnerSecond { get; set; }
        public int Fk_PartnerThird { get; set; }
        public int Fk_ManagerOne { get; set; }
        public int Fk_ManagerSecond { get; set; }
        public int Fk_ManagerThird { get; set; }
        public string TaxType { get; set; }
        public int Fk_NatureId { get; set; }
        public string NoNVat { get; set; }
        public Nullable<decimal> NoNVatPrecentage { get; set; }
        public Nullable<decimal> VatPercentage { get; set; }
        public Nullable<decimal> NBTPercentage { get; set; }
        public string NarrationOne { get; set; }
        public string NarrationTwo { get; set; }
        public int Fk_InvoiceShortNarrationId { get; set; }
        public int Fk_JobMasterId { get; set; }
        public Nullable<decimal> LastYearAmount { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> IsActiveDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalReceivedAmount { get; set; }
        public Nullable<decimal> ValueNBT { get; set; }
        public Nullable<decimal> ValueVAT { get; set; }
        public string DepartmentOneName { get; set; }
        public string DepartmentTwoName { get; set; }
        public string DepartmentThreeName { get; set; }
        public string WorkGroupName { get; set; }
        public string CustomerName { get; set; }
        public string PartnerOneName { get; set; }
        public string PartnerTwoName { get; set; }
        public string PartnerThreeName { get; set; }
        public string ManagerOneName { get; set; }
        public string ManagerTwoName { get; set; }
        public string ManagerThreeName { get; set; }

        public int Fk_CompanyId { get; set; }
        public string CompanyName { get; set; }

        public SelectList WorkGroupLists { get; set; }
        public SelectList PartnerLists { get; set; }
        public SelectList CompanyLists { get; set; }
        public SelectList MangerLists { get; set; }
        public SelectList CustomerLists { get; set; }
        public SelectList InvoiceShrotNarration { get; set; }
        public SelectList DepartmentListOne { get; set; }
        public SelectList NatureList { get; set; }
        public SelectList JobList { get; set; }
    }
}