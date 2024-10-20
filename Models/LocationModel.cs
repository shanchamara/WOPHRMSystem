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
      
        public int Fk_CustomerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CodeAndNarration { get; set; }
        public SelectList CustomerLists { get; set; }
      
    }

    public class ListLocationDetails
    {
        public int Id { get; set; }
     
        public Nullable<decimal> Rate { get; set; }
      
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public List<RateModel> LocationModels { get; set; }
    }


    public class RateModel
    {
        public int Id { get; set; }
        public int Fk_LocatonId { get; set; }
        public int Fk_CustomerId { get; set; }
        public string FromDate { get; set; }  
        public string ToDate { get; set; }   
        public decimal? Rate { get; set; }
    }
    public class CombinedModel
    {
        public LocationModel MasterModel { get; set; }
        public List<RateModel> Rates { get; set; }
    }


    public partial class VW_CustomerLocationRatesModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
        public int Fk_LocatonId { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<long> LocationNumber { get; set; }
    }
}