using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class DepartmentSecondController : Controller
    {
        readonly DepartmentSecondServices _ClientService = new DepartmentSecondServices();
        readonly DepartmentServices department = new DepartmentServices();
        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new DepartmentSecondModel()
            {
                SelectListItems = new SelectList(department.GetAll(), "Id", "CodeAndNarration"),
            };
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(DepartmentSecondModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDepartmentSecond tbl = new TblDepartmentSecond();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Create_By = "User";
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Fk_DepartmentIdFirst = masterModel.Fk_DepartmentIdFirst;
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
            var model = new DepartmentSecondModel()
            {
                Id = dt.Id,
                Code = dt.Code,
                IsActive = dt.IsActive,
                Narration = dt.Narration,
                Fk_DepartmentIdFirst = dt.Fk_DepartmentIdFirst,
                SelectListItems = new SelectList(department.GetAll(), "Id", "CodeAndNarration"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentSecondModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDepartmentSecond tbl = new TblDepartmentSecond();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
                        tbl.Fk_DepartmentIdFirst = masterModel.Fk_DepartmentIdFirst;
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
                TblDepartmentSecond tbl = new TblDepartmentSecond();
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