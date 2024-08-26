using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class InvoiceBodyModel
    {
        public int Id { get; set; }
        public int BodyRowId { get; set; }
        public int Fk_InvoiceNarrttionId { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public int FK_CustomerId { get; set; }
        public int FK_JobMasterId { get; set; }
        public string Create_By { get; set; }

    }
    public class ListInvoiceBodyModel
    {
        public List<InvoiceBodyModel> InvoiceBodyModels { get; set; }

        public int Id { get; set; }
        public int BodyRowId { get; set; }
        public int Fk_InvoiceNarrttionId { get; set; }
        public string Code { get; set; }
        public string InvoiceNarration { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> RowTotalAmount { get; set; }
        public int FK_CustomerId { get; set; }
        public int FK_JobMasterId { get; set; }
        public string Create_By { get; set; }


        public List<InvoiceNarrationMasterModel> ListNarrations { get; set; }
        public SelectList SelectListItems { get; set; }
    }

}

