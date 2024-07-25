using AuditSystem.Services;
using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class InvoiceHeadController : Controller
    {
        readonly InvoiceHeadServices _ClientService = new InvoiceHeadServices();
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
        readonly DepartmentSecondServices secondServices = new DepartmentSecondServices();
        readonly DepartmentThirdServices thirdServices = new DepartmentThirdServices();

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
            var model = new InvoiceHeadModel()
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
        public ActionResult Create(InvoiceHeadModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblInvoiceHead
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
                        Create_Date = new CommonResources().LocalDatetime().Date,

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
            narrationMasterServices.DeleteCurrentlyTempAndInsertDataForUpdate("User", Id);
            var dt = _ClientService.GetById(Id);

            var model = new InvoiceHeadModel()
            {
                Id = Id,
                TotalReceivedAmount = dt.TotalReceivedAmount,
                ValueVAT = dt.ValueVAT,
                TotalAmount = dt.TotalAmount,
                ValueNBT = dt.ValueNBT,
                TaxType = dt.TaxType,
                WorkGroupLists = new SelectList(workGroup.GetAll(), "Id", "CodeAndNarration"),
                PartnerLists = new SelectList(employee.GetAllIsPartner(), "Id", "Name"),
                CompanyLists = new SelectList(companyServices.GetAll(), "Id", "Name"),
                MangerLists = new SelectList(employee.GetAllIsManager(), "Id", "Name"),
                CustomerLists = new SelectList(customer.GetAll(), "Id", "Name"),
                NarrationOne = " To Profressional Services render re - ",
                InvoiceShrotNarration = new SelectList(invoiceShortNarrationMasterServices.GetAll(), "Id", "CodeAndNarration"),
                DepartmentListOne = new SelectList(department.GetAll(), "Id", "CodeAndNarration"),
                DepartmentListThird = new SelectList(thirdServices.GetDepartmentThirdByDepartmentOneId(dt.Fk_DepartmentIdOne, dt.Fk_DepartmentIdTwo), "Id", "CodeAndNarration"),
                DepartmentListsecond = new SelectList(secondServices.GetDepartmentSecondByDepartmentOneId(dt.Fk_DepartmentIdOne), "Id", "CodeAndNarration"),
                NatureList = new SelectList(natureMasterServices.GetAll(), "Id", "CodeAndNarration"),
                JobList = new SelectList(jobMasterServices.GetAllJObsForIsReadytoInvoice(dt.Fk_CustomerId), "Fk_JobMasterId", "FullDetails"),
                Date = dt.Date,
                DocNo = dt.DocNo,
                Fk_CompanyId = dt.Fk_CompanyId,
                Fk_CustomerId = dt.Fk_CustomerId,
                Fk_DepartmentIdOne = dt.Fk_DepartmentIdOne,
                Fk_DepartmentIdThird = dt.Fk_DepartmentIdThird,
                Fk_DepartmentIdTwo = dt.Fk_DepartmentIdTwo,
                Fk_InvoiceShortNarrationId = dt.Fk_InvoiceShortNarrationId,
                Fk_JobMasterId = dt.Fk_JobMasterId,
                Fk_ManagerOne = dt.Fk_ManagerOne,
                Fk_ManagerSecond = dt.Fk_ManagerSecond,
                Fk_ManagerThird = dt.Fk_ManagerThird,
                Fk_NatureId = dt.Fk_NatureId,
                Fk_PartnerOne = dt.Fk_PartnerOne,
                Fk_PartnerSecond = dt.Fk_PartnerSecond,
                Fk_PartnerThird = dt.Fk_PartnerThird,
                Fk_WorkGroupId = dt.Fk_WorkGroupId,
                IsActive = dt.IsActive,
                IsActiveDate = dt.IsActiveDate,
                InvoiceNo = dt.InvoiceNo,
                IsDelete = dt.IsDelete,
                LastYearAmount = dt.LastYearAmount,
                NBTPercentage = dt.NBTPercentage,
                NarrationTwo = dt.NarrationTwo,
                NoNVat = dt.NoNVat,
                NoNVatPrecentage = dt.NoNVatPrecentage,
                OurReferance = dt.OurReferance,
                PostingDate = dt.PostingDate,
                VatPercentage = dt.VatPercentage,
                YourReferance = dt.YourReferance,



            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(InvoiceHeadModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblInvoiceHead
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
                        Create_Date = new CommonResources().LocalDatetime().Date,
                        Id = masterModel.Id,
                        IsMangerOneComfirm = masterModel.IsMangerOneComfirm,
                        //IsPostingToInvoice = masterModel.IsPostingToInvoice,
                        PartnerOneComfirmDate = masterModel.PartnerOneComfirmDate,
                        IsPartnerOneComfirm = masterModel.IsPartnerOneComfirm,
                        ManagerOneComfirmDate = masterModel.ManagerOneComfirmDate,
                        InvoiceNo = masterModel.InvoiceNo,

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
                TblInvoiceHead tbl = new TblInvoiceHead();
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
                InvoiceBodyModels = narrationMasterServices.GetAllInvoiceBody(Create_By),
                RowTotalAmount = narrationMasterServices.GetAllInvoiceBodyRowTotalAmount(Create_By)
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
                    TblInvoiceBodyTemp tbl = new TblInvoiceBodyTemp()
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
                    TblInvoiceBodyTemp tbl = new TblInvoiceBodyTemp()
                    {
                        Create_By = "User",
                        FK_CustomerId = CusId,
                        FK_JobMasterId = JobId,
                        Amount = Amount,
                        Fk_InvoiceNarrttionId = NarratiionId,
                        Id = Id,
                        BodyRowId = Id
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
                TblInvoiceBodyTemp tbl = new TblInvoiceBodyTemp();
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