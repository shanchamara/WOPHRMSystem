using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class JobTransactionController : Controller
    {
        readonly EmployeeJobTransactionServices _ClientService = new EmployeeJobTransactionServices();
        readonly WorkTypeServices workTypeServices = new WorkTypeServices();

        // GET: JobTransaction
        [HttpGet]
        public ActionResult Index()
        {
            var model = new EmployeeJobTransactionModel
            {
                EmployeeList = new SelectList(_ClientService.GetAll(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult WorkingHoursViewByEmployee(int id)
        {
            var data = _ClientService.GetEmployeeWiseTransaction(id);
            var JobList = _ClientService.GetEmployeeWiseJob(id);
            var model = new ListEmployeeJobTransaction()
            {
                EmployeeJobTransactionModels = data,
                JobList = new SelectList(JobList, "Id", "JobCode"),
                //LocationList = new SelectList(location.GetAll(), "Id", "Code"),
                WorkTypeList = new SelectList(workTypeServices.GetAll(), "Id", "Code"),
            };
            return PartialView("WorkingHoursViewByEmployee", model);
        }

        [HttpGet]
        public ActionResult GetCustomerCode(int id)
        {
            var customerCode = _ClientService.GetCustomerCode(id); ; // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetLocationName(int id)
        {
            var customerCode = _ClientService.GetLocationName(id); ; // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult PostData(ListEmployeeJobTransaction masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = new TblJobTransaction
                    {


                        Narration = masterModel.Narration,
                        Fk_EmployeeId = masterModel.Fk_EmployeeId,
                        Fk_LocationId = masterModel.Fk_LocationId,
                        Fk_JobMasterId = masterModel.Fk_JobMasterId,
                        Fk_WorkTypeId = masterModel.Fk_WorkTypeId,
                        Hours = masterModel.Hours,
                        TrDate = masterModel.TrDate,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        Create_By = "User",
                        Create_Date = new CommonResources().LocalDatetime().Date,
                        IsApplyTravelingCost = masterModel.IsApplyTravelingCost,
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

    }
}