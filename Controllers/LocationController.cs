using DocumentFormat.OpenXml.EMMA;
using System;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class LocationController : Controller
    {
        readonly LocationServices _ClientService = new LocationServices();
        readonly CustomerServices _ClientService2 = new CustomerServices();

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new LocationModel()
            {
                CustomerLists = new SelectList(_ClientService2.GetAll(), "Id", "Name"),
                IsActive = true
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LocationModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblLocation tbl = new TblLocation();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Create_By = "User";
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Rate = masterModel.Rate;
                        tbl.Fk_CustomerId = masterModel.Fk_CustomerId;
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
            var model = new LocationModel()
            {
                Id = dt.Id,
                Code = dt.Code,
                IsActive = dt.IsActive,
                Narration = dt.Narration,
                CustomerLists = new SelectList(_ClientService2.GetAll(), "Id", "Name"),
                Fk_CustomerId = dt.Fk_CustomerId,
                Rate = dt.Rate,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(LocationModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblLocation tbl = new TblLocation();
                    {
                        tbl.Narration = masterModel.Narration;
                        tbl.Code = masterModel.Code;
                        tbl.Edit_By = "User";
                        tbl.Id = masterModel.Id;
                        tbl.IsActive = masterModel.IsActive;
                        tbl.Fk_CustomerId = masterModel.Fk_CustomerId;
                        tbl.Rate = masterModel.Rate;
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
                TblLocation tbl = new TblLocation();
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

        [HttpGet]
        public ActionResult DisplayCustomerHasLocation(int id)
        {
            var dt = _ClientService.GetAllLocationByCustomer(id);
            return PartialView("DisplayCustomerHasLocation", dt);
        }


    }
}