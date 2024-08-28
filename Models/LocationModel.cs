using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CodeAndNarration { get; set; }
        public SelectList CustomerLists { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    }

    public class ListLocationDetails
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public int Fk_CustomerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CodeAndNarration { get; set; }
        public SelectList CustomerLists { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public List<LocationModel> LocationModels { get; set; }
    }


    public class RateModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeAndNarration { get; set; }  // If this is relevant
        public string CustomerName { get; set; }
        public int Fk_CustomerId { get; set; }
        public string FromDate { get; set; }  // Assuming you want to keep the date as a string in JSON format
        public string ToDate { get; set; }    // Same as FromDate
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Narration { get; set; }
        public decimal Rate { get; set; }
    }
    public class CombinedModel
    {
        public LocationModel MasterModel { get; set; }
        public List<RateModel> Rates { get; set; }
    }
}