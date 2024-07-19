using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace AuditSystem.Services
{
    public class InvoiceNarrationMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public List<InvoiceNarrationMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_InvoiceNarrationMaster
                          orderby a.Id descending
                          where !_context.TblProformaInvoiceBodyTemps.Any(b => b.Fk_InvoiceNarrttionId == a.Id)
                          select new InvoiceNarrationMasterModel()
                          {
                              Id = a.Id,
                              CodeAndNarration = a.Code + " " + a.Narration,
                              Code = a.Code,
                              Narration = a.Narration,
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

        #region Proforma Invoice Body

        public List<InvoiceBodyModel> GetAllProformaInvoiceBody(string Create_By)
        {
            try
            {
                var dr = (from a in _context.TblProformaInvoiceBodyTemps
                          join n in _context.TblInvoiceNarrationMasters on a.Fk_InvoiceNarrttionId equals n.Id
                          orderby a.Id descending
                          where a.Create_By == Create_By
                          select new InvoiceBodyModel()
                          {
                              Id = a.Id,
                              Code = n.Code,
                              Narration = n.Narration,
                              FK_CustomerId = a.FK_CustomerId,
                              Amount = a.Amount,
                              BodyRowId = a.BodyRowId,
                              Create_By = a.Create_By,
                              Fk_InvoiceNarrttionId = a.Fk_InvoiceNarrttionId,
                              FK_JobMasterId = a.FK_JobMasterId,
                          }).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal? GetAllProformaInvoiceBodyRowTotalAmount(string Create_By)
        {
            try
            {
                var sumAmount = _context.TblProformaInvoiceBodyTemps
                                .Where(row => row.Create_By == Create_By) // Filter by Create_By if needed
                                .Sum(row => row.Amount); // Summing the Amount property

                return sumAmount;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public TblProformaInvoiceBodyTemp GetById(int Id)
        {
            return _context.TblProformaInvoiceBodyTemps.SingleOrDefault(d => d.Fk_InvoiceNarrttionId.Equals(Id));
        }

        public MessageModel Insert(TblProformaInvoiceBodyTemp obj)
        {

            try
            {
                var data = GetById(obj.Fk_InvoiceNarrttionId);
                if (data == null)
                {
                    _context.TblProformaInvoiceBodyTemps.Add(obj);
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


        public MessageModel Update(TblProformaInvoiceBodyTemp obj)
        {

            try
            {
                var data = GetById(obj.Fk_InvoiceNarrttionId);
                obj.BodyRowId = data.BodyRowId;
                _context.TblProformaInvoiceBodyTemps.AddOrUpdate(obj);
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

        public MessageModel Delete(TblProformaInvoiceBodyTemp obj)
        {
            try
            {
                var dbobj = _context.TblProformaInvoiceBodyTemps.SingleOrDefault(d => d.Id.Equals(obj.Id));
                _context.TblProformaInvoiceBodyTemps.Remove(dbobj);

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

        public void DeleteCurrentlyTemp(string Create_By)
        {
            try
            {
                var recordsToDelete = _context.TblProformaInvoiceBodyTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var record in recordsToDelete)
                {
                    _context.TblProformaInvoiceBodyTemps.Remove(record);
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
                    var recordsToDelete = _context.TblProformaInvoiceBodyTemps.Where(d => d.Create_By == Create_By).ToList();
                    foreach (var record in recordsToDelete)
                    {
                        _context.TblProformaInvoiceBodyTemps.Remove(record);
                    }

                    var get = _context.TblProformaInvoiceBodies.Where(d => d.Fk_ProformaInvoiceHeadId == pInvoiceId && d.IsDelete.Equals(false)).ToList();
                    foreach (var record in get)
                    {
                        TblProformaInvoiceBodyTemp tbl = new TblProformaInvoiceBodyTemp
                        {
                            Create_By = Create_By,
                            BodyRowId = record.Id,
                            FK_JobMasterId = record.Fk_JobMasterId,
                            Amount = record.Amount,
                            FK_CustomerId = record.Fk_CustomerId,
                            Fk_InvoiceNarrttionId = record.Fk_InvoiceNarrttionId
                        };
                        _context.TblProformaInvoiceBodyTemps.Add(tbl);
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