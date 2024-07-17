using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class DepartmentThirdController : Controller
    {
        readonly DepartmentThirdServices _ClientService = new DepartmentThirdServices();
        readonly DepartmentServices department = new DepartmentServices();
        readonly DepartmentSecondServices departmentSecond = new DepartmentSecondServices();
        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new DepartmentThirdModel()
            {
                SelectListItems = new SelectList(department.GetAll(), "Id", "CodeAndNarration"),
                SelectSecondListItems = new SelectList(departmentSecond.GetAll(), "Id", "CodeAndNarration"),
            };
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(DepartmentThirdModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDepartmentThird tbl = new TblDepartmentThird();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Create_By = "User";
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Fk_DepartmentIdFirst = masterModel.Fk_DepartmentIdFirst;
                        tbl.Fk_DepartmentIdSecond = masterModel.Fk_DepartmentIdSecond;
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
            var model = new DepartmentThirdModel()
            {
                Id = dt.Id,
                Code = dt.Code,
                IsActive = dt.IsActive,
                Narration = dt.Narration,
                Fk_DepartmentIdFirst = dt.Fk_DepartmentIdFirst,
                Fk_DepartmentIdSecond = dt.Fk_DepartmentIdSecond,
                SelectListItems = new SelectList(department.GetAll(), "Id", "CodeAndNarration"),
                SelectSecondListItems = new SelectList(departmentSecond.GetAll(), "Id", "CodeAndNarration"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentThirdModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDepartmentThird tbl = new TblDepartmentThird();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
                        tbl.Fk_DepartmentIdFirst = masterModel.Fk_DepartmentIdFirst;
                        tbl.Fk_DepartmentIdSecond = masterModel.Fk_DepartmentIdSecond;
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
                TblDepartmentThird tbl = new TblDepartmentThird();
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