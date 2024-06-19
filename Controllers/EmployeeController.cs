using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;
using System;
using System.Web.Mvc;

namespace WOPHRMSystem.Controllers
{
    public class EmployeeController : Controller
    {
        readonly EmployeeServices _ClientService = new EmployeeServices();
        readonly DepartmentServices departmentServices = new DepartmentServices();
        readonly DesignationServices designationServices = new DesignationServices();
        readonly TitleServices titleServices = new TitleServices();

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EmployeeModel()
            {
                DepartmentLists = new SelectList(departmentServices.GetAll(), "Id", "CodeAndNarration"),
                Designationlists = new SelectList(designationServices.GetAll(), "Id", "CodeAndNarration"),
                TitileLists = new SelectList(titleServices.GetAll(), "Id", "CodeAndNarration"),
            };
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(EmployeeModel masterModel)
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
                        tbl.IdManager = masterModel.IdManager;
                        tbl.BirthDay = (DateTime)masterModel.BirthDay;
                        tbl.Fk_DesginationId = masterModel.Fk_DesginationId;
                        tbl.Fk_DepartmentId = masterModel.Fk_DepartmentId;
                        tbl.Fk_TitleId = masterModel.Fk_TitleId;
                        tbl.Code = masterModel.Code;
                        tbl.DateOfJoin = (DateTime)masterModel.DateOfJoin;
                        tbl.Create_By = "User";
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Create_Date = new CommonResources().LocalDatetime().Date;
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


        [HttpGet]
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
                IdManager = dt.IdManager,
                Name = dt.Name,
                Nic = dt.Nic,
                DateOfJoin = dt.DateOfJoin,
                DepartmentLists = new SelectList(departmentServices.GetAll(), "Id", "CodeAndNarration"),
                Designationlists = new SelectList(designationServices.GetAll(), "Id", "CodeAndNarration"),
                TitileLists = new SelectList(titleServices.GetAll(), "Id", "CodeAndNarration"),
            };
            return View(model);
        }

        [HttpPost]
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
                        tbl.IdManager = masterModel.IdManager;
                        tbl.BirthDay = (DateTime)masterModel.BirthDay;
                        tbl.Nic = masterModel.Nic;
                        tbl.Fk_DepartmentId = masterModel.Fk_DepartmentId;
                        tbl.Fk_TitleId = masterModel.Fk_TitleId;
                        tbl.Fk_DesginationId = masterModel.Fk_DesginationId;
                        tbl.IdManager = masterModel.IdManager;
                        tbl.Code = masterModel.Code;
                        tbl.DateOfJoin = (DateTime)masterModel.DateOfJoin;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
                        tbl.IsActive = masterModel.IsActive;
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
    }
}