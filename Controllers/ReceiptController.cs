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
    public class ReceiptController : Controller
    {
        readonly ReceiptServices _ClientService = new ReceiptServices();
        readonly WorkGroupServices workGroup = new WorkGroupServices();
        readonly CustomerServices customer = new CustomerServices();
        readonly CompanyServices companyServices = new CompanyServices();

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
            _ClientService.DeleteCurrentlyInvoiceTemp("User");
            var model = new ReceiptModel()
            {
                ReceiptNo = _ClientService.GeTReceiptNo(),
                WorkGroupList = new SelectList(workGroup.GetAll(), "Id", "CodeAndNarration"),
                Companylist = new SelectList(companyServices.GetAll(), "Id", "Name"),
                CustomerList = new SelectList(customer.GetAll(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ReceiptModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblReceipt
                    {
                        Create_By = "User",
                        IsActive = masterModel.IsActive,
                        Date = Convert.ToDateTime(masterModel.Date),
                        Fk_CompanyId = masterModel.Fk_CompanyId,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        Fk_WorkGroupId = masterModel.Fk_WorkGroupId,
                        Narration = masterModel.Narration,
                        NoNTaxAmount = masterModel.NoNTaxAmount,
                        PaymentType = masterModel.PaymentType,
                        ReceiptAmount = masterModel.ReceiptAmount,
                        ReceiptNo = masterModel.ReceiptNo,
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


   

        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            try
            {
                TblReceipt tbl = new TblReceipt();
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

        #region Customer Wise Invoices 

        [HttpGet]
        public ActionResult GetCustomerrWiseInvoices(int id)
        {
            return PartialView("GetCustomerrWiseInvoices", _ClientService.GetAllInvoicesforListDown(id));
        }

        [HttpPost]
        public ActionResult PostInvoiceData(int cId, int InvId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblReceiptTempInvoice
                    {
                        Create_By = "User",
                        CustomerId = cId,
                        InvoiceId = InvId,

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


        [HttpDelete]
        public ActionResult DeleteInvoice(int ID)
        {
            try
            {
                TblReceiptTempInvoice tbl = new TblReceiptTempInvoice();
                {
                    tbl.InvoiceId = ID;
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


        [HttpGet]
        public ActionResult ViewSelectedInvoices()
        {
            return PartialView("ViewSelectedInvoices", _ClientService.GetAllInvoices("User"));
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
                        TableName = "TblReceipt",
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