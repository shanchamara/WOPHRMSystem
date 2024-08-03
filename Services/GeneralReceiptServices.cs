using DocumentFormat.OpenXml.Drawing;
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
    public class GeneralReceiptServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();

        #region Headers

        public MessageModel Insert(TblGenaralReceipt obj)
        {

            try
            {
                var dbobj = GetById(obj.Id);
                if (dbobj == null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {


                        var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblGeneralReceipt"));

                        obj.ReceiptNo = JobCodePattern.Pattern + Convert.ToString(JobCodePattern.Number + 1);

                        JobCodePattern.Number++;
                        _context.TblGenaralReceipts.Add(obj);
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
                                TableName = "TblGeneralReceipt",
                                TableHeadId = obj.Id,
                                BankDetails = s.BankDetails,
                                CashTookDate = s.CashTookDate,

                            };
                            _context.TblChequeDetails.Add(tbl);
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

        public TblGenaralReceipt GetById(int Id)
        {
            return _context.TblGenaralReceipts.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblGenaralReceipt obj)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    _context.TblGenaralReceipts.AddOrUpdate(obj);
                    _context.SaveChanges();


                    var CurrentData = _context.TblChequeDetails.Where(d => d.TableHeadId.Equals(obj.Id) && d.IsDelete.Equals(false) && d.TableName.Equals("TblGeneralReceipt")).ToList();
                    foreach (var s in CurrentData)
                    {
                        s.IsDelete = true;
                        s.Delete_Date = new CommonResources().LocalDatetime().Date;
                    }
                    _context.SaveChanges();



                    var tempBody = _context.TblChequeTempDetails.Where(d => d.TableName.Equals("TblGeneralReceipt") && d.Create_By.Equals(obj.Edit_By)).ToList();

                    foreach (var s in tempBody)
                    {
                        var Editlocation = _context.TblChequeDetails.SingleOrDefault(d => d.Id.Equals(s.RowId) && d.TableName.Equals("TblGeneralReceipt"));
                        if (Editlocation != null)
                        {
                            TblChequeDetail tbl = new TblChequeDetail
                            {
                                Edit_By = obj.Edit_By,
                                Edit_Date = obj.Edit_Date,
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
                                Edit_By = obj.Edit_By,
                                Edit_Date = obj.Edit_Date,
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

        public MessageModel Delete(TblGenaralReceipt obj)
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

        public List<GeneralReceiptModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_GeneralReceipts
                          orderby a.Id descending
                          where a.IsActive == false
                          select new GeneralReceiptModel()
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
                              PaymentType = a.PaymentType,
                              ReceiptAmount = a.ReceiptAmount,
                              ReceiptNo = a.ReceiptNo,
                              CustomerName = a.CustomerName,
                              AccountName = a.AccountName,
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
                var JobCodePattern = _context.TblDocuments.SingleOrDefault(d => d.TypeOfTable.Equals("TblGeneralReceipt"));
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
                          where a.Create_By == Create_By && a.TableName == "TblGeneralReceipt"
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
                                .Where(row => row.Create_By == Create_By && row.TableName == "TblGeneralReceipt")
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

                    var get = _context.TblChequeDetails.Where(d => d.TableHeadId == pInvoiceId && d.IsDelete.Equals(false) && d.TableName.Equals("TblGeneralReceipt")).ToList();
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

    }
}
