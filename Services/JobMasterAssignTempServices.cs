using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using Antlr.Runtime;
using Microsoft.Ajax.Utilities;

namespace WOPHRMSystem.Services
{
    public class JobMasterAssignTempServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblJobMasterAssignTemp GetByName(string code)
        {
            return _context.TblJobMasterAssignTemps.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblJobMasterAssignTemp obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblJobMasterAssignTemps.Add(obj);
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


        public MessageModel AddOrUpdate(TblJobMasterAssignTemp obj)
        {

            try
            {
                //var data = GetByName(obj.Code);

                var dt = _context.TblJobMasterAssignTemps.SingleOrDefault(d => d.Id.Equals(obj.Id));
                dt.BudgetedHours = obj.BudgetedHours;
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

        public TblJobMasterAssignTemp GetById(int Id)
        {
            return _context.TblJobMasterAssignTemps.SingleOrDefault(i => i.Id == Id);
        }

        public void DeleteCurrentlyTemp(string Create_By)
        {
            try
            {
                var recordsToDelete = _context.TblJobMasterAssignTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var record in recordsToDelete)
                {
                    _context.TblJobMasterAssignTemps.Remove(record);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }



        public MessageModel RemoveCustomerSelectedAssignee(TblJobMasterAssignTemp obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                _context.TblJobMasterAssignTemps.Remove(dbobj);

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

        public List<JobMasterAssignTempModel> GetAllCurrentlyMangers_Partners_Employee()
        {
            try
            {
                var dr = (from a in _context.TblEmployees
                          join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                          orderby a.Id descending
                          select new JobMasterAssignTempModel()
                          {

                              Code = a.Code,
                              Name = a.Name,
                              TypeOftable = "Employees",
                              Designation = d.Code,
                              TypeOftableId = a.Id,
                              Id = a.Id,



                          }).ToList();

                var partner = (from a in _context.TblPartners
                               select new JobMasterAssignTempModel()
                               {

                                   Code = a.Code,
                                   Name = a.Narration,
                                   TypeOftable = "Partners",

                                   TypeOftableId = a.Id,
                                   Id = a.Id,



                               }).ToList();

                var manager = (from a in _context.TblManagers
                               select new JobMasterAssignTempModel()
                               {

                                   Code = a.Code,
                                   Name = a.Name,
                                   TypeOftable = "Manager",

                                   TypeOftableId = a.Id,
                                   Id = a.Id



                               }).ToList();

                // Combine all three lists
                var combinedList = dr.Concat(partner).Concat(manager).ToList();


                return combinedList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<JobMasterAssignTempModel> GetAllCurrentlySelectedAssignees(int CustomerId, string Createby)
        {
            try
            {
                var dr = (from a in _context.TblJobMasterAssignTemps
                          where a.Create_By == Createby && a.CustomerId == CustomerId
                          orderby a.Id descending
                          select new JobMasterAssignTempModel()
                          {

                              Code = a.Code,
                              Name = a.Name,
                              BudgetedHours = a.BudgetedHours,
                              Designation = a.Designation,
                              TypeOftable = a.TypeOftable,
                              TypeOftableId = a.TypeOftableId,
                              Id = a.Id,
                              Create_By = a.Create_By,
                              CustomerId = CustomerId,

                          }).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void GetPartnerForEdit(int JobId, string Create_By)
        {
            try
            {
                // Clear Data 

                var dt = _context.TblJobMasterAssignTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var d in dt)
                {
                    _context.TblJobMasterAssignTemps.Remove(d);
                    _context.SaveChanges();
                }

                var members = _context.VW_CurrentlyGetJobAssignee.Where(d => d.Fk_JobMasterId == JobId && d.IsDelete.Equals(false)).ToList();
                foreach (var member in members)
                {
                    TblJobMasterAssignTemp tblJobMasterAssignTemp = new TblJobMasterAssignTemp
                    {
                        Code = member.CombinedCode,
                        Create_By = Create_By,
                        BudgetedHours = member.BudgetedHours,
                        CustomerId = member.Fk_CustomerId,
                        Designation = member.DesignationCode,
                        Name = member.CombinedName,
                        TypeOftable = member.TypeOfTable,
                        TypeOftableId = member.TypeOfTableId,
                        RowId = member.Id,
                    };
                    _context.TblJobMasterAssignTemps.Add(tblJobMasterAssignTemp);
                    _context.SaveChanges();
                }




            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
