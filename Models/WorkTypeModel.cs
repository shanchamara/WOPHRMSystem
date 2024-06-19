using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class WorkTypeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool Billable { get; set; }
        public int Fk_WorkGroupId { get; set; }
        public string GroupCode { get; set; }
        public SelectList WorkGroupLists { get; set; }
    }
}