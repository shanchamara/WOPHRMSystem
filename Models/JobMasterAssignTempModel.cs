using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class JobMasterAssignTempModel
    {
        public int Id { get; set; }
        public string TypeOftable { get; set; }
        public int TypeOftableId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> BudgetedHours { get; set; }
        public string Designation { get; set; }
        public string Create_By { get; set; }

        public string CombineName { get; set; }


        public bool IsManager { get; set; }
        public bool IsPartner { get; set; }

    }


    public class ListViewCurrentlyAssignees
    {
        public List<JobMasterAssignTempModel> JobMasterAssignTempModels { get; set; }


        public int Id { get; set; }
        public string TypeOftable { get; set; }
        public int TypeOftableId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> BudgetedHours1 { get; set; }
        public string Designation { get; set; }
        public string Create_By { get; set; }

        public SelectList PartnerList { get; set; }
        public int EmployeeId { get; set; }
    }

    public class ListViewCurrentlyEmployees_partners_Managers
    {
        public List<JobMasterAssignTempModel> JobMasterAssignTempModels { get; set; }
    }
}