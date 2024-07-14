using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class NonEffectiveEmployeeServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblNonEffectiveEmployee GetByEmployeeId(int id)
        {
            return _context.TblNonEffectiveEmployees.SingleOrDefault(d => d.Fk_EmployeeId.Equals(id));
        }

        public MessageModel Insert(TblNonEffectiveEmployee obj)
        {

            try
            {
                var data = GetByEmployeeId(obj.Fk_EmployeeId);
                if (data == null)
                {
                    _context.TblNonEffectiveEmployees.Add(obj);
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

        public TblNonEffectiveEmployee GetById(int Id)
        {
            return _context.TblNonEffectiveEmployees.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblNonEffectiveEmployee obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Fk_EmployeeId = obj.Fk_EmployeeId;
                dbobj.NowEffective = obj.NowEffective;
                dbobj.Edit_By = obj.Edit_By;
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

        public MessageModel Delete(TblNonEffectiveEmployee obj)
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

        public List<NonEffectiveEmployeeModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblNonEffectiveEmployees
                          join e in _context.TblEmployees on a.Fk_EmployeeId equals e.Id
                          orderby a.Id descending
                          select new NonEffectiveEmployeeModel()
                          {
                              Id = a.Id,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              NowEffective = a.NowEffective,
                              EmployeeName = a.TblEmployee.Name,
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
