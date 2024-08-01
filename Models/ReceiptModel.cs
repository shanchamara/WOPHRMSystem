using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class ReceiptModel
    {
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Fk_WorkGroupId { get; set; }
        public int Fk_CustomerId { get; set; }
        public string Narration { get; set; }
        public int Fk_CompanyId { get; set; }
        public Nullable<decimal> ReceiptAmount { get; set; }
        public Nullable<decimal> NoNTaxAmount { get; set; }
        public string PaymentType { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public bool IsDelete { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Edit_By { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Delete_By { get; set; }
        public Nullable<System.DateTime> Delete_Date { get; set; }
        public string CustomerName { get; set; }
        public string WorkGroupName { get; set; }
        public string InvoiceNo { get; set; }
        public string CompanyName { get; set; }


        public SelectList WorkGroupList { get; set; }
        public SelectList CustomerList { get; set; }
        public SelectList Companylist { get; set; }
        public SelectList Invoicelist { get; set; }
    }
}