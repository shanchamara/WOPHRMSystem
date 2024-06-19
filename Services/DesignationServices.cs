using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class DesignationServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblDesignation GetByName(string code)
        {
            return _context.TblDesignations.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblDesignation obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblDesignations.Add(obj);
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

        public TblDesignation GetById(int Id)
        {
            return _context.TblDesignations.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblDesignation obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Narration = obj.Narration;
                dbobj.Code = obj.Code;
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

        public MessageModel Delete(TblDesignation obj)
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

        public List<DesignationModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblDesignations
                          orderby a.Id descending
                          select new DesignationModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Narration = a.Narration,
                              CodeAndNarration = a.Code + " " + a.Narration,
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

    }
}
