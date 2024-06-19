using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class WorkTypeServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblWorkType GetByName(string code)
        {
            return _context.TblWorkTypes.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblWorkType obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblWorkTypes.Add(obj);
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

        public TblWorkType GetById(int Id)
        {
            return _context.TblWorkTypes.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblWorkType obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Narration = obj.Narration;
                dbobj.Code = obj.Code;
                dbobj.Edit_By = obj.Edit_By;
                dbobj.IsActive = obj.IsActive;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;
                dbobj.Fk_WorkGroupId = obj.Fk_WorkGroupId;
                dbobj.Billable = obj.Billable;

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

        public MessageModel Delete(TblWorkType obj)
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

        public List<WorkTypeModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblWorkTypes
                          join g in _context.TblWorkGroups on a.Fk_WorkGroupId equals g.Id
                          orderby a.Id descending
                          select new WorkTypeModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Narration = a.Narration,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              Billable = a.Billable,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              GroupCode = g.Code
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
