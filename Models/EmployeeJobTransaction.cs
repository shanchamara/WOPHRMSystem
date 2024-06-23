using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOPHRMSystem.Models
{
    public class EmployeeJobTransaction
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public int Fk_JobMasterId { get; set; }
        public int Fk_EmployeeId { get; set; }
        public string JobCode { get; set; }
        public string CustomerCode { get; set; }
        public string EmployeeName { get; set; }
        public string LocationCode { get; set; }
        public string WorkTypeCode { get; set; }
        public string LocationName { get; set; }
        public string Narration { get; set; }
        public int Fk_LocationId { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public int Fk_WorkTypeId { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsDelete { get; set; }
    }
}