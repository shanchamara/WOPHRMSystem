using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;
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

        public MessageModel Insert(CombinedModel model)
        {

            try
            {
                foreach (var item in model.Rates)
                {
                    var data = GetByName(item.Code);
                    if (data == null)
                    {
                        string myStringfrom = item.FromDate;
                        long Fromtimestamp = long.Parse(myStringfrom);

                        string myStringTo = item.ToDate;
                        long Totimestamp = long.Parse(myStringTo);

                        DateTime FromdateTime = DateTimeOffset.FromUnixTimeMilliseconds(Fromtimestamp).UtcDateTime;
                        DateTime TodateTime = DateTimeOffset.FromUnixTimeMilliseconds(Totimestamp).UtcDateTime;
                        TblLocation tbl = new TblLocation
                        {

                            Rate = item.Rate,
                            FromDate = FromdateTime,
                            ToDate = TodateTime,
                            Fk_CustomerId = item.Fk_CustomerId,
                            Create_By = "User",
                            Code = item.Code,
                            Create_Date = new CommonResources().LocalDatetime().Date,
                            Narration = item.Narration,
                            IsActive = true
                        };
                        _context.TblLocations.Add(tbl);
                        _context.SaveChanges();
                    }
                    else
                    {

                    }
                }
                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record has been registered",
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

        public TblLocation GetById(int Id)
        {
            return _context.TblLocations.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(CombinedModel model)
        {
            try
            {
                foreach (var item in model.Rates)
                {
                    //
                    var dbobj = GetById(item.Id);
                    if (dbobj != null)
                    {
                        dbobj.Narration = item.Narration;
                        dbobj.Code = item.Code;
                        dbobj.Fk_CustomerId = item.Fk_CustomerId;
                        dbobj.Rate = item.Rate;
                        dbobj.Edit_By = "User";
                        dbobj.IsActive = item.IsActive;

                        _context.SaveChanges();
                    }
                    else
                    {
                        var data = GetByName(item.Code);

                        if (data != null)
                        {
                            string myStringfrom = item.FromDate;
                            long Fromtimestamp = long.Parse(myStringfrom);

                            string myStringTo = item.ToDate;
                            long Totimestamp = long.Parse(myStringTo);

                            DateTime FromdateTime = DateTimeOffset.FromUnixTimeMilliseconds(Fromtimestamp).UtcDateTime;
                            DateTime TodateTime = DateTimeOffset.FromUnixTimeMilliseconds(Totimestamp).UtcDateTime;

                            data.Narration = item.Narration;
                            data.Code = item.Code;
                            data.Fk_CustomerId = item.Fk_CustomerId;
                            data.Rate = item.Rate;
                            data.Edit_By = "User";
                            data.FromDate = FromdateTime;
                            data.ToDate = TodateTime;
                            data.IsActive = item.IsActive;

                            _context.SaveChanges();

                        }
                        else
                        {
                            string myStringfrom = item.FromDate;
                            long Fromtimestamp = long.Parse(myStringfrom);

                            string myStringTo = item.ToDate;
                            long Totimestamp = long.Parse(myStringTo);

                            DateTime FromdateTime = DateTimeOffset.FromUnixTimeMilliseconds(Fromtimestamp).UtcDateTime;
                            DateTime TodateTime = DateTimeOffset.FromUnixTimeMilliseconds(Totimestamp).UtcDateTime;
                            TblLocation tbl = new TblLocation
                            {

                                Rate = item.Rate,
                                FromDate = FromdateTime,
                                ToDate = TodateTime,
                                Fk_CustomerId = item.Fk_CustomerId,
                                Create_By = "User",
                                Code = item.Code,
                                Create_Date = new CommonResources().LocalDatetime().Date,
                                Narration = item.Narration,
                                IsActive = true
                            };
                            _context.TblLocations.Add(tbl);
                            _context.SaveChanges();
                        }
                    }
                }
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
