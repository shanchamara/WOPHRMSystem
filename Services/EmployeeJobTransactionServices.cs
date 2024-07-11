using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class EmployeeJobTransactionServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();
        public List<EmployeeModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_Employee
                          orderby a.Id descending
                          select new EmployeeModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Name = a.Code + "  " + a.Name,
                              IsManager = a.IsManager,
                              IsPartner = a.IsPartner,
                              BirthDay = a.BirthDay,
                              Fk_TitleId = a.Fk_TitleId,
                              Email = a.Email,
                              Nic = a.Nic,
                              Fk_DesginationId = a.Fk_DesginationId,
                              Fk_DepartmentId = a.Fk_DepartmentId,
                              DepartmentCode = a.DepartmentCode,
                              DesignationCode = a.DepartmentCode,
                              DateOfJoin = a.DateOfJoin,
                              titleCode = a.titleCode,
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


        public List<JobMasterCompletedModel> GetEmployeeWiseJob(int empid)
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          join p in _context.TblJobMasterPartners on a.Id equals p.Fk_JobMasterId
                          where p.TypeOfTableId == empid 
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

        public List<EmployeeJobTransactionModel> GetEmployeeWiseTransaction(int empid)
        {
            try
            {
                var dr = (from a in _context.VW_EmployeeJobTransaction
                          where a.Fk_EmployeeId == empid && !a.IsDelete
                          orderby a.Id descending
                          select new EmployeeJobTransactionModel
                          {
                              Id = a.Id,
                              JobCode = a.JobCode,
                              Narration = a.Narration,
                              IsDelete = a.IsDelete,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              CustomerCode = a.CustomerCode,
                              EmployeeName = a.EmployeeName,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              Fk_LocationId = a.Fk_LocationId,
                              Fk_WorkTypeId = a.Fk_WorkTypeId,
                              Hours = a.Hours,
                              LocationCode = a.LocationCode,
                              LocationName = a.LocationName,
                              TrDate = a.TrDate,
                              WorkTypeCode = a.WorkTypeCode
                          }).ToList();



                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MessageModel GetCustomerCode(int jobId)
        {
            try
            {
                var dr = (from a in _context.VW_JobMaster
                          join c in _context.TblCustomers on a.Fk_CustomerId equals c.Id
                          where a.Id == jobId
                          orderby a.Id descending
                          select new JobMasterCompletedModel()
                          {
                              CustomerName = c.Code,
                              Id = c.Id,
                          }).SingleOrDefault();


                return new MessageModel()
                {
                    Status = dr.CustomerName,
                    Text = Convert.ToString(dr.Id),
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetLocationName(int id)
        {
            try
            {
                var dr = (from a in _context.TblLocations
                          where a.Id == id
                          orderby a.Id descending
                          select new LocationModel()
                          {
                              Narration = a.Narration
                          }).SingleOrDefault();


                return dr.Narration;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TblJobTransaction GetByName(int code, int jobid, int empid)
        {
            return _context.TblJobTransactions.SingleOrDefault(d => d.Fk_CustomerId.Equals(code) && d.Fk_JobMasterId.Equals(jobid) && d.Fk_EmployeeId.Equals(empid));
        }

        public MessageModel Insert(TblJobTransaction obj)
        {

            try
            {
                var data = GetByName(obj.Fk_CustomerId, obj.Fk_JobMasterId, obj.Fk_EmployeeId);
                if (data == null)
                {
                    _context.TblJobTransactions.Add(obj);
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
    }
}