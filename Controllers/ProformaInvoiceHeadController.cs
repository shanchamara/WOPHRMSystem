using AuditSystem.Services;
using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class ProformaInvoiceHeadController : Controller
    {
        readonly ProformaInvoiceHeadServices _ClientService = new ProformaInvoiceHeadServices();
        readonly NatureMasterServices natureMasterServices = new NatureMasterServices();
        readonly DepartmentServices department = new DepartmentServices();
        readonly EmployeeServices employee = new EmployeeServices();
        readonly CustomerServices customer = new CustomerServices();
        readonly WorkGroupServices workGroup = new WorkGroupServices();
        readonly InvoiceShortNarrationMasterServices invoiceShortNarrationMasterServices = new InvoiceShortNarrationMasterServices();
        readonly DepartmentSecondServices departmentSecond = new DepartmentSecondServices();
        readonly DepartmentThirdServices departmentThird = new DepartmentThirdServices();
        readonly JobMasterServices jobMasterServices = new JobMasterServices();
        readonly CompanyServices companyServices = new CompanyServices();
        readonly InvoiceNarrationMasterServices narrationMasterServices = new InvoiceNarrationMasterServices();


        #region InvoiceHead

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            narrationMasterServices.DeleteCurrentlyTemp("User");
            var model = new ProformaInvoiceHeadModel()
            {
                WorkGroupLists = new SelectList(workGroup.GetAll(), "Id", "CodeAndNarration"),
                PartnerLists = new SelectList(employee.GetAllIsPartner(), "Id", "Name"),
                CompanyLists = new SelectList(companyServices.GetAll(), "Id", "Name"),
                MangerLists = new SelectList(employee.GetAllIsManager(), "Id", "Name"),
                CustomerLists = new SelectList(customer.GetAll(), "Id", "Name"),
                NarrationOne = " To Profressional Services render re - ",
                InvoiceShrotNarration = new SelectList(invoiceShortNarrationMasterServices.GetAll(), "Id", "CodeAndNarration"),
                DepartmentListOne = new SelectList(department.GetAll(), "Id", "CodeAndNarration"),
                NatureList = new SelectList(natureMasterServices.GetAll(), "Id", "CodeAndNarration"),
                NBTPercentage = 2,
                VatPercentage = 15,
                DocNo = _ClientService.GetDOCNo(),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProformaInvoiceHeadModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblProformaInvoiceHead
                    {


                        Create_By = "User",
                        IsActive = masterModel.IsActive,
                        Fk_PartnerSecond = masterModel.Fk_PartnerSecond,
                        Fk_PartnerOne = masterModel.Fk_PartnerOne,
                        Fk_ManagerSecond = masterModel.Fk_ManagerSecond,
                        Fk_NatureId = masterModel.Fk_NatureId,
                        Fk_PartnerThird = masterModel.Fk_PartnerThird,
                        Fk_ManagerThird = masterModel.Fk_ManagerThird,
                        Fk_WorkGroupId = masterModel.Fk_WorkGroupId,
                        Fk_JobMasterId = masterModel.Fk_JobMasterId,
                        Fk_InvoiceShortNarrationId = masterModel.Fk_InvoiceShortNarrationId,
                        Fk_DepartmentIdOne = masterModel.Fk_DepartmentIdOne,
                        Fk_DepartmentIdThird = masterModel.Fk_DepartmentIdThird,
                        Date = Convert.ToDateTime(masterModel.Date),
                        DocNo = masterModel.DocNo,
                        Fk_CompanyId = masterModel.Fk_CompanyId,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        Fk_DepartmentIdTwo = masterModel.Fk_DepartmentIdTwo,
                        Fk_ManagerOne = masterModel.Fk_ManagerOne,
                        IsActiveDate = masterModel.IsActiveDate,
                        LastYearAmount = masterModel.LastYearAmount,
                        NoNVat = masterModel.NoNVat,
                        NBTPercentage = masterModel.NBTPercentage,
                        NoNVatPrecentage = masterModel.NoNVatPrecentage,
                        OurReferance = masterModel.OurReferance,
                        TaxType = masterModel.TaxType,
                        ValueNBT = masterModel.ValueNBT,
                        VatPercentage = masterModel.VatPercentage,
                        YourReferance = masterModel.YourReferance,
                        ValueVAT = masterModel.ValueVAT,
                        TotalReceivedAmount = masterModel.TotalReceivedAmount,
                        TotalAmount = masterModel.TotalAmount,
                        PostingDate = masterModel.PostingDate,
                        NarrationOne = masterModel.NarrationOne,
                        NarrationTwo = masterModel.NarrationTwo,
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
            var model = new ProformaInvoiceHeadModel()
            {

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProformaInvoiceHeadModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblProformaInvoiceHead
                    {

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
                TblProformaInvoiceHead tbl = new TblProformaInvoiceHead();
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

        #endregion


        #region Set DropDown List 
        //// Set Departments 
        ///

        [HttpGet]
        public ActionResult GetDepartmentTwo(int id)
        {
            var customerCode = departmentSecond.GetDepartmentSecondByDepartmentOneId(id); // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult GetDepartmentThird(int Firstid, int Secondid)
        {
            var customerCode = departmentThird.GetDepartmentThirdByDepartmentOneId(Firstid, Secondid);  // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetCustomerWiseJob(int CustomerId)
        {
            var customerCode = jobMasterServices.GetAllJObsForIsReadytoInvoice(CustomerId);  // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetSelectedValueLastAmountTotalAmount(int CustomerId, int JobId)
        {
            narrationMasterServices.DeleteCurrentlyTemp("User");
            var customerCode = jobMasterServices.GetAllJObsForIsReadytoInvoiceById(CustomerId, JobId); // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }


        #endregion



        #region InvoiceNarrtionSection

        [HttpGet]
        public ActionResult InvoiceNarrationList()
        {
            var Create_By = "User";
            var model = new ListInvoiceBodyModel()
            {
                SelectListItems = new SelectList(narrationMasterServices.GetAll(), "Id", "CodeAndNarration"),
                InvoiceBodyModels = narrationMasterServices.GetAllProformaInvoiceBody(Create_By),
                RowTotalAmount = narrationMasterServices.GetAllProformaInvoiceBodyRowTotalAmount(Create_By)
            };

            return PartialView("InvoiceNarrationList", model);
        }


        [HttpPost]
        public ActionResult InvoiceBodyTempInsert(int CusId, int JobId, int NarratiionId, decimal Amount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblProformaInvoiceBodyTemp tbl = new TblProformaInvoiceBodyTemp()
                    {
                        Create_By = "User",
                        FK_CustomerId = CusId,
                        FK_JobMasterId = JobId,
                        Amount = Amount,
                        Fk_InvoiceNarrttionId = NarratiionId
                    };

                    return Json(narrationMasterServices.Insert(tbl));
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


        [HttpPost]
        public ActionResult InvoiceBodyTempUpdate(int Id, int CusId, int JobId, int NarratiionId, decimal Amount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblProformaInvoiceBodyTemp tbl = new TblProformaInvoiceBodyTemp()
                    {
                        Create_By = "User",
                        FK_CustomerId = CusId,
                        FK_JobMasterId = JobId,
                        Amount = Amount,
                        Fk_InvoiceNarrttionId = NarratiionId,
                        Id = Id
                    };

                    return Json(narrationMasterServices.Update(tbl));
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
        public ActionResult InvoiceBodyDelete(int ID)
        {
            try
            {
                TblProformaInvoiceBodyTemp tbl = new TblProformaInvoiceBodyTemp();
                {
                    tbl.Id = ID;
                };
                return Json(narrationMasterServices.Delete(tbl));
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
        #endregion 
    }
}