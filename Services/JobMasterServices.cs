﻿using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Data.Entity.Migrations;

namespace WOPHRMSystem.Services
{
    public class JobMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblJobMaster GetByName(string code)
        {
            return _context.TblJobMasters.SingleOrDefault(d => d.JobCode.Equals(code));
        }

        public MessageModel Insert(TblJobMaster obj)
        {

            try
            {
                var data = GetByName(obj.JobCode);
                if (data == null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var getJobCodeNumber = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblJobMaster"));

                        obj.JobCode = getJobCodeNumber.Pattern + (getJobCodeNumber.Number + 1);

                        getJobCodeNumber.Number++;
                        _context.TblJobMasters.AddOrUpdate(obj);
                        _context.SaveChanges();

                        var location = _context.TblJobMasterLocationTemps.Where(d => d.CustomerId.Equals(obj.Fk_CustomerId) && d.Create_By.Equals(obj.Create_By)).ToList();
                        foreach (var s in location)
                        {
                            TblJobMasterLocation tblJobMasterLocation = new TblJobMasterLocation
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Id,
                                IsDelete = false,
                                FK_LocationId = s.FK_LocationId,
                                Fk_CustomerId = obj.Fk_CustomerId,

                            };
                            _context.TblJobMasterLocations.AddOrUpdate(tblJobMasterLocation);

                        }

                        var partners = _context.TblJobMasterAssignTemps.Where(d => d.CustomerId.Equals(obj.Fk_CustomerId) && d.Create_By.Equals(obj.Create_By)).ToList();
                        foreach (var s in partners)
                        {
                            TblJobMasterPartner tblJobMasterPartner = new TblJobMasterPartner
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Id,
                                IsDelete = false,
                                TypeOfTable = s.TypeOftable,
                                TypeOfTableId = s.TypeOftableId,
                                Fk_CustomerId = obj.Fk_CustomerId,
                                BudgetedHours = s.BudgetedHours,
                            };
                            _context.TblJobMasterPartners.AddOrUpdate(tblJobMasterPartner);

                        }

                        scope.Complete();
                    }
                    _context.SaveChanges();

                    return new MessageModel()
                    {
                        Status = "success",
                        Text = $"This Record has been registered",
                    };
                }
                else
                {
                    return new MessageModel()
                    {
                        Status = "warning",
                        Text = $"This Record has been already registered",
                    };
                }
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }

        public TblJobMaster GetById(int Id)
        {
            return _context.TblJobMasters.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel UpdateCompltedStatus(TblJobMaster obj)
        {
            try
            {


                var dt = _context.TblJobMasters.SingleOrDefault(d => d.Id.Equals(obj.Id));
                dt.IsCompleted = true;
                dt.CompletedDate = obj.CompletedDate;

                _context.SaveChanges();


                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record has been Updated",
                };
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }


        public MessageModel UpdateReactivateStatus(TblJobMaster obj)
        {
            try
            {


                var dt = _context.TblJobMasters.SingleOrDefault(d => d.Id.Equals(obj.Id));
                dt.IsReActivate = true;
                dt.IsCompleted = false;
                dt.ReActivateDate = obj.ReActivateDate;

                _context.SaveChanges();


                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record has been Updated",
                };
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }

        public MessageModel Update(TblJobMaster obj)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    _context.TblJobMasters.AddOrUpdate(obj);
                    _context.SaveChanges();

                    var location = _context.TblJobMasterLocationTemps.Where(d => d.CustomerId.Equals(obj.Fk_CustomerId) && d.Create_By.Equals(obj.Create_By)).ToList();
                    foreach (var s in location)
                    {
                        var Editlocation = _context.TblJobMasterLocations.SingleOrDefault(d => d.FK_LocationId.Equals(s.FK_LocationId) && d.Fk_JobMasterId.Equals(obj.Id));
                        if (Editlocation == null)
                        {
                            TblJobMasterLocation tblJobMasterLocation = new TblJobMasterLocation
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Id,
                                IsDelete = false,
                                FK_LocationId = s.FK_LocationId,
                                Fk_CustomerId = obj.Fk_CustomerId,

                            };
                            _context.TblJobMasterLocations.AddOrUpdate(tblJobMasterLocation);
                        }

                    }

                    var partners = _context.TblJobMasterAssignTemps.Where(d => d.CustomerId.Equals(obj.Fk_CustomerId) && d.Create_By.Equals(obj.Create_By)).ToList();
                    foreach (var s in partners)
                    {
                        var editpartners = _context.TblJobMasterPartners.SingleOrDefault(d => d.Fk_JobMasterId.Equals(obj.Id) && d.Id.Equals(s.RowId));
                        if (editpartners == null)
                        {
                            TblJobMasterPartner tblJobMasterPartner = new TblJobMasterPartner
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Id,
                                IsDelete = false,
                                TypeOfTable = s.TypeOftable,
                                TypeOfTableId = s.TypeOftableId,
                                Fk_CustomerId = obj.Fk_CustomerId,
                                BudgetedHours = s.BudgetedHours,
                            };
                            _context.TblJobMasterPartners.AddOrUpdate(tblJobMasterPartner);
                        }

                    }

                    scope.Complete();
                }
                _context.SaveChanges();


                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record has been Updated",
                };
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }

        public MessageModel Delete(TblJobMaster obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.IsDelete = true;
                dbobj.Delete_By = obj.Delete_By;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;

                _context.SaveChanges();
                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record have been deleted Successfully",
                };
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }


        }

        public List<JobMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          orderby a.Id descending
                          select new JobMasterModel()
                          {
                              Id = a.Id,
                              JobCode = a.JobCode,
                              Narration = a.Narration,
                              StartDate = a.StartDate,
                              DueDate = a.DueDate,
                              PreViewvalue = a.PreViewvalue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              CustomerName = a.Name,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetJoBCode()
        {
            try
            {
                var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblJobMaster"));
                var makeJobCode = JobCodePattern.Pattern + (JobCodePattern.Number + 1);
                return makeJobCode;

            }
            catch
            {

            }
            return "dd";
        }



        public List<JobMasterCompletedModel> GetAllDropdown()
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          orderby a.Id descending
                          select new JobMasterCompletedModel()
                          {
                              Id = a.Id,
                              JobCode = a.JobCode,
                              Narration = a.Narration,
                              StartDate = a.StartDate,
                              DueDate = a.DueDate,
                              PreViewvalue = a.PreViewvalue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              CustomerName = a.Name,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              CompletedDate = a.CompletedDate,
                              ReActivateDate = a.ReActivateDate,
                          }).Where(d => d.IsDelete.Equals(false) && d.IsCompleted.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<JobMasterCompletedModel> GetAllDropdownForReactivated()
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          orderby a.Id descending
                          select new JobMasterCompletedModel()
                          {
                              Id = a.Id,
                              JobCode = a.JobCode,
                              Narration = a.Narration,
                              StartDate = a.StartDate,
                              DueDate = a.DueDate,
                              PreViewvalue = a.PreViewvalue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              CustomerName = a.Name,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              CompletedDate = a.CompletedDate,
                              ReActivateDate = a.ReActivateDate,
                          }).Where(d => d.IsDelete.Equals(false) && d.IsCompleted.Equals(true)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<JobMasterCompletedModel> GetAllIsCompletedJob()
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          orderby a.Id descending
                          select new JobMasterCompletedModel()
                          {
                              Id = a.Id,
                              JobCode = a.JobCode,
                              Narration = a.Narration,
                              StartDate = a.StartDate,
                              DueDate = a.DueDate,
                              PreViewvalue = a.PreViewvalue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              CustomerName = a.Name,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              CompletedDate = a.CompletedDate,
                              ReActivateDate = a.ReActivateDate,
                          }).Where(d => d.IsDelete.Equals(false) && d.IsCompleted.Equals(true)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<JobMasterCompletedModel> GetAllIsReactivatedJob()
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          orderby a.Id descending
                          select new JobMasterCompletedModel()
                          {
                              Id = a.Id,
                              JobCode = a.JobCode,
                              Narration = a.Narration,
                              StartDate = a.StartDate,
                              DueDate = a.DueDate,
                              PreViewvalue = a.PreViewvalue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              CustomerName = a.Name,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              CompletedDate = a.CompletedDate,
                              ReActivateDate = a.ReActivateDate,
                          }).Where(d => d.IsDelete.Equals(false) && d.IsReActivate.Equals(true)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}