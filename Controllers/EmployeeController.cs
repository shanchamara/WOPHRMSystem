using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class EmployeeController : Controller
    {
        readonly EmployeeServices _ClientService = new EmployeeServices();
        readonly DepartmentServices departmentServices = new DepartmentServices();
        readonly DesignationServices designationServices = new DesignationServices();
        readonly TitleServices titleServices = new TitleServices();

        #region Head

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult GetLoadPrefixCode()
        {
            var dt = _ClientService.GetAllJobPrefixCode();
            return Json((new MessageModelWithData<List<EmployeeModel>>()
            {
                DataList = dt
            }), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            var model = new EmployeeModel()
            {
                DepartmentLists = new SelectList(departmentServices.GetAll(), "Id", "CodeAndNarration"),
                Designationlists = new SelectList(designationServices.GetAll(), "Id", "CodeAndNarration"),
                TitileLists = new SelectList(titleServices.GetAll(), "Id", "CodeAndNarration"),
                IsActive = true
            };
            return View(model);

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(EmployeeModel masterModel, List<ListEmployeeRate> rates)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblEmployee tbl = new TblEmployee();
                    {
                        tbl.Name = masterModel.Name;
                        tbl.Email = masterModel.Email;
                        tbl.Nic = masterModel.Nic;
                        tbl.IsManager = masterModel.IsManager;
                        tbl.IsPartner = masterModel.IsPartner;
                        tbl.BirthDay = (DateTime)masterModel.BirthDay;
                        tbl.Fk_DesginationId = masterModel.Fk_DesginationId;
                        tbl.Fk_DepartmentId = masterModel.Fk_DepartmentId;
                        tbl.Fk_TitleId = masterModel.Fk_TitleId;
                        tbl.Code = masterModel.Code;
                        tbl.DateOfJoin = (DateTime)masterModel.DateOfJoin;
                        tbl.Create_By = "User";
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Create_Date = new CommonResources().LocalDatetime().Date;
                        tbl.JObPrefixCode = masterModel.JObPrefixCode;
                    };

                    return Json(_ClientService.Insert(tbl, rates));
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


        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int Id)
        {
            var dt = _ClientService.GetById(Id);
            var model = new EmployeeModel()
            {
                Id = dt.Id,
                Code = dt.Code,
                IsActive = dt.IsActive,
                Email = dt.Email,
                Fk_TitleId = dt.Fk_TitleId,
                Fk_DepartmentId = dt.Fk_DepartmentId,
                Fk_DesginationId = dt.Fk_DesginationId,
                BirthDay = dt.BirthDay,
                IsManager = dt.IsManager,
                IsPartner = dt.IsPartner,
                Name = dt.Name,
                Nic = dt.Nic,
                JObPrefixCode = dt.JObPrefixCode,
                DateOfJoin = dt.DateOfJoin,
                DepartmentLists = new SelectList(departmentServices.GetAll(), "Id", "CodeAndNarration"),
                Designationlists = new SelectList(designationServices.GetAll(), "Id", "CodeAndNarration"),
                TitileLists = new SelectList(titleServices.GetAll(), "Id", "CodeAndNarration"),
            };
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(EmployeeModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblEmployee tbl = new TblEmployee();
                    {
                        tbl.Name = masterModel.Name;
                        tbl.Email = masterModel.Email;
                        tbl.IsPartner = masterModel.IsPartner;
                        tbl.BirthDay = (DateTime)masterModel.BirthDay;
                        tbl.Nic = masterModel.Nic;
                        tbl.Fk_DepartmentId = masterModel.Fk_DepartmentId;
                        tbl.Fk_TitleId = masterModel.Fk_TitleId;
                        tbl.Fk_DesginationId = masterModel.Fk_DesginationId;
                        tbl.IsManager = masterModel.IsManager;
                        tbl.Code = masterModel.Code;
                        tbl.DateOfJoin = (DateTime)masterModel.DateOfJoin;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
                        tbl.IsActive = masterModel.IsActive;
                        tbl.JObPrefixCode = masterModel.JObPrefixCode;
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

        [System.Web.Mvc.HttpDelete]
        public ActionResult Delete(int ID)
        {
            try
            {
                TblEmployee tbl = new TblEmployee();
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


        #region Body Add Hourly Rate 

        [System.Web.Mvc.HttpGet]
        public ActionResult ViewEmployeeHourlyRate(int Fk_employeeId)
        {
            var data = _ClientService.GetAllEmployeeWiseRateList(Fk_employeeId);
            var model = new ListEmployeeRate() { EmployeeHourlyRateModels = data, Designationlists = new SelectList(designationServices.GetAll(), "Id", "CodeAndNarration"), };
            return PartialView("ViewEmployeeHourlyRate", model);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult ViewEmployeeHourlyRateTemperley()
        {
            var data = _ClientService.GetAllEmployeeWiseRateList(0);
            var model = new ListEmployeeRate()
            {
                EmployeeHourlyRateModels = data,
                Designationlists = new SelectList(designationServices.GetAll(), "Id", "CodeAndNarration"),
            };
            return PartialView("ViewEmployeeHourlyRateTemperley", model);
        }


        [System.Web.Mvc.HttpDelete]
        public ActionResult DeleteHourlyRate(int ID)
        {
            try
            {
                TblEmployeeHourlyRate tbl = new TblEmployeeHourlyRate();
                {
                    tbl.Id = ID;
                    tbl.Delete_By = "User";
                };
                return Json(_ClientService.DeleteHourlyRate(tbl));
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

        [System.Web.Mvc.HttpPost]
        public ActionResult PostEmployeeRate(int Fk_EmployeeId, string ToDate, string FromDate, decimal Rate, int Fk_DesginationId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblEmployeeHourlyRate tbl = new TblEmployeeHourlyRate
                    {

                        Rate = Rate,
                        FromDate = Convert.ToDateTime(FromDate),
                        ToDate = Convert.ToDateTime(ToDate),
                        Fk_EmployeeId = Fk_EmployeeId,
                        Create_By = "User",
                        Fk_DesginationId = Fk_DesginationId,
                        Create_Date = new CommonResources().LocalDatetime().Date,
                    };

                    return Json(_ClientService.InsertRate(tbl));
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

        #endregion
    }
}