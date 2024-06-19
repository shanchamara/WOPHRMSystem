using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;
using System;
using System.Web.Mvc;

namespace WOPHRMSystem.Controllers
{
    public class WorkTypeController : Controller
    {
        readonly WorkTypeServices _ClientService = new WorkTypeServices();
        readonly WorkGroupServices _ClientService2 = new WorkGroupServices();

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new WorkTypeModel() { WorkGroupLists = new SelectList(_ClientService2.GetAll(), "Id", "Code"), };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WorkTypeModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblWorkType tbl = new TblWorkType();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Create_By = "User";
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Create_Date = new CommonResources().LocalDatetime().Date;
                        tbl.Billable = masterModel.Billable;
                        tbl.Fk_WorkGroupId = masterModel.Fk_WorkGroupId;
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
            var model = new WorkTypeModel()
            {
                Id = dt.Id,
                Code = dt.Code,
                IsActive = dt.IsActive,
                Narration = dt.Narration,
                WorkGroupLists = new SelectList(_ClientService2.GetAll(), "Id", "Code"),
                Billable = dt.Billable,
                Fk_WorkGroupId = dt.Fk_WorkGroupId,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(WorkTypeModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblWorkType tbl = new TblWorkType();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Billable = masterModel.Billable;
                        tbl.Fk_WorkGroupId = masterModel.Fk_WorkGroupId;
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
                TblWorkType tbl = new TblWorkType();
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