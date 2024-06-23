using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;
using System;
using System.Web.Mvc;

namespace WOPHRMSystem.Controllers
{
    public class JobMasterController : Controller
    {
        readonly JobMasterServices _ClientService = new JobMasterServices();
        readonly CustomerServices _ClientService2 = new CustomerServices();
        readonly JobMasterLocationTempServices jobMasterLocationTemp = new JobMasterLocationTempServices();
        readonly JobMasterAssignTempServices jobMasterAssignTempServices = new JobMasterAssignTempServices();

        #region HeadOfJObMaster 

        [HttpGet]
        public ActionResult Index()
        {
            var dt = _ClientService.GetAll();
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            jobMasterLocationTemp.DeleteCurrentlyTemp("User");
            jobMasterAssignTempServices.DeleteCurrentlyTemp("User");

            var model = new JobMasterModel()
            {
                CustomerSelectListItems = new SelectList(_ClientService2.GetAll(), "Id", "Name"),
                JobCode = _ClientService.GetJoBCode()

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(JobMasterModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMaster tbl = new TblJobMaster
                    {
                        Narration = masterModel.Narration,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        PreViewvalue = masterModel.PreViewvalue,
                        DueDate = masterModel.DueDate,
                        StartDate = masterModel.StartDate,
                        JobCode = masterModel.JobCode,
                        Create_By = "User",
                        IsActive = masterModel.IsActive,
                        Create_Date = new CommonResources().LocalDatetime().Date,
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
            var model = new JobMasterModel()
            {
                Id = dt.Id,
                JobCode = dt.JobCode,
                DueDate = dt.DueDate,
                Fk_CustomerId = dt.Fk_CustomerId,
                PreViewvalue = dt.PreViewvalue,
                StartDate = dt.StartDate,
                IsActive = dt.IsActive,
                Narration = dt.Narration,
                CustomerSelectListItems = new SelectList(_ClientService2.GetAll(), "Id", "Name"),
            };
            jobMasterLocationTemp.GetLocationForEdit(Id, "User");
            jobMasterAssignTempServices.GetPartnerForEdit(Id, "User");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(JobMasterModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMaster tbl = new TblJobMaster
                    {
                        Narration = masterModel.Narration,
                        Fk_CustomerId = masterModel.Fk_CustomerId,
                        PreViewvalue = masterModel.PreViewvalue,
                        DueDate = masterModel.DueDate,
                        StartDate = masterModel.StartDate,
                        JobCode = masterModel.JobCode,
                        Create_By = "User",
                        IsActive = masterModel.IsActive,
                        Create_Date = new CommonResources().LocalDatetime().Date,
                        Id = masterModel.Id,
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
                TblJobMaster tbl = new TblJobMaster();
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


        #region IsComplted 


        [HttpGet]
        public ActionResult CompltedIndex()
        {
            var dt = _ClientService.GetAllIsCompletedJob();
            return View(dt);
        }


        [HttpGet]
        public ActionResult Complted()
        {
            
            var model = new JobMasterCompletedModel()
            {
                JobList = new SelectList(_ClientService.GetAllDropdown(), "Id", "JobCode"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Complted(JobMasterCompletedModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMaster tbl = new TblJobMaster
                    {
                        Id = masterModel.Id,
                        IsCompleted = masterModel.IsCompleted,
                        CompletedDate = new CommonResources().LocalDatetime().Date,
                    };

                    return Json(_ClientService.UpdateCompltedStatus(tbl));
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

        #region IsReactivate


        [HttpGet]
        public ActionResult ReactivateIndex()
        {
            var dt = _ClientService.GetAllIsReactivatedJob();
            return View(dt);
        }


        [HttpGet]
        public ActionResult Reactivate()
        {

            var model = new JobMasterCompletedModel()
            {
                JobList = new SelectList(_ClientService.GetAllDropdownForReactivated(), "Id", "JobCode"),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Reactivate(JobMasterCompletedModel masterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMaster tbl = new TblJobMaster
                    {
                        Id = masterModel.Id,
                        IsReActivate = masterModel.IsCompleted,
                        ReActivateDate = new CommonResources().LocalDatetime().Date,
                    };

                    return Json(_ClientService.UpdateReactivateStatus(tbl));
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

        #region CustomerLocationView 

        [HttpGet]
        public ActionResult ViewCustomerSelectedLocation(int customerId, string Createby)
        {
            var data = jobMasterLocationTemp.GetAllCurrentlySelectedCustomerWiseLocation(customerId, Createby);
            var model = new ListLocationCustomerWise() { JobMasterLocationTempModels = data };
            return PartialView("ViewCustomerSelectedLocation", model);
        }

        [HttpGet]
        public ActionResult ViewCustomerCurrentlyLocation(int customerId)
        {
            var data = jobMasterLocationTemp.GetAllCurrentlyCustomerWiseLocation(customerId);
            var model = new ListCurrentlyCustomerLocation() { LocationModels = data };
            return PartialView("ViewCustomerCurrentlyLocation", model);
        }

        [HttpPost]
        public ActionResult InsertSelectedLocation(string Narration, string code, int locationId, int customerId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMasterLocationTemp tbl = new TblJobMasterLocationTemp()
                    {
                        Create_By = "User",
                        Narration = Narration,
                        Code = code,
                        FK_LocationId = locationId,
                        CustomerId = customerId
                    };

                    return Json(jobMasterLocationTemp.Insert(tbl));
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
        public ActionResult Removelocation(int ID)
        {
            try
            {
                TblJobMasterLocationTemp tbl = new TblJobMasterLocationTemp
                {
                    Id = ID

                };
                return Json(jobMasterLocationTemp.RemoveCustomerSelectedLocation(tbl));
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


        #region AssignEmployee 

        [HttpGet]
        public ActionResult ViewAssignees(int customerId, string Createby)
        {
            var data = jobMasterAssignTempServices.GetAllCurrentlySelectedAssignees(customerId, Createby);
            var model = new ListViewCurrentlyAssignees() { JobMasterAssignTempModels = data };
            return PartialView("ViewAssignees", model);
        }

        [HttpGet]
        public ActionResult ViewAssigneeByManagerPartnerEmployee(int customerId)
        {
            var data = jobMasterAssignTempServices.GetAllCurrentlyMangers_Partners_Employee();
            var model = new ListViewCurrentlyAssignees() { JobMasterAssignTempModels = data };
            return PartialView("ViewAssigneeByManagerPartnerEmployee", model);
        }

        [HttpPost]
        public ActionResult InsertSelectedAssign(int TypeOftableId, string TypeOftable, string Name, string Code, string Designation, int CustomerId, decimal BudgetedHours)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMasterAssignTemp tbl = new TblJobMasterAssignTemp()
                    {
                        Create_By = "User",
                        TypeOftableId = TypeOftableId,
                        TypeOftable = TypeOftable,
                        Name = Name,
                        Code = Code,
                        Designation = Designation,
                        CustomerId = CustomerId,
                        BudgetedHours = BudgetedHours,
                    };

                    return Json(jobMasterAssignTempServices.Insert(tbl));
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
        public ActionResult RemoveAssignee(int ID)
        {
            try
            {
                TblJobMasterAssignTemp tbl = new TblJobMasterAssignTemp
                {
                    Id = ID

                };
                return Json(jobMasterAssignTempServices.RemoveCustomerSelectedAssignee(tbl));
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


        [HttpPost]
        public ActionResult UpdateBudgeet(int TypeOftableId, string TypeOftable, string Name, string Code, string Designation, int CustomerId, int Id, decimal BudgetedHours)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMasterAssignTemp tbl = new TblJobMasterAssignTemp()
                    {
                        Create_By = "User",
                        TypeOftableId = TypeOftableId,
                        TypeOftable = TypeOftable,
                        Name = Name,
                        Code = Code,
                        Designation = Designation,
                        CustomerId = CustomerId,
                        BudgetedHours = BudgetedHours,
                        Id = Id,
                    };



                    return Json(jobMasterAssignTempServices.AddOrUpdate(tbl));
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