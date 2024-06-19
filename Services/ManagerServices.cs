using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class ManagerServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblManager GetByName(string code)
        {
            return _context.TblManagers.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblManager obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblManagers.Add(obj);
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

        public TblManager GetById(int Id)
        {
            return _context.TblManagers.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblManager obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Name = obj.Name;
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

        public MessageModel Delete(TblManager obj)
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

        public List<ManagerModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblManagers
                          orderby a.Id descending
                          select new ManagerModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Name = a.Name,
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
