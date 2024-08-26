using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class CustomerController : Controller
    {
        readonly CustomerServices _ClientService = new CustomerServices();

        readonly CustomerGroupServices workGroupServices = new CustomerGroupServices();
        readonly IntroductionMasterServices introductionMasterServices = new IntroductionMasterServices();
        readonly SectorMasterServices sector = new SectorMasterServices();
        readonly InternationalReferalMasterServices internationalReferalMasterServices = new InternationalReferalMasterServices();
        readonly LegalStatusMasterServices legalStatusMasterServices = new LegalStatusMasterServices();
        readonly GradeMasterServices gradeMasterServices = new GradeMasterServices();
        readonly EmployeeServices employee = new EmployeeServices();



        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CustomerModel()
            {
                WorkGroupLists = new SelectList(workGroupServices.GetAll(), "Id", "CodeAndNarration"),
                PartnerLists = new SelectList(employee.GetAllIsPartner(), "Id", "Name"),
                IntroductionLists = new SelectList(introductionMasterServices.GetAll(), "Id", "CodeAndNarration"),
                SectorMasterLists = new SelectList(sector.GetAll(), "Id", "CodeAndNarration"),
                InternationslReferalLists = new SelectList(internationalReferalMasterServices.GetAll(), "Id", "CodeAndNarration"),
                LegalStatusMasterLists = new SelectList(legalStatusMasterServices.GetAll(), "Id", "CodeAndNarration"),
                GradeMasterLists = new SelectList(gradeMasterServices.GetAll(), "Id", "CodeAndNarration"),
                MangerLists = new SelectList(employee.GetAllIsManager(), "Id", "Name"),
                IsActive = true
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomerModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblCustomer
                    {
                        Name = masterModel.Name,
                        Code = masterModel.Code,
                        Address = masterModel.Address,
                        ContactPersonOne = masterModel.ContactPersonOne,
                        ContactPersonSecond = masterModel.ContactPersonSecond,
                        DateOfJoined = masterModel.DateOfJoined,
                        Email = masterModel.Email,
                        Fax = masterModel.Fax,
                        Fk_GradeMasterId = masterModel.Fk_GradeMasterId,
                        Fk_InternationslReferalId = masterModel.Fk_InternationslReferalId,
                        Fk_ManagerId = masterModel.Fk_ManagerId,
                        Fk_IntroductionId = masterModel.Fk_IntroductionId,
                        Fk_LegalStatusMasterId = masterModel.Fk_LegalStatusMasterId,
                        Fk_SectorMasterId = masterModel.Fk_SectorMasterId,
                        Fk_CustomerGroupId = masterModel.Fk_CustomerGroupId,
                        Fk_PartnerId = masterModel.Fk_PartnerId,
                        SVatNo = masterModel.SVatNo,
                        TaxNo = masterModel.TaxNo,
                        TelOne = masterModel.TelOne,
                        VatType = masterModel.VatType,
                        VatNo = masterModel.VatNo,
                        TelSecond = masterModel.TelSecond,


                        Create_By = "User",
                        IsActive = masterModel.IsActive,
                        Create_Date = new CommonResources().LocalDatetime().Date
                    };
                    return Json(_ClientService.Insert(tbl));
                };



            }
            catch (Exception)
            {
                return Json(new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                });
            }
            return Json(new MessageModel()
            {
                Status = "warning",
                Text = $"There was a error with retrieving data. Please try again",
            });
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var dt = _ClientService.GetById(Id);
            var model = new CustomerModel()
            {
                Id = dt.Id,
                Code = dt.Code,
                IsActive = dt.IsActive,
                Name = dt.Name,
                TelSecond = dt.TelSecond,
                VatNo = dt.VatNo,
                VatType = dt.VatType,
                TelOne = dt.TelOne,
                TaxNo = dt.TaxNo,
                SVatNo = dt.SVatNo,
                Fk_PartnerId = dt.Fk_PartnerId,
                Fk_CustomerGroupId = dt.Fk_CustomerGroupId,
                Fk_SectorMasterId = dt.Fk_SectorMasterId,
                Address = dt.Address,
                ContactPersonOne = dt.ContactPersonOne,
                ContactPersonSecond = dt.ContactPersonSecond,
                DateOfJoined = dt.DateOfJoined,
                Email = dt.Email,
                Fax = dt.Fax,
                Fk_GradeMasterId = dt.Fk_GradeMasterId,
                Fk_InternationslReferalId = dt.Fk_InternationslReferalId,
                Fk_IntroductionId = dt.Fk_IntroductionId,
                Fk_LegalStatusMasterId = dt.Fk_LegalStatusMasterId,
                Fk_ManagerId = dt.Fk_ManagerId,
                WorkGroupLists = new SelectList(workGroupServices.GetAll(), "Id", "CodeAndNarration"),
                PartnerLists = new SelectList(employee.GetAllIsPartner(), "Id", "Name"),
                IntroductionLists = new SelectList(introductionMasterServices.GetAll(), "Id", "CodeAndNarration"),
                SectorMasterLists = new SelectList(sector.GetAll(), "Id", "CodeAndNarration"),
                InternationslReferalLists = new SelectList(internationalReferalMasterServices.GetAll(), "Id", "CodeAndNarration"),
                LegalStatusMasterLists = new SelectList(legalStatusMasterServices.GetAll(), "Id", "CodeAndNarration"),
                GradeMasterLists = new SelectList(gradeMasterServices.GetAll(), "Id", "CodeAndNarration"),
                MangerLists = new SelectList(employee.GetAllIsManager(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblCustomer
                    {
                        Name = masterModel.Name,
                        Code = masterModel.Code,
                        Address = masterModel.Address,
                        ContactPersonOne = masterModel.ContactPersonOne,
                        ContactPersonSecond = masterModel.ContactPersonSecond,
                        DateOfJoined = masterModel.DateOfJoined,
                        Email = masterModel.Email,
                        Fax = masterModel.Fax,
                        Fk_GradeMasterId = masterModel.Fk_GradeMasterId,
                        Fk_InternationslReferalId = masterModel.Fk_InternationslReferalId,
                        Fk_ManagerId = masterModel.Fk_ManagerId,
                        Fk_IntroductionId = masterModel.Fk_IntroductionId,
                        Fk_LegalStatusMasterId = masterModel.Fk_LegalStatusMasterId,
                        Fk_SectorMasterId = masterModel.Fk_SectorMasterId,
                        Fk_CustomerGroupId = masterModel.Fk_CustomerGroupId,
                        Fk_PartnerId = masterModel.Fk_PartnerId,
                        SVatNo = masterModel.SVatNo,
                        TaxNo = masterModel.TaxNo,
                        TelOne = masterModel.TelOne,
                        VatType = masterModel.VatType,
                        VatNo = masterModel.VatNo,
                        TelSecond = masterModel.TelSecond,
                        Id = masterModel.Id,
                        Edit_By = "User",
                        IsActive = masterModel.IsActive,
                        Edit_Date = new CommonResources().LocalDatetime().Date
                    };
                    return Json(_ClientService.Update(tbl));
                }
            }
            catch (Exception)
            {
                return Json(new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                });
            }
            return Json(new MessageModel()
            {
                Status = "warning",
                Text = $"There was a error with retrieving data. Please try again",
            });
        }

        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            try
            {
                TblCustomer tbl = new TblCustomer();
                {
                    tbl.Id = ID;
                    tbl.Delete_By = "User";
                };
                return Json(_ClientService.Delete(tbl));
            }
            catch (Exception)
            {
                return Json(new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                });
            }

        }
    }
}