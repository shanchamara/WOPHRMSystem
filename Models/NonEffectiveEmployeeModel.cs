using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class NonEffectiveEmployeeModel
    {
        public int Id { get; set; }
        public int Fk_EmployeeId { get; set; }
        public bool NowEffective { get; set; }
        public bool IsDelete { get; set; }
        public string EmployeeName { get; set; }

        public SelectList selectListItems { get; set; }
    }
}