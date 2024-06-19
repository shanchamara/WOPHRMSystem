using System;
using System.Web.Mvc;

namespace WOPHRMSystem.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public int Fk_WorkGroupId { get; set; }
        public string WorkGroupCode { get; set; }
        public SelectList WorkGroupLists { get; set; }

        public string VatType { get; set; }
        public string Address { get; set; }
        public string TaxNo { get; set; }
        public string VatNo { get; set; }
        public string SVatNo { get; set; }
        public string Fax { get; set; }
        public string TelOne { get; set; }
        public string TelSecond { get; set; }

        public int Fk_GradeMasterId { get; set; }
        public string GradeMasterCode { get; set; }
        public SelectList GradeMasterLists { get; set; }

        public string Email { get; set; }
        public string ContactPersonOne { get; set; }
        public string ContactPersonSecond { get; set; }

        public int Fk_PartnerId { get; set; }
        public string Partner { get; set; }
        public SelectList PartnerLists { get; set; }

        public int Fk_ManagerId { get; set; }

        public string ManagerName { get; set; }
        public SelectList MangerLists { get; set; }

        public int Fk_LegalStatusMasterId { get; set; }
        public string LegalStatusMasterCode { get; set; }
        public SelectList LegalStatusMasterLists { get; set; }

        public int Fk_SectorMasterId { get; set; }

        public string SectorMasterCode { get; set; }
        public SelectList SectorMasterLists { get; set; }

        public int Fk_IntroductionId { get; set; }

        public string IntroductionCode { get; set; }
        public SelectList IntroductionLists { get; set; }

        public int Fk_InternationslReferalId { get; set; }
        public string InternationslReferalCode { get; set; }
        public SelectList InternationslReferalLists { get; set; }
        public Nullable<System.DateTime> DateOfJoined { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}