using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class EmployeeServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblEmployee GetByName(string code, string jobPrefixcode)
        {
            return _context.TblEmployees.SingleOrDefault(d => d.Code.Equals(code) || d.JObPrefixCode.Equals(jobPrefixcode));
        }

        public MessageModel Insert(TblEmployee obj, List<ListEmployeeRate> lists)
        {

            try
            {
                var data = GetByName(obj.Code, obj.JObPrefixCode);
                if (data == null)
                {
                    _context.TblEmployees.Add(obj);
                    _context.SaveChanges();

                    foreach (var item in lists)
                    {
                        TblEmployeeHourlyRate tbl = new TblEmployeeHourlyRate
                        {

                            Rate = item.Rate,
                            FromDate = Convert.ToDateTime(item.FromDate),
                            ToDate = Convert.ToDateTime(item.ToDate),
                            Fk_EmployeeId = obj.Id,
                            Create_By = "User",
                            Fk_DesginationId = item.Fk_DesginationId,
                            Create_Date = new CommonResources().LocalDatetime().Date,
                        };
                        _context.TblEmployeeHourlyRates.Add(tbl);
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
                dbobj.IsPartner = obj.IsPartner;
                dbobj.IsManager = obj.IsManager;
                dbobj.DateOfJoin = obj.DateOfJoin;
                dbobj.Name = obj.Name;
                dbobj.Code = obj.Code;
                dbobj.Edit_By = obj.Edit_By;
                dbobj.IsActive = obj.IsActive;
                dbobj.JObPrefixCode = obj.JObPrefixCode;
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
                              JObPrefixCode = a.JObPrefixCode,
                              DesignationName = a.DepartmentName,
                              DepartmentName = a.DepartmentName,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EmployeeModel> GetAllEmployeeASC()
        {
            try
            {
                var dr = (from a in _context.VW_Employee
                          orderby a.Id ascending
                          select new EmployeeModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Name = a.Name,
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
                              JObPrefixCode = a.JObPrefixCode,
                              DesignationName = a.DepartmentName,
                              DepartmentName = a.DepartmentName,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<EmployeeHourlyRateModel> GetAllEmployeeHourlyRates()
        {
            try
            {
                var dr = (from a in _context.VW_EmployeeHourlyRate
                          orderby a.Id descending
                          select new EmployeeHourlyRateModel()
                          {
                              Id = a.Id,
                              DesignationName = a.DesignationName,
                              Fk_DesginationId = a.Fk_DesginationId,
                              ToDate = a.ToDate,
                              FromDate = a.FromDate,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              IsDelete = a.IsDelete,
                              Rate = a.Rate,
                              EmployeeName = a.EmployeeName,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EmployeeModel> GetAllJobPrefixCode()
        {
            try
            {
                var dr = (from a in _context.VW_Employee
                          orderby a.Id descending
                          select new EmployeeModel()
                          {
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              JObPrefixCode = a.JObPrefixCode,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<EmployeeModel> GetAllIsManager()
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
                          }).Where(d => d.IsDelete.Equals(false) && d.IsManager.Equals(true)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<EmployeeModel> GetAllIsPartner()
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
                          }).Where(d => d.IsDelete.Equals(false) && d.IsPartner.Equals(true)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// Employee Hourly rate 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<EmployeeHourlyRateModel> GetAllEmployeeWiseRateList(int id)
        {
            try
            {
                var dr = (from a in _context.TblEmployeeHourlyRates
                          join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                          where a.Fk_EmployeeId == id
                          orderby a.Id descending
                          select new EmployeeHourlyRateModel()
                          {
                              Id = a.Id,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              FromDate = a.FromDate,
                              Rate = a.Rate,
                              Fk_DesginationId = a.Fk_DesginationId,
                              DesignationName = d.Code + " " + d.Narration,
                              ToDate = a.ToDate,
                              IsDelete = a.IsDelete,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TblEmployeeHourlyRate GetByHourlyRateId(int Id)
        {
            return _context.TblEmployeeHourlyRates.SingleOrDefault(i => i.Id == Id);
        }

        public MessageModel DeleteHourlyRate(TblEmployeeHourlyRate obj)
        {
            try
            {
                var dbobj = GetByHourlyRateId(obj.Id);
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


        public TblEmployeeHourlyRate GetByEmployeeRate(string todateString, string fromdateString)
        {
            //2024-06-08 00:00:00.0000000
            DateTime todate = DateTime.Parse(todateString);
            DateTime fromdate = DateTime.Parse(fromdateString);

            //DateTime specificDate = new DateTime(2024, 6, 08);
            var entities = _context.TblEmployeeHourlyRates.SingleOrDefault(
              (order => order.ToDate.HasValue && order.ToDate.Value == todate && order.FromDate.HasValue && order.FromDate.Value == fromdate));

            return entities;
        }
        public MessageModel InsertRate(TblEmployeeHourlyRate obj)
        {

            try
            {
                var data = GetByEmployeeRate(obj.ToDate.Value.ToString("yyyy/MM/dd"), obj.FromDate.Value.ToString("yyyy/MM/dd"));
                if (data == null)
                {
                    _context.TblEmployeeHourlyRates.Add(obj);
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
