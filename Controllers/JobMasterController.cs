using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class JobMasterController : Controller
    {
        readonly JobMasterServices _ClientService = new JobMasterServices();
        readonly CustomerServices _ClientService2 = new CustomerServices();
        readonly EmployeeServices _employeeServices = new EmployeeServices();
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
                DefaultJobCode = _ClientService.GetJoBCode(),
                PartnerSelectListItems = new SelectList(_employeeServices.GetAllIsPartner(), "Id", "Name"),
                ManagerSelectListItems = new SelectList(_employeeServices.GetAllIsManager(), "Id", "Name"),
                IsActive = true
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(JobMasterModel masterModel)
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
                        JObPrefixCode = masterModel.JObPrefixCode,
                        Fk_PartnerId = masterModel.PartnerId,
                        Fk_MangerId = masterModel.ManagerId,
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
                PartnerSelectListItems = new SelectList(_employeeServices.GetAllIsPartner(), "Id", "Name"),
                ManagerSelectListItems = new SelectList(_employeeServices.GetAllIsManager(), "Id", "Name"),
                PartnerId = dt.Fk_PartnerId,
                ManagerId = dt.Fk_MangerId,
                JObPrefixCode = dt.JObPrefixCode
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
                        Edit_By = "User",
                        IsActive = masterModel.IsActive,
                        Create_Date = new CommonResources().LocalDatetime().Date,
                        Id = masterModel.Id,
                        JObPrefixCode = masterModel.JObPrefixCode,
                        Fk_PartnerId = masterModel.PartnerId,
                        Fk_MangerId = masterModel.ManagerId
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
            var model = new ListLocationCustomerWise()
            {
                JobMasterLocationTempModels = data,
                LocationSelectListItems = new SelectList(jobMasterLocationTemp.GetCutomerIdLocation(customerId), "Id", "CodeAndNarration"),
            };
            return PartialView("ViewCustomerSelectedLocation", model);
        }



        [HttpPost]
        public ActionResult InsertSelectedLocation(int locationId, int customerId, string toDate, string fromDate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMasterLocationTemp tbl = new TblJobMasterLocationTemp()
                    {
                        Create_By = "User",
                        FK_LocationId = locationId,
                        CustomerId = customerId,
                        FromDate = Convert.ToDateTime(fromDate),
                        ToDate = Convert.ToDateTime(toDate),
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
        public ActionResult ViewAssignees(string Createby)
        {
            var data = jobMasterAssignTempServices.GetAllCurrentlySelectedAssignees(Createby);
            var model = new ListViewCurrentlyAssignees()
            {
                JobMasterAssignTempModels = data,
                PartnerList = new SelectList(jobMasterAssignTempServices.GetAllCurrentlyMangers_Partners_Employee(), "Id", "CombineName"),
            };
            return PartialView("ViewAssignees", model);
        }



        [HttpPost]
        public ActionResult InsertSelectedAssign(int id, decimal? Hours = 0)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblJobMasterAssignTemp tbl = new TblJobMasterAssignTemp()
                    {
                        Create_By = "User",
                        TypeOftableId = id,
                        BudgetedHours = Hours,
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
        public ActionResult UpdateBudgeet(int TypeOftableId, string TypeOftable, string Name, string Code, string Designation, int Id, decimal BudgetedHours)
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


        // New Mothod 

        [HttpGet]
        public ActionResult GetPartnerCode(int id)
        {
            var customerCode = jobMasterAssignTempServices.GetPartnerCodeDetails(id);  // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetManagerCode(int id)
        {
            var customerCode = jobMasterAssignTempServices.PostManagerDetails(id);  // Your logic to get the customer code

            return Json(customerCode, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}