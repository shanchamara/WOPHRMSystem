using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class JobMasterCompletedModel
    {
        public int Id { get; set; }
        public string JobCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> PreViewvalue { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public int Fk_CustomerId { get; set; }

        public bool IsActive { get; set; }


        public string CustomerName { get; set; }
        public SelectList JobList { get; set; }

        public Nullable<System.DateTime> CompletedDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReActivate { get; set; }
        public Nullable<System.DateTime> ReActivateDate { get; set; }

        public bool IsDelete { get; set; }
    }
}