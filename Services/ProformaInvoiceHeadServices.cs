using System;
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
                var dbobj = GetById(obj.Id);

                dbobj.InvoiceNoProforma = obj.InvoiceNoProforma;
                dbobj.Fk_DepartmentIdOne = obj.Fk_DepartmentIdOne;
                dbobj.Fk_DepartmentIdTwo = obj.Fk_DepartmentIdTwo;
                dbobj.Fk_DepartmentIdThird = obj.Fk_DepartmentIdThird;
                dbobj.DocNo = obj.DocNo;
                dbobj.Date = obj.Date;
                dbobj.OurReferance = obj.OurReferance;
                dbobj.YourReferance = obj.YourReferance;
                dbobj.Fk_WorkGroupId = obj.Fk_WorkGroupId;
                dbobj.Fk_CustomerId = obj.Fk_CustomerId;
                dbobj.Fk_PartnerOne = obj.Fk_PartnerOne;
                dbobj.Fk_PartnerSecond = obj.Fk_PartnerSecond;
                dbobj.Fk_PartnerThird = obj.Fk_PartnerThird;
                dbobj.Fk_ManagerOne = obj.Fk_ManagerOne;
                dbobj.Fk_ManagerSecond = obj.Fk_ManagerSecond;
                dbobj.Fk_ManagerThird = obj.Fk_ManagerThird;
                dbobj.TaxType = obj.TaxType;
                dbobj.Fk_NatureId = obj.Fk_NatureId;
                dbobj.NoNVat = obj.NoNVat;
                dbobj.NoNVatPrecentage = obj.NoNVatPrecentage;
                dbobj.VatPercentage = obj.VatPercentage;
                dbobj.NBTPercentage = obj.NBTPercentage;
                dbobj.NarrationOne = obj.NarrationOne;
                dbobj.NarrationTwo = obj.NarrationTwo;
                dbobj.Fk_InvoiceShortNarrationId = obj.Fk_InvoiceShortNarrationId;
                dbobj.Fk_JobMasterId = obj.Fk_JobMasterId;
                dbobj.LastYearAmount = obj.LastYearAmount;
                dbobj.PostingDate = obj.PostingDate;
                dbobj.IsActive = obj.IsActive;
                dbobj.IsActiveDate = obj.IsActiveDate;
                dbobj.IsDelete = obj.IsDelete;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;
                dbobj.Edit_By = obj.Edit_By;


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

        public List<ProformaInvoiceHeadModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_ProformaInvoiceHead
                          orderby a.Id descending
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
                              CompanyName = a.CompanyName
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
