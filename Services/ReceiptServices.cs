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
    public class ReceiptServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();

        #region Headers

        public MessageModel Insert(TblReceipt obj)
        {

            try
            {
                var dbobj = GetById(obj.Id);
                if (dbobj == null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {


                        var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblReceipt"));

                        obj.ReceiptNo = JobCodePattern.Pattern + Convert.ToString(JobCodePattern.Number + 1);

                        JobCodePattern.Number++;
                        _context.TblReceipts.Add(obj);
                        _context.SaveChanges();


                        var tempBody = _context.TblChequeTempDetails.Where(d => d.Create_By.Equals(obj.Create_By)).ToList();
                        foreach (var s in tempBody)
                        {
                            TblChequeDetail tbl = new TblChequeDetail
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                ChequeNo = s.ChequeNo,
                                ReceivedDate = s.ReceivedDate,
                                Amount = s.Amount,
                                TableName = "TblReceipt",
                                TableHeadId = obj.Id,
                                BankDetails = s.BankDetails,
                                CashTookDate = s.CashTookDate,

                            };
                            _context.TblChequeDetails.Add(tbl);
                            _context.SaveChanges();
                        }
                        decimal receiptAmount = Convert.ToDecimal(obj.ReceiptAmount);

                        var get = _context.TblReceiptTempInvoices.Where(d => d.CustomerId == obj.Fk_CustomerId && d.Create_By.Equals(obj.Create_By)).OrderBy(d => d.Id).ToList();
                        foreach (var record in get)
                        {
                            var getSingleInvoice = _context.TblInvoiceHeads.SingleOrDefault(d => d.Id == record.InvoiceId);

                            decimal BalancePayAmount = Convert.ToDecimal(getSingleInvoice.BalanceAmount);

                            if (receiptAmount > BalancePayAmount)
                            {
                                TblReceiptBodydeatil tbl = new TblReceiptBodydeatil
                                {
                                    Create_By = obj.Create_By,
                                    Create_Date = obj.Create_Date,
                                    Fk_InvoiceId = record.InvoiceId,
                                    Fk_ReceiptId = obj.Id,
                                    GrandAmount = getSingleInvoice.TotalReceivedAmount,
                                    BalanceAmount = 0,
                                    PaymentAmount = BalancePayAmount,
                                };
                                _context.TblReceiptBodydeatils.Add(tbl);

                                // Update Invoice Table 
                                getSingleInvoice.BalanceAmount = 0;
                                _context.SaveChanges();

                                receiptAmount -= BalancePayAmount;
                            }
                            else
                            {
                                // Create a receipt detail for the entire invoice amount
                                TblReceiptBodydeatil tbl1 = new TblReceiptBodydeatil
                                {
                                    Create_By = obj.Create_By,
                                    Create_Date = obj.Create_Date,
                                    Fk_InvoiceId = record.InvoiceId,
                                    Fk_ReceiptId = obj.Id,
                                    GrandAmount = getSingleInvoice.TotalReceivedAmount,
                                    BalanceAmount = BalancePayAmount - receiptAmount,
                                    PaymentAmount = receiptAmount,
                                };
                                _context.TblReceiptBodydeatils.Add(tbl1);

                                getSingleInvoice.BalanceAmount = BalancePayAmount - receiptAmount;
                                _context.SaveChanges();
                                receiptAmount -= BalancePayAmount;
                            }
                            // Subtract the invoice amount from the receipt amount
                            



                            if (receiptAmount < 0)
                            {
                                receiptAmount = 0;
                                break;
                            }
                            //_context.TblReceiptBodydeatils.Add(tbl);
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

        public TblReceipt GetById(int Id)
        {
            return _context.TblReceipts.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblReceipt obj)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    _context.TblReceipts.AddOrUpdate(obj);
                    _context.SaveChanges();


                    var CurrentData = _context.TblChequeDetails.Where(d => d.TableHeadId.Equals(obj.Id) && d.IsDelete.Equals(false) && d.TableName.Equals("TblReceipt")).ToList();
                    foreach (var s in CurrentData)
                    {
                        s.IsDelete = true;
                        s.Delete_Date = new CommonResources().LocalDatetime().Date;
                    }
                    _context.SaveChanges();

                    var tempBody = _context.TblChequeTempDetails.Where(d => d.TableHeadId.Equals(obj.Id) && d.TableName.Equals("TblReceipt") && d.Create_By.Equals(obj.Create_By)).ToList();

                    foreach (var s in tempBody)
                    {
                        var Editlocation = _context.TblChequeDetails.SingleOrDefault(d => d.Id.Equals(s.RowId) && d.TableName.Equals("TblReceipt"));
                        if (Editlocation != null)
                        {
                            TblChequeDetail tbl = new TblChequeDetail
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                IsDelete = false,
                                TableName = s.TableName,
                                Amount = s.Amount,
                                TableHeadId = obj.Id,
                                Id = s.RowId,
                                BankDetails = s.BankDetails,
                                CashTookDate = s.CashTookDate,
                                ChequeNo = s.ChequeNo,
                                ReceivedDate = s.ReceivedDate,
                            };
                            _context.TblChequeDetails.AddOrUpdate(tbl);
                        }
                        else
                        {
                            TblChequeDetail tbl = new TblChequeDetail
                            {
                                Create_By = obj.Create_By,
                                Create_Date = obj.Create_Date,
                                IsDelete = false,
                                TableName = s.TableName,
                                Amount = s.Amount,
                                TableHeadId = obj.Id,
                                BankDetails = s.BankDetails,
                                CashTookDate = s.CashTookDate,
                                ChequeNo = s.ChequeNo,
                                ReceivedDate = s.ReceivedDate,
                            };
                            _context.TblChequeDetails.AddOrUpdate(tbl);
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

        public MessageModel Delete(TblReceipt obj)
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

        public List<ReceiptModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_Receipts
                          orderby a.Id descending
                          where a.IsActive == false
                          select new ReceiptModel()
                          {
                              Id = a.Id,
                              CancelDate = a.CancelDate,
                              Date = a.Date,
                              Fk_CompanyId = a.Fk_CompanyId,
                              IsDelete = a.IsDelete,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              IsActive = a.IsActive,
                              Narration = a.Narration,
                              NoNTaxAmount = a.NoNTaxAmount,
                              PaymentType = a.PaymentType,
                              ReceiptAmount = a.ReceiptAmount,
                              ReceiptNo = a.ReceiptNo,
                              CustomerName = a.CustomerName,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<InvoiceHeadModel> GetAllInvoicesforListDown(int customerid)
        {
            try
            {
                var dr = (from a in _context.VW_InvoiceHead
                          orderby a.Id descending
                          where a.IsActive == false && a.Fk_CustomerId == customerid
                          select new InvoiceHeadModel()
                          {
                              Id = a.Id,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              InvoiceNoANDDocNo = a.InvoiceNo + " " + a.DocNo,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              InvoiceNo = a.InvoiceNo,
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
                              BalanceAmount = a.BalanceAmount,
                              JobCode = a.JobCode,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string GeTReceiptNo()
        {
            try
            {
                var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblReceipt"));
                var makeJobCode = JobCodePattern.Pattern + Convert.ToString(JobCodePattern.Number + 1);
                return makeJobCode;

            }
            catch
            {

            }
            return "";
        }


        #endregion



        #region Cheque Body

        public List<ChequeModel> GetAllChequeBody(string Create_By)
        {
            try
            {
                var dr = (from a in _context.TblChequeTempDetails
                          orderby a.Id descending
                          where a.Create_By == Create_By && a.TableName == "TblReceipt"
                          select new ChequeModel()
                          {
                              Id = a.Id,
                              ReceivedDate = a.ReceivedDate,
                              ChequeNo = a.ChequeNo,
                              CashTookDate = a.CashTookDate,
                              BankDetails = a.BankDetails,
                              TableName = a.TableName,
                              Amount = a.Amount,
                              IsActive = a.IsActive,
                              RowId = a.RowId,
                              TableHeadId = a.TableHeadId,
                          }).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal? GetAllChequeBodyRowTotalAmount(string Create_By)
        {
            try
            {
                var sumAmount = _context.TblChequeTempDetails
                                .Where(row => row.Create_By == Create_By && row.TableName == "TblReceipt")
                                .Sum(row => row.Amount) ?? 0;

                return sumAmount;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public TblChequeTempDetail GetChequeById(int Id)
        {
            return _context.TblChequeTempDetails.SingleOrDefault(d => d.Id.Equals(Id));
        }

        public MessageModel Insert(TblChequeTempDetail obj)
        {

            try
            {
                var data = GetChequeById(obj.Id);
                if (data == null)
                {
                    _context.TblChequeTempDetails.Add(obj);
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


        public MessageModel Update(TblChequeTempDetail obj)
        {

            try
            {
                var data = GetChequeById(obj.Id);
                obj.RowId = data.RowId;
                _context.TblChequeTempDetails.AddOrUpdate(obj);
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

        public MessageModel Delete(TblChequeTempDetail obj)
        {
            try
            {
                var dbobj = _context.TblChequeTempDetails.SingleOrDefault(d => d.Id.Equals(obj.Id));
                _context.TblChequeTempDetails.Remove(dbobj);

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

        public void DeleteCurrentlyChequeTemp(string Create_By)
        {
            try
            {
                var recordsToDelete = _context.TblChequeTempDetails.Where(d => d.Create_By == Create_By).ToList();
                foreach (var record in recordsToDelete)
                {
                    _context.TblChequeTempDetails.Remove(record);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteCurrentlyTempAndInsertDataForUpdate(string Create_By, int pInvoiceId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var recordsToDelete = _context.TblChequeTempDetails.Where(d => d.Create_By == Create_By).ToList();
                    foreach (var record in recordsToDelete)
                    {
                        _context.TblChequeTempDetails.Remove(record);
                    }

                    var get = _context.TblChequeDetails.Where(d => d.TableHeadId == pInvoiceId && d.IsDelete.Equals(false)).ToList();
                    foreach (var record in get)
                    {
                        TblChequeTempDetail tbl = new TblChequeTempDetail
                        {
                            Create_By = Create_By,
                            RowId = record.Id,
                            Amount = record.Amount,
                            IsActive = record.IsActive,
                            BankDetails = record.BankDetails,
                            TableHeadId = record.TableHeadId,
                            ReceivedDate = record.ReceivedDate,
                            TableName = record.TableName,
                            CashTookDate = record.CashTookDate,
                            ChequeNo = record.ChequeNo,
                        };
                        _context.TblChequeTempDetails.Add(tbl);
                    }

                    scope.Complete();
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion



        #region Invoices
        public List<InvoiceHeadModel> GetAllInvoices(string Create_By)
        {
            try
            {
                var dr = (from a in _context.VW_InvoiceHead
                          join s in _context.TblReceiptTempInvoices on a.Id equals s.InvoiceId
                          orderby a.Id ascending
                          where a.IsActive == false && s.Create_By == Create_By
                          select new InvoiceHeadModel()
                          {
                              Id = a.Id,
                              IsActive = a.IsActive,
                              IsDelete = a.IsDelete,
                              InvoiceNoANDDocNo = a.InvoiceNo + " " + a.DocNo,
                              Fk_WorkGroupId = a.Fk_WorkGroupId,
                              InvoiceNo = a.InvoiceNo,
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
                              BalanceAmount = a.BalanceAmount,
                              JobCode = a.JobCode,
                          }).Where(d => d.IsDelete.Equals(false)).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TblReceiptTempInvoice GetInvoiceById(int Id)
        {
            return _context.TblReceiptTempInvoices.SingleOrDefault(d => d.InvoiceId.Equals(Id));
        }

        public MessageModel Insert(TblReceiptTempInvoice obj)
        {

            try
            {
                var data = GetInvoiceById(obj.InvoiceId);
                if (data == null)
                {
                    _context.TblReceiptTempInvoices.Add(obj);
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

        public MessageModel Delete(TblReceiptTempInvoice obj)
        {
            try
            {
                var dbobj = _context.TblReceiptTempInvoices.SingleOrDefault(d => d.InvoiceId.Equals(obj.InvoiceId));
                _context.TblReceiptTempInvoices.Remove(dbobj);

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



        public void DeleteCurrentlyInvoiceTemp(string Create_By)
        {
            try
            {
                var recordsToDelete = _context.TblReceiptTempInvoices.Where(d => d.Create_By == Create_By).ToList();
                foreach (var record in recordsToDelete)
                {
                    _context.TblReceiptTempInvoices.Remove(record);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteCurrentlyInvoiceTempAndInsertDataForUpdate(string Create_By, int receiptId, int CustomerId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var recordsToDelete = _context.TblReceiptTempInvoices.Where(d => d.Create_By == Create_By).ToList();
                    foreach (var record in recordsToDelete)
                    {
                        _context.TblReceiptTempInvoices.Remove(record);
                    }

                    var get = _context.TblReceiptBodydeatils.Where(d => d.Fk_ReceiptId == receiptId && d.IsDelete.Equals(false)).ToList();
                    foreach (var record in get)
                    {
                        TblReceiptTempInvoice tbl = new TblReceiptTempInvoice
                        {
                            Create_By = Create_By,
                            InvoiceId = record.Fk_InvoiceId,
                            RowId = record.Id,
                            CustomerId = CustomerId,
                        };
                        _context.TblReceiptTempInvoices.Add(tbl);
                    }

                    scope.Complete();
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

    }
}
