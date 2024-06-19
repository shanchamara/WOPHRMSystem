using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOPHRMSystem.Models
{
    public class EmployeeHourlyRateModel
    {
        public int Id { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int Fk_EmployeeId { get; set; }
    }
}