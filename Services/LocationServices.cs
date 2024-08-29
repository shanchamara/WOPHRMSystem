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

        public MessageModel Insert(TblLocation tblLocation, CombinedModel model)
        {

            try
            {
                var data = GetByName(model.MasterModel.Code);
                if (data == null)
                {
                    _context.TblLocations.Add(tblLocation);
                    _context.SaveChanges();


                    foreach (var item in model.Rates)
                    {
                       
                        TblLocationRate tbl = new TblLocationRate
                        {

                            Rate = item.Rate,
                            FromDate = Convert.ToDateTime(item.FromDate),
                            ToDate = Convert.ToDateTime(item.ToDate),
                            Fk_LocatonId = tblLocation.Id,
                        };
                        _context.TblLocationRates.Add(tbl);
                        _context.SaveChanges();

                    }


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


        public MessageModel Update(TblLocation tbl, CombinedModel model)
        {
            try
            {
                var dbobj = GetById(tbl.Id);
                dbobj.Narration = tbl.Narration;
                dbobj.Code = tbl.Code;
                dbobj.Edit_By = tbl.Edit_By;
                dbobj.IsActive = tbl.IsActive;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;

                _context.SaveChanges();


                foreach (var item in model.Rates)
                {
                    var ishave = _context.TblLocationRates.SingleOrDefault(d => d.Id == item.Id);
                    if (ishave == null)
                    {
                        TblLocationRate tblrate = new TblLocationRate
                        {

                            Rate = item.Rate,
                            FromDate = Convert.ToDateTime(item.FromDate),
                            ToDate = Convert.ToDateTime(item.ToDate),
                            Fk_LocatonId = tbl.Id,
                        };

                        _context.TblLocationRates.Add(tblrate);
                        _context.SaveChanges();
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
                              CustomerName = c.Name,
                              IsDelete = a.IsDelete,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RateModel> GetAllLocationByCustomer(int id, int locid)
        {
            try
            {
                var dr = (from a in _context.TblLocationRates
                          join c in _context.TblLocations on a.Fk_LocatonId equals c.Id
                          orderby a.Id descending
                          select new
                          {
                              a.Id,
                              a.Rate,
                              a.Fk_LocatonId,
                              c.Fk_CustomerId,
                              a.ToDate,
                              a.FromDate
                          })
           .Where(d => d.Fk_CustomerId.Equals(id) && d.Fk_LocatonId.Equals(locid))
           .AsEnumerable()  // Executes the query and brings the data into memory
           .Select(x => new RateModel()
           {
               Id = x.Id,
               Rate = x.Rate,
               Fk_LocatonId = x.Fk_LocatonId,
               Fk_CustomerId = x.Fk_CustomerId,
               ToDate = x.ToDate.HasValue ? x.ToDate.Value.ToString("yyyy-MM-dd") : "",
               FromDate = x.FromDate.HasValue ? x.FromDate.Value.ToString("yyyy-MM-dd") : "",
           })
           .ToList();

                return dr;


            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
