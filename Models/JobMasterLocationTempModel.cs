using System.Collections.Generic;
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
    }

    public class ListLocationCustomerWise
    {
        public List<JobMasterLocationTempModel> JobMasterLocationTempModels { get; set; }

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