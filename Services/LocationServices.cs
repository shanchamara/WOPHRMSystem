using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class LocationServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblLocation GetByName(string code)
        {
            return _context.TblLocations.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblLocation obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblLocations.Add(obj);
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

        public TblLocation GetById(int Id)
        {
            return _context.TblLocations.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblLocation obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Narration = obj.Narration;
                dbobj.Code = obj.Code;
                dbobj.Fk_CustomerId = obj.Fk_CustomerId;
                dbobj.Rate = obj.Rate;
                dbobj.ToDate = obj.ToDate;
                dbobj.FromDate = obj.FromDate;
                dbobj.Edit_By = obj.Edit_By;
                dbobj.IsActive = obj.IsActive;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;

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

        public MessageModel Delete(TblLocation obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.IsDelete = true;
                dbobj.Delete_By = obj.Delete_By;
                dbobj.Delete_Date = new CommonResources().LocalDatetime().Date;

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

        public List<LocationModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblLocations
                          join c in _context.TblCustomers on a.Fk_CustomerId equals c.Id
                          orderby a.Id descending
                          select new LocationModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Narration = a.Narration,
                              IsActive = a.IsActive,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Rate = a.Rate,
                              CustomerName = c.Name,
                              IsDelete = a.IsDelete,
                              FromDate = a.FromDate,
                              ToDate = a.ToDate,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<LocationModel> GetAllLocationByCustomer(int id)
        {
            try
            {
                var dr = (from a in _context.TblLocations
                          join c in _context.TblCustomers on a.Fk_CustomerId equals c.Id
                          orderby a.Id descending
                          select new LocationModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Narration = a.Narration,
                              IsActive = a.IsActive,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Rate = a.Rate,
                              CustomerName = c.Name,
                              IsDelete = a.IsDelete,
                              FromDate = a.FromDate,
                              ToDate = a.ToDate,
                          }).Where(d => d.IsDelete.Equals(false) && d.Fk_CustomerId.Equals(id)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
