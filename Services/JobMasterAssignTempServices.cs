using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class JobMasterAssignTempServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();



        public MessageModelTwo GetPartnerCodeDetails(int id)
        {
            try
            {
                var Prifixcode = "";
                using (TransactionScope scope = new TransactionScope())
                {

                    var checkOldPartners = _context.TblJobMasterAssignTemps.Where(d => d.TypeOftable.Equals("Partners")).ToList();

                    foreach (var d in checkOldPartners)
                    {
                        if (d.IsProjectOnwer == true)
                        {
                            _context.TblJobMasterAssignTemps.Remove(d);
                        }
                        else
                        {
                            d.IsProjectOnwer = false;
                        }
                        _context.SaveChanges();
                    }


                    var dr = (from a in _context.TblEmployees
                              join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                              where a.Id == id
                              orderby a.Id descending
                              select new JobMasterAssignTempModel()
                              {
                                  Name = a.Name,
                                  Code = a.Code,
                                  TypeOftable = "Partners",
                                  TypeOftableId = id,
                                  Designation = d.Code,
                                  Create_By = a.JObPrefixCode

                              }).SingleOrDefault();

                    Prifixcode = dr.Create_By;

                    TblJobMasterAssignTemp tbl = new TblJobMasterAssignTemp()
                    {
                        Create_By = "User",
                        TypeOftableId = dr.TypeOftableId,
                        TypeOftable = dr.TypeOftable,
                        Name = dr.Name,
                        Code = dr.Code,
                        Designation = dr.Designation,
                        IsProjectOnwer = true
                    };
                    _context.TblJobMasterAssignTemps.Add(tbl);


                    scope.Complete();


                }
                _context.SaveChanges();

                return new MessageModelTwo()
                {
                    Status = "success",
                    Text = $"This Record has been registered",
                    Value = Prifixcode
                };
            }
            catch (Exception)
            {
                return new MessageModelTwo()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }

        public MessageModel PostManagerDetails(int id)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    var checkOldPartners = _context.TblJobMasterAssignTemps.Where(d => d.TypeOftable.Equals("Manager")).ToList();

                    foreach (var d in checkOldPartners)
                    {
                        if (d.IsProjectOnwer == true)
                        {
                            _context.TblJobMasterAssignTemps.Remove(d);
                        }
                        else
                        {
                            d.IsProjectOnwer = false;
                        }
                        _context.SaveChanges();
                    }


                    var dr = (from a in _context.TblEmployees
                              join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                              where a.Id == id
                              orderby a.Id descending
                              select new JobMasterAssignTempModel()
                              {
                                  Name = a.Name,
                                  Code = a.Code,
                                  TypeOftable = "Manager",
                                  TypeOftableId = id,
                                  Designation = d.Code,
                                  Create_By = a.JObPrefixCode

                              }).SingleOrDefault();


                    TblJobMasterAssignTemp tbl = new TblJobMasterAssignTemp()
                    {
                        Create_By = "User",
                        TypeOftableId = dr.TypeOftableId,
                        TypeOftable = dr.TypeOftable,
                        Name = dr.Name,
                        Code = dr.Code,
                        Designation = dr.Designation,
                        IsProjectOnwer = true
                    };
                    _context.TblJobMasterAssignTemps.Add(tbl);


                    scope.Complete();


                }
                _context.SaveChanges();

                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record has been registered",
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



        public TblJobMasterAssignTemp GetByName(string code)
        {
            return _context.TblJobMasterAssignTemps.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblJobMasterAssignTemp obj)
        {

            try
            {

                var dr = (from a in _context.TblEmployees
                          join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                          where a.Id == obj.TypeOftableId
                          orderby a.Id descending
                          select new JobMasterAssignTempModel()
                          {
                              Name = a.Name,
                              Code = a.Code,
                              TypeOftableId = obj.TypeOftableId,
                              Designation = d.Code,
                              Create_By = a.JObPrefixCode,
                              IsPartner = a.IsPartner,
                              IsManager = a.IsManager,

                          }).SingleOrDefault();

                obj.TypeOftable = dr.IsManager == true ? "Manager" : dr.IsPartner == true ? "Partners" : "Employees";
                obj.Name = dr.Name;
                obj.Code = dr.Code;
                obj.Designation = dr.Designation;
                obj.IsProjectOnwer = false;

                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblJobMasterAssignTemps.Add(obj);
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


        public MessageModel AddOrUpdate(TblJobMasterAssignTemp obj)
        {

            try
            {
                //var data = GetByName(obj.Code);

                var dt = _context.TblJobMasterAssignTemps.SingleOrDefault(d => d.Id.Equals(obj.Id));
                dt.BudgetedHours = obj.BudgetedHours;
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

        public TblJobMasterAssignTemp GetById(int Id)
        {
            return _context.TblJobMasterAssignTemps.SingleOrDefault(i => i.Id == Id);
        }

        public void DeleteCurrentlyTemp(string Create_By)
        {
            try
            {
                var recordsToDelete = _context.TblJobMasterAssignTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var record in recordsToDelete)
                {
                    _context.TblJobMasterAssignTemps.Remove(record);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }



        public MessageModel RemoveCustomerSelectedAssignee(TblJobMasterAssignTemp obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                _context.TblJobMasterAssignTemps.Remove(dbobj);

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

        public List<JobMasterAssignTempModel> GetAllCurrentlyMangers_Partners_Employee()
        {
            try
            {
                var dr = (from a in _context.TblEmployees
                          join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id

                          where !_context.TblJobMasterAssignTemps.Any(b => b.TypeOftableId == a.Id) && a.IsManager == false && a.IsPartner == false
                          //.Where(a => !_context.YourEntityB.Any(b => b.SomeProperty == a.SomeProperty))
                          select new JobMasterAssignTempModel()
                          {

                              Code = a.Code,
                              Name = a.Name,
                              TypeOftable = "Employees",
                              Designation = d.Code,
                              TypeOftableId = a.Id,
                              Id = a.Id,
                              CombineName = "Employee Name :" + a.Name + " , " + a.Code + " , " + "  Designation " + d.Code


                          }).ToList();

                var partner = (from a in _context.TblEmployees
                               join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                               where a.IsPartner == true && !_context.TblJobMasterAssignTemps.Any(b => b.TypeOftableId == a.Id)
                               select new JobMasterAssignTempModel()
                               {

                                   Code = a.Code,
                                   Name = a.Name,
                                   TypeOftable = "Partners",
                                   TypeOftableId = a.Id,
                                   Id = a.Id,
                                   CombineName = "Partner Name :" + a.Name + " , " + a.Code + " , " + "  Designation " + d.Code


                               }).ToList();

                var manager = (from a in _context.TblEmployees
                               join d in _context.TblDesignations on a.Fk_DesginationId equals d.Id
                               where !_context.TblJobMasterAssignTemps.Any(b => b.TypeOftableId == a.Id) && a.IsManager == true
                               select new JobMasterAssignTempModel()
                               {

                                   Code = a.Code,
                                   Name = a.Name,
                                   TypeOftable = "Manager",
                                   TypeOftableId = a.Id,
                                   Id = a.Id,
                                   CombineName = "Manager Name :" + a.Name + " , " + a.Code + " , " + "  Designation " + d.Code


                               }).ToList();

                // Combine all three lists
                var combinedList = dr.Concat(partner).Concat(manager).ToList();


                return combinedList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<JobMasterAssignTempModel> GetAllCurrentlySelectedAssignees(string Createby)
        {
            try
            {
                var dr = (from a in _context.TblJobMasterAssignTemps
                          where a.Create_By == Createby
                          orderby a.Id descending
                          select new JobMasterAssignTempModel()
                          {

                              Code = a.Code,
                              Name = a.Name,
                              BudgetedHours = a.BudgetedHours,
                              Designation = a.Designation,
                              TypeOftable = a.TypeOftable,
                              TypeOftableId = a.TypeOftableId,
                              Id = a.Id,
                              Create_By = a.Create_By,

                          }).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void GetPartnerForEdit(int JobId, string Create_By)
        {
            try
            {
                // Clear Data 

                var dt = _context.TblJobMasterAssignTemps.Where(d => d.Create_By == Create_By).ToList();
                foreach (var d in dt)
                {
                    _context.TblJobMasterAssignTemps.Remove(d);
                    _context.SaveChanges();
                }

                var members = _context.VW_CurrentlyGetJobAssignee.Where(d => d.Fk_JobMasterId == JobId && d.IsDelete.Equals(false)).ToList();
                foreach (var member in members)
                {
                    TblJobMasterAssignTemp tblJobMasterAssignTemp = new TblJobMasterAssignTemp
                    {
                        Code = member.CombinedCode,
                        Create_By = Create_By,
                        BudgetedHours = member.BudgetedHours,
                        Designation = member.DesignationCode,
                        Name = member.CombinedName,
                        TypeOftable = member.TypeOfTable,
                        TypeOftableId = member.TypeOfTableId,
                        RowId = member.Id,
                        IsProjectOnwer = member.IsProjectOnwer
                    };
                    _context.TblJobMasterAssignTemps.Add(tblJobMasterAssignTemp);
                    _context.SaveChanges();
                }




            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
