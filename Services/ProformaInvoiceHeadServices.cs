﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class ProformaInvoiceHeadServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public MessageModel Insert(TblProformaInvoiceHead obj)
        {

            try
            {
                var dbobj = GetById(obj.Id);
                if (dbobj == null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {


                        var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblProformaInvoiceHead"));

                        obj.DocNo = "M0000" + Convert.ToString(JobCodePattern.Number + 1);

                        JobCodePattern.Number++;
                        _context.TblProformaInvoiceHeads.Add(obj);
                        _context.SaveChanges();


                        var tempBody = _context.TblProformaInvoiceBodyTemps.Where(d => d.FK_JobMasterId.Equals(obj.Fk_JobMasterId) && d.Create_By.Equals(obj.Create_By)).ToList();
                        foreach (var s in tempBody)
                        {
                            TblProformaInvoiceBody tbl = new TblProformaInvoiceBody
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Fk_JobMasterId,
                                IsDelete = false,
                                Fk_InvoiceNarrttionId = s.Fk_InvoiceNarrttionId,
                                Fk_CustomerId = obj.Fk_CustomerId,
                                Amount = s.Amount,
                                Fk_ProformaInvoiceHeadId = obj.Id
                            };
                            _context.TblProformaInvoiceBodies.Add(tbl);
                            _context.SaveChanges();
                        }

                        scope.Complete();
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

        public TblProformaInvoiceHead GetById(int Id)
        {
            return _context.TblProformaInvoiceHeads.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblProformaInvoiceHead obj)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    _context.TblProformaInvoiceHeads.AddOrUpdate(obj);
                    _context.SaveChanges();


                    var CurrentData = _context.TblProformaInvoiceBodies.Where(d => d.Fk_ProformaInvoiceHeadId.Equals(obj.Id) && d.IsDelete.Equals(false) && d.Fk_JobMasterId.Equals(obj.Fk_JobMasterId)).ToList();
                    foreach (var s in CurrentData)
                    {
                        s.IsDelete = true;
                        s.Delete_Date = new CommonResources().LocalDatetime().Date;
                    }
                    _context.SaveChanges();

                    var tempBody = _context.TblProformaInvoiceBodyTemps.Where(d => d.FK_JobMasterId.Equals(obj.Fk_JobMasterId) && d.Create_By.Equals(obj.Create_By)).ToList();

                    foreach (var s in tempBody)
                    {
                        var Editlocation = _context.TblProformaInvoiceBodies.SingleOrDefault(d => d.Id.Equals(s.BodyRowId) && d.Fk_JobMasterId.Equals(obj.Fk_JobMasterId));
                        if (Editlocation != null)
                        {
                            TblProformaInvoiceBody tbl = new TblProformaInvoiceBody
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Fk_JobMasterId,
                                IsDelete = false,
                                Fk_InvoiceNarrttionId = s.Fk_InvoiceNarrttionId,
                                Fk_CustomerId = obj.Fk_CustomerId,
                                Amount = s.Amount,
                                Fk_ProformaInvoiceHeadId = obj.Id,
                                Id = s.BodyRowId,
                            };
                            _context.TblProformaInvoiceBodies.AddOrUpdate(tbl);
                        }
                        else
                        {
                            TblProformaInvoiceBody tbl = new TblProformaInvoiceBody
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                Fk_JobMasterId = obj.Fk_JobMasterId,
                                IsDelete = false,
                                Fk_InvoiceNarrttionId = s.Fk_InvoiceNarrttionId,
                                Fk_CustomerId = obj.Fk_CustomerId,
                                Amount = s.Amount,
                                Fk_ProformaInvoiceHeadId = obj.Id,

                            };
                            _context.TblProformaInvoiceBodies.AddOrUpdate(tbl);
                        }


                    }

                    scope.Complete();
                }
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

        public MessageModel Delete(TblProformaInvoiceHead obj)
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

        public List<ProformaInvoiceHeadModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_ProformaInvoiceHead
                          orderby a.Id descending
                          where a.IsActive == false
                          select new ProformaInvoiceHeadModel()
                          {
                              Id = a.Id,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              InvoiceNoProforma = a.InvoiceNoProforma,
                              Fk_DepartmentIdOne = a.Fk_DepartmentIdOne,
                              Fk_DepartmentIdTwo = a.Fk_DepartmentIdTwo,
                              Fk_DepartmentIdThird = a.Fk_DepartmentIdThird,
                              DocNo = a.DocNo,
                              Date = a.Date,
                              OurReferance = a.OurReferance,
                              YourReferance = a.YourReferance,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_PartnerOne = a.Fk_PartnerOne,
                              Fk_PartnerSecond = a.Fk_PartnerSecond,
                              Fk_PartnerThird = a.Fk_PartnerThird,
                              Fk_ManagerOne = a.Fk_ManagerOne,
                              Fk_ManagerSecond = a.Fk_ManagerSecond,
                              Fk_ManagerThird = a.Fk_ManagerThird,
                              TaxType = a.TaxType,
                              Fk_NatureId = a.Fk_NatureId,
                              NoNVat = a.NoNVat,
                              NoNVatPrecentage = a.NoNVatPrecentage,
                              VatPercentage = a.VatPercentage,
                              NBTPercentage = a.NBTPercentage,
                              NarrationOne = a.NarrationOne,
                              NarrationTwo = a.NarrationTwo,
                              Fk_InvoiceShortNarrationId = a.Fk_InvoiceShortNarrationId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              LastYearAmount = a.LastYearAmount,
                              PostingDate = a.PostingDate,
                              IsActiveDate = a.IsActiveDate,
                              DepartmentOneName = a.DepartmentOneName,
                              DepartmentTwoName = a.DepartmentTwoName,
                              DepartmentThreeName = a.DepartmentThreeName,
                              WorkGroupName = a.WorkGroupName,
                              CustomerName = a.CustomerName,
                              PartnerOneName = a.PartnerOneName,
                              PartnerTwoName = a.PartnerTwoName,
                              PartnerThreeName = a.PartnerThreeName,
                              ManagerOneName = a.ManagerOneName,
                              ManagerTwoName = a.ManagerTwoName,
                              ManagerThreeName = a.ManagerThreeName,
                              Fk_CompanyId = a.Fk_CompanyId,
                              CompanyName = a.CompanyName,
                              TotalAmount = a.TotalAmount,
                              ValueNBT = a.ValueNBT,
                              ValueVAT = a.ValueVAT,
                              TotalReceivedAmount = a.TotalReceivedAmount,

                              JobCode = a.JobCode,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProformaInvoiceHeadModel> GetAllIspartnerPendingComfirm()
        {
            try
            {
                var dr = (from a in _context.VW_ProformaInvoiceHead
                          orderby a.Id descending
                          where a.IsActive == false && a.IsPartnerOneComfirm == false
                          select new ProformaInvoiceHeadModel()
                          {
                              Id = a.Id,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              InvoiceNoProforma = a.InvoiceNoProforma,
                              Fk_DepartmentIdOne = a.Fk_DepartmentIdOne,
                              Fk_DepartmentIdTwo = a.Fk_DepartmentIdTwo,
                              Fk_DepartmentIdThird = a.Fk_DepartmentIdThird,
                              DocNo = a.DocNo,
                              Date = a.Date,
                              OurReferance = a.OurReferance,
                              YourReferance = a.YourReferance,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_PartnerOne = a.Fk_PartnerOne,
                              Fk_PartnerSecond = a.Fk_PartnerSecond,
                              Fk_PartnerThird = a.Fk_PartnerThird,
                              Fk_ManagerOne = a.Fk_ManagerOne,
                              Fk_ManagerSecond = a.Fk_ManagerSecond,
                              Fk_ManagerThird = a.Fk_ManagerThird,
                              TaxType = a.TaxType,
                              Fk_NatureId = a.Fk_NatureId,
                              NoNVat = a.NoNVat,
                              NoNVatPrecentage = a.NoNVatPrecentage,
                              VatPercentage = a.VatPercentage,
                              NBTPercentage = a.NBTPercentage,
                              NarrationOne = a.NarrationOne,
                              NarrationTwo = a.NarrationTwo,
                              Fk_InvoiceShortNarrationId = a.Fk_InvoiceShortNarrationId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              LastYearAmount = a.LastYearAmount,
                              PostingDate = a.PostingDate,
                              IsActiveDate = a.IsActiveDate,
                              DepartmentOneName = a.DepartmentOneName,
                              DepartmentTwoName = a.DepartmentTwoName,
                              DepartmentThreeName = a.DepartmentThreeName,
                              WorkGroupName = a.WorkGroupName,
                              CustomerName = a.CustomerName,
                              PartnerOneName = a.PartnerOneName,
                              PartnerTwoName = a.PartnerTwoName,
                              PartnerThreeName = a.PartnerThreeName,
                              ManagerOneName = a.ManagerOneName,
                              ManagerTwoName = a.ManagerTwoName,
                              ManagerThreeName = a.ManagerThreeName,
                              Fk_CompanyId = a.Fk_CompanyId,
                              CompanyName = a.CompanyName,
                              TotalAmount = a.TotalAmount,
                              ValueNBT = a.ValueNBT,
                              ValueVAT = a.ValueVAT,
                              TotalReceivedAmount = a.TotalReceivedAmount,

                              JobCode = a.JobCode,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProformaInvoiceHeadModel> GetAllIspartnerComfirmed()
        {
            try
            {
                var dr = (from a in _context.VW_ProformaInvoiceHead
                          orderby a.Id descending
                          where a.IsActive == false && a.IsPartnerOneComfirm == true
                          select new ProformaInvoiceHeadModel()
                          {
                              Id = a.Id,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              InvoiceNoProforma = a.InvoiceNoProforma,
                              Fk_DepartmentIdOne = a.Fk_DepartmentIdOne,
                              Fk_DepartmentIdTwo = a.Fk_DepartmentIdTwo,
                              Fk_DepartmentIdThird = a.Fk_DepartmentIdThird,
                              DocNo = a.DocNo,
                              Date = a.Date,
                              OurReferance = a.OurReferance,
                              YourReferance = a.YourReferance,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_PartnerOne = a.Fk_PartnerOne,
                              Fk_PartnerSecond = a.Fk_PartnerSecond,
                              Fk_PartnerThird = a.Fk_PartnerThird,
                              Fk_ManagerOne = a.Fk_ManagerOne,
                              Fk_ManagerSecond = a.Fk_ManagerSecond,
                              Fk_ManagerThird = a.Fk_ManagerThird,
                              TaxType = a.TaxType,
                              Fk_NatureId = a.Fk_NatureId,
                              NoNVat = a.NoNVat,
                              NoNVatPrecentage = a.NoNVatPrecentage,
                              VatPercentage = a.VatPercentage,
                              NBTPercentage = a.NBTPercentage,
                              NarrationOne = a.NarrationOne,
                              NarrationTwo = a.NarrationTwo,
                              Fk_InvoiceShortNarrationId = a.Fk_InvoiceShortNarrationId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              LastYearAmount = a.LastYearAmount,
                              PostingDate = a.PostingDate,
                              IsActiveDate = a.IsActiveDate,
                              DepartmentOneName = a.DepartmentOneName,
                              DepartmentTwoName = a.DepartmentTwoName,
                              DepartmentThreeName = a.DepartmentThreeName,
                              WorkGroupName = a.WorkGroupName,
                              CustomerName = a.CustomerName,
                              PartnerOneName = a.PartnerOneName,
                              PartnerTwoName = a.PartnerTwoName,
                              PartnerThreeName = a.PartnerThreeName,
                              ManagerOneName = a.ManagerOneName,
                              ManagerTwoName = a.ManagerTwoName,
                              ManagerThreeName = a.ManagerThreeName,
                              Fk_CompanyId = a.Fk_CompanyId,
                              CompanyName = a.CompanyName,
                              TotalAmount = a.TotalAmount,
                              ValueNBT = a.ValueNBT,
                              ValueVAT = a.ValueVAT,
                              TotalReceivedAmount = a.TotalReceivedAmount,

                              JobCode = a.JobCode,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string GetDOCNo()
        {
            try
            {
                var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblProformaInvoiceHead"));
                var makeJobCode = "M0000" + Convert.ToString(JobCodePattern.Number + 1);
                return makeJobCode;

            }
            catch
            {

            }
            return "dd";
        }

    }
}