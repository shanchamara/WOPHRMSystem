using AuditSystem.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class GeneralReceiptController : Controller
    {
        readonly GeneralReceiptServices _ClientService = new GeneralReceiptServices();
        readonly WorkGroupServices workGroup = new WorkGroupServices();
        readonly CustomerServices customer = new CustomerServices();
        readonly CompanyServices companyServices = new CompanyServices();
        readonly AccountServices accountServices = new AccountServices();

        #region Receipt Head

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            _ClientService.DeleteCurrentlyChequeTemp("User");
            var model = new GeneralReceiptModel()
            {
                ReceiptNo = _ClientService.GeTReceiptNo(),
                WorkGroupList = new SelectList(workGroup.GetAll(), "Id", "CodeAndNarration"),
                Companylist = new SelectList(companyServices.GetAll(), "Id", "Name"),
                CustomerList = new SelectList(customer.GetAll(), "Id", "Name"),
                AccountList = new SelectList(accountServices.GetAll(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(GeneralReceiptModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblGenaralReceipt
                    {
                        Create_By = "User",
                        IsActive = masterModel.IsActive,
                        Date = Convert.ToDateTime(masterModel.Date),
                        Fk_CompanyId = masterModel.Fk_CompanyId,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        Fk_WorkGroupId = masterModel.Fk_WorkGroupId,
                        Narration = masterModel.Narration,
                        PaymentType = masterModel.PaymentType,
                        ReceiptAmount = masterModel.ReceiptAmount,
                        ReceiptNo = masterModel.ReceiptNo,
                        Create_Date = new CommonResources().LocalDatetime().Date,
                        Fk_AccountId = masterModel.Fk_AccountId,
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

            if (dt.IsPrint == false)
            {


                _ClientService.DeleteCurrentlyTempAndInsertDataForUpdate("User", Id);


                var model = new GeneralReceiptModel()
                {
                    Id = Id,
                    ReceiptNo = dt.ReceiptNo,
                    ReceiptAmount = dt.ReceiptAmount,
                    PaymentType = dt.PaymentType,
                    Narration = dt.Narration,
                    Fk_WorkGroupId = dt.Fk_WorkGroupId,
                    Date = dt.Date,
                    Fk_CustomerId = dt.Fk_CustomerId,
                    Fk_CompanyId = dt.Fk_CompanyId,
                    Fk_AccountId = dt.Fk_AccountId,
                    WorkGroupList = new SelectList(workGroup.GetAll(), "Id", "CodeAndNarration"),
                    Companylist = new SelectList(companyServices.GetAll(), "Id", "Name"),
                    CustomerList = new SelectList(customer.GetAll(), "Id", "Name"),
                    AccountList = new SelectList(accountServices.GetAll(), "Id", "Name"),
                };
                return View(model);
            }
            else
            {
                return Json(new MessageModelTwo()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                    Value = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult CheckStatus(int Id)
        {
            var dt = _ClientService.GetById(Id);

            if (dt.IsPrint == false)
            {
                return RedirectToAction("Edit", new { Id });
            }
            else
            {
                return Json(new MessageModelTwo()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                    Value = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult Edit(GeneralReceiptModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblGenaralReceipt
                    {
                        Edit_By = "User",
                        IsActive = masterModel.IsActive,
                        Date = Convert.ToDateTime(masterModel.Date),
                        Fk_CompanyId = masterModel.Fk_CompanyId,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        Fk_WorkGroupId = masterModel.Fk_WorkGroupId,
                        Narration = masterModel.Narration,
                        PaymentType = masterModel.PaymentType,
                        ReceiptAmount = masterModel.ReceiptAmount,
                        ReceiptNo = masterModel.ReceiptNo,
                        Edit_Date = new CommonResources().LocalDatetime().Date,
                        Fk_AccountId = masterModel.Fk_AccountId,
                        Id = masterModel.Id,
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
                TblGenaralReceipt tbl = new TblGenaralReceipt();
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


        #region ReceiptChequelist

        [HttpGet]
        public ActionResult ReceiptChequeList()
        {
            var Create_By = "User";
            var model = new Chequeform()
            {
                ChequeModelData = _ClientService.GetAllChequeBody(Create_By),
                RowTotalAmount = _ClientService.GetAllChequeBodyRowTotalAmount(Create_By)
            };
            return PartialView("ReceiptChequeList", model);
        }


        [HttpPost]
        public ActionResult ReceiptBodyTempInsert(string cashTookDate, string details, decimal Amount, string chequeNo, string receivedDate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblChequeTempDetail tbl = new TblChequeTempDetail()
                    {
                        Create_By = "User",
                        Amount = Amount,
                        BankDetails = details,
                        CashTookDate = Convert.ToDateTime(cashTookDate),
                        ChequeNo = chequeNo,
                        TableName = "TblGeneralReceipt",
                        TableHeadId = 0,
                        ReceivedDate = Convert.ToDateTime(receivedDate),
                    };

                    return Json(_ClientService.Insert(tbl));
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
        public ActionResult ChequeDelete(int ID)
        {
            try
            {
                TblChequeTempDetail tbl = new TblChequeTempDetail();
                {
                    tbl.Id = ID;
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


    }
}