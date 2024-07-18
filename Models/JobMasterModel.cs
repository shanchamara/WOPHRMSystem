using System;
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
}