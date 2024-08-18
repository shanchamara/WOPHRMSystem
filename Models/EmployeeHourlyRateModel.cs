using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class EmployeeHourlyRateModel
    {
        public int Id { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int Fk_EmployeeId { get; set; }
        public bool IsDelete { get; set; }
        public int Fk_DesginationId { get; set; }
        public string DesignationName { get; set; }
    }

    public class ListEmployeeRate
    {
        public List<EmployeeHourlyRateModel> EmployeeHourlyRateModels { get; set; }
        public int Id { get; set; }
        public Nullable<decimal> Rate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public int Fk_EmployeeId { get; set; }
        public int Fk_DesginationId { get; set; }
        public bool IsDelete { get; set; }

        public SelectList Designationlists { get; set; }
    }
}