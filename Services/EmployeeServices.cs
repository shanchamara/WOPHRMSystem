using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class EmployeeServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblEmployee GetByName(string code)
        {
            return _context.TblEmployees.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblEmployee obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblEmployees.Add(obj);
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

        public TblEmployee GetById(int Id)
        {
            return _context.TblEmployees.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblEmployee obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Fk_DepartmentId = obj.Fk_DepartmentId;
                dbobj.Fk_DesginationId = obj.Fk_DesginationId;
                dbobj.Nic = obj.Nic;
                dbobj.Email = obj.Email;
                dbobj.Fk_TitleId = obj.Fk_TitleId;
                dbobj.BirthDay = obj.BirthDay;
                dbobj.IdManager = obj.IdManager;
                dbobj.DateOfJoin = obj.DateOfJoin;
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

        public MessageModel Delete(TblEmployee obj)
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
                              Name = a.Name,
                              IdManager = a.IdManager,
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

    }
}
