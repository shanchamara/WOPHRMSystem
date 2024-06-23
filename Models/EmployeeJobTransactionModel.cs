﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class EmployeeJobTransactionModel
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

        public SelectList EmployeeList { get; set; }
    }

    public class ListEmployeeJobTransaction
    {
        public List<EmployeeJobTransactionModel> EmployeeJobTransactionModels { get; set; }
        public SelectList JobList { get; set; }

        public int Fk_JobMasterId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> TrDate { get; set; }

        public string Narration { get; set; }

        public int Fk_LocationId { get; set; }
        public int Fk_WorkTypeId { get; set; }
        public SelectList LocationList { get; set; }
        public SelectList WorkTypeList { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public string CustomerCode { get; set; }
        public string LocationName { get; set; }
        public int Fk_EmployeeId { get; set; }
        public int Fk_CustomerId { get; set; }
    }

}