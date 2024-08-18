using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class JobMasterLocationTempModel
    {
        public int Id { get; set; }
        public int Fk_locationId { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public string Create_By { get; set; }
        public int CustomerId { get; set; }

        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    }

    public class ListLocationCustomerWise
    {
        public List<JobMasterLocationTempModel> JobMasterLocationTempModels { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public int Id { get; set; }
        public int Fk_locationId { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public string Create_By { get; set; }
        public int CustomerId { get; set; }

        public SelectList LocationSelectListItems { get; set; }
    }

    public class ListCurrentlyCustomerLocation
    {
        public List<LocationModel> LocationModels { get; set; }
    }



}