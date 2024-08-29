using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Web.Http;
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

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            var model = new LocationModel()
            {
                CustomerLists = new SelectList(_ClientService2.GetAll(), "Id", "Name"),
                IsActive = true
            };
            return View(model);
        }






        [System.Web.Mvc.HttpPost]
        public ActionResult Create([FromBody] CombinedModel combinedModel)
        {
            try
            {

                TblLocation tbl = new TblLocation()
                {
                    Fk_CustomerId = combinedModel.MasterModel.Fk_CustomerId,
                    Code = combinedModel.MasterModel.Code,
                    Narration = combinedModel.MasterModel.Narration,
                    Create_By = "User",
                    Create_Date = new CommonResources().LocalDatetime().Date,
                    IsActive = true
                };

                return Json(_ClientService.Insert(tbl, combinedModel));

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


        [System.Web.Mvc.HttpGet]
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

            };
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody] CombinedModel combinedModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblLocation tbl = new TblLocation()
                    {
                        Fk_CustomerId = combinedModel.MasterModel.Fk_CustomerId,
                        Code = combinedModel.MasterModel.Code,
                        Narration = combinedModel.MasterModel.Narration,
                        Create_By = "User",
                        Create_Date = new CommonResources().LocalDatetime().Date,
                        Id = combinedModel.MasterModel.Id,
                        IsActive = combinedModel.MasterModel.IsActive,
                    };
                    return Json(_ClientService.Update(tbl, combinedModel));
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

        [System.Web.Mvc.HttpGet]
        public ActionResult DisplayCustomerHasLocation(int id, int locid)
        {
            var dt = _ClientService.GetAllLocationByCustomer(id, locid);
            var model = new ListLocationDetails()
            {
                LocationModels = dt,
            };
            return PartialView("DisplayCustomerHasLocation", model);
        }

    }
}