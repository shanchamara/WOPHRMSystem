using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class JobMasterLocationTempServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblJobMasterLocationTemp GetByName(string code)
        {
            return _context.TblJobMasterLocationTemps.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblJobMasterLocationTemp obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblJobMasterLocationTemps.Add(obj);
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

        public TblJobMasterLocationTemp GetById(int Id)
        {
            return _context.TblJobMasterLocationTemps.SingleOrDefault(i => i.Id == Id);
        }

        public void DeleteCurrentlyTemp(string Create_By)
        {
            try
            {
                var recordsToDelete = _context.TblJobMasterLocationTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var record in recordsToDelete)
                {
                    _context.TblJobMasterLocationTemps.Remove(record);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }



        public MessageModel RemoveCustomerSelectedLocation(TblJobMasterLocationTemp obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                _context.TblJobMasterLocationTemps.Remove(dbobj);

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

        public List<LocationModel> GetAllCurrentlyCustomerWiseLocation(int CustomerId)
        {
            try
            {
                var dr = (from a in _context.TblLocations
                          join c in _context.TblCustomers on a.Fk_CustomerId equals c.Id
                          where c.Id == CustomerId
                          orderby a.Id descending
                          select new LocationModel()
                          {

                              Code = a.Code,
                              Narration = a.Narration,
                              IsDelete = a.IsDelete,
                              Id = a.Id,


                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<JobMasterLocationTempModel> GetAllCurrentlySelectedCustomerWiseLocation(int CustomerId, string Createby)
        {
            try
            {
                var dr = (from a in _context.TblJobMasterLocationTemps
                          where a.Create_By == Createby && a.CustomerId == CustomerId
                          orderby a.Id descending
                          select new JobMasterLocationTempModel()
                          {

                              Code = a.Code,
                              Narration = a.Narration,
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


        public void GetLocationForEdit(int JobId, string Create_By)
        {
            try
            {
                var dr = (from a in _context.TblJobMasterLocations
                          join l in _context.TblLocations on a.FK_LocationId equals l.Id
                          where a.Fk_JobMasterId == JobId
                          orderby a.Id descending
                          select new JobMasterLocationTempModel()
                          {

                              Id = a.Id,
                              Code = l.Code,
                              Narration = l.Narration,
                              Create_By = a.Create_By,
                              CustomerId = a.Fk_CustomerId,
                              


                          }).ToList();

                // Clear Data 

                var dt = _context.TblJobMasterLocationTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var d in dt)
                {
                    _context.TblJobMasterLocationTemps.Remove(d);
                    _context.SaveChanges();
                }
                foreach (var d in dr)
                {
                    TblJobMasterLocationTemp tblJobMasterLocationTemp = new TblJobMasterLocationTemp
                    {
                        CustomerId = d.CustomerId,
                        Create_By = Create_By,
                        Code = d.Code,
                        FK_LocationId = d.Id,
                        Narration = d.Narration
                    };
                    _context.TblJobMasterLocationTemps.Add(tblJobMasterLocationTemp);
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
