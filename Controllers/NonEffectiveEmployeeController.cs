using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class NonEffectiveEmployeeController : Controller
    {
        readonly NonEffectiveEmployeeServices _ClientService = new NonEffectiveEmployeeServices();
        readonly EmployeeServices employeeServices = new EmployeeServices();

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new NonEffectiveEmployeeModel()
            {
                selectListItems = new SelectList(employeeServices.GetAll(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NonEffectiveEmployeeModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblNonEffectiveEmployee tbl = new TblNonEffectiveEmployee();
                    {
                        tbl.Fk_EmployeeId = masterModel.Fk_EmployeeId;
                        tbl.NowEffective = masterModel.NowEffective;
                        tbl.Create_By = "User";
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
            var model = new NonEffectiveEmployeeModel()
            {
                Id = dt.Id,
                Fk_EmployeeId = dt.Fk_EmployeeId,
                NowEffective = dt.NowEffective,
                selectListItems = new SelectList(employeeServices.GetAll(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NonEffectiveEmployeeModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblNonEffectiveEmployee tbl = new TblNonEffectiveEmployee();
                    {
                        tbl.Fk_EmployeeId = masterModel.Fk_EmployeeId;
                        tbl.NowEffective = masterModel.NowEffective;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
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
                TblNonEffectiveEmployee tbl = new TblNonEffectiveEmployee();
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