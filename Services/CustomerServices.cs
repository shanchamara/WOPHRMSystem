using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class CustomerServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblCustomer GetByName(string code)
        {
            return _context.TblCustomers.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblCustomer obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblCustomers.Add(obj);
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

        public TblCustomer GetById(int Id)
        {
            return _context.TblCustomers.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblCustomer obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Name = obj.Name;
                dbobj.Fk_GradeMasterId = obj.Fk_GradeMasterId;
                dbobj.Address = obj.Address;
                dbobj.Code = obj.Code;
                dbobj.ContactPersonOne = obj.ContactPersonOne;
                dbobj.ContactPersonSecond = obj.ContactPersonSecond;
                dbobj.SVatNo = obj.SVatNo;
                dbobj.VatNo = obj.VatNo;
                dbobj.DateOfJoined = obj.DateOfJoined;
                dbobj.Fax = obj.Fax;
                dbobj.Email = obj.Email;
                dbobj.Fk_InternationslReferalId = obj.Fk_InternationslReferalId;
                dbobj.Fk_IntroductionId = obj.Fk_IntroductionId;
                dbobj.Fk_LegalStatusMasterId = obj.Fk_LegalStatusMasterId;
                dbobj.Fk_ManagerId = obj.Fk_ManagerId;
                dbobj.Fk_PartnerId = obj.Fk_PartnerId;
                dbobj.Fk_WorkGroupId = obj.Fk_WorkGroupId;
                dbobj.Fk_SectorMasterId = obj.Fk_SectorMasterId;
                dbobj.TelOne = obj.TelOne;
                dbobj.TelSecond = obj.TelSecond;
                dbobj.VatType = obj.VatType;
                dbobj.Name = obj.Name;
                dbobj.TaxNo = obj.TaxNo;
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

        public MessageModel Delete(TblCustomer obj)
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

        public List<CustomerModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_Customer
                          orderby a.Id descending
                          select new CustomerModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Name = a.Name,
                              TaxNo = a.TaxNo,
                              DateOfJoined = a.DateOfJoined,
                              Address = a.Address,
                              VatType = a.VatType,
                              TelSecond = a.TelSecond,
                              TelOne = a.TelOne,
                              SVatNo = a.SVatNo,
                              Fk_SectorMasterId = a.Fk_SectorMasterId,
                              ContactPersonOne = a.ContactPersonOne,
                              ContactPersonSecond = a.ContactPersonSecond,
                              Email = a.Email,
                              Fax = a.Fax,
                              Fk_GradeMasterId = a.Fk_GradeMasterId,
                              Fk_InternationslReferalId = a.Fk_InternationslReferalId,
                              Fk_IntroductionId = a.Fk_IntroductionId,
                              Fk_LegalStatusMasterId = a.Fk_LegalStatusMasterId,
                              Fk_ManagerId = a.Fk_ManagerId,
                              Fk_PartnerId = a.Fk_PartnerId,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              GradeMasterCode = a.GradeMasterCode,
                              InternationslReferalCode = a.InternationslReferalCode,
                              IntroductionCode = a.IntroductionCode,
                              LegalStatusMasterCode = a.LegalStatusMasterCode,
                              ManagerName = a.ManagerName,
                              Partner = a.Partner,
                              SectorMasterCode = a.SectorMasterCode,
                              WorkGroupCode = a.WorkGroupCode,
                              VatNo = a.VatNo,
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
