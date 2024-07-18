using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class InvoiceNarrationMasterModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeAndNarration { get; set; }
        public string Narration { get; set; }
        public decimal? Amount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


        
    }
}