using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class ReportServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();



        public List<LaberUtilizationStatementWorkTypeAndGroup> GetAll(string fromDate, string todate, int fromEmployee, int toEmployee)
        {
            try
            {
                List<LaberUtilizationStatementWorkTypeAndGroup> lists;
                DateTime Fromdate = DateTime.Parse(fromDate);
                DateTime Todate = DateTime.Parse(todate);

                var dr = (from a in _context.VW_LaberUtilizationStatementWorkTypeAndGroup
                          orderby a.Fk_EmployeeId ascending
                          select new LaberUtilizationStatementWorkTypeAndGroup()
                          {
                              TransactionId = a.TransactionId,
                              EmployeeName = a.EmployeeName,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              Fk_LocationId = a.Fk_LocationId,
                              Fk_WorkTypeId = a.Fk_WorkTypeId,
                              Groups = a.Groups,
                              IsApplyTravelingCost = a.IsApplyTravelingCost,
                              Narration = a.Narration,
                              TrDate = a.TrDate,
                              WorkingHours = a.WorkingHours,
                              Worktypes = a.Worktypes

                          }).AsNoTracking().ToList();

                lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                var wheredr = dr.Where(a => (a.TrDate >= Fromdate && a.TrDate <= Todate) && (a.Fk_EmployeeId >= fromEmployee && a.Fk_EmployeeId <= toEmployee)).ToList();

                return lists.Concat(wheredr).ToList();


                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<LaberUtilizationStatementWorkTypeAndGroup> GetAllSummary(string fromDate, string todate, int fromJObid, int tojobId)
        {
            try
            {
                List<LaberUtilizationStatementWorkTypeAndGroup> lists;
                DateTime Fromdate = DateTime.Parse(fromDate);
                DateTime Todate = DateTime.Parse(todate);

                var dr = (from a in _context.VW_LaberUtilizationSummaryWorkTypeAndGroup
                          orderby a.JobCode ascending
                          select new LaberUtilizationStatementWorkTypeAndGroup()
                          {
                              TransactionId = a.TransactionId,
                              EmployeeName = a.EmployeeName,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              Fk_LocationId = a.Fk_LocationId,
                              Fk_WorkTypeId = a.Fk_WorkTypeId,
                              Groups = a.Groups,
                              IsApplyTravelingCost = a.IsApplyTravelingCost,
                              Narration = a.Narration,
                              TrDate = a.TrDate,
                              WorkingHours = a.WorkingHours,
                              Worktypes = a.Worktypes,
                              JobCode = a.JobCode,

                          }).AsNoTracking().ToList();

                lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                var wheredr = dr.Where(a => (a.TrDate >= Fromdate && a.TrDate <= Todate) && (a.Fk_JobMasterId >= fromJObid && a.Fk_JobMasterId <= tojobId)).ToList();

                return lists.Concat(wheredr).ToList();


                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VW_CustomerLocationRatesModel> GetAllCustomersLocationRates()
        {
            try
            {


                var dr = (from a in _context.VW_CustomerLocationRates
                          orderby a.LocationNumber ascending
                          select new VW_CustomerLocationRatesModel()
                          {
                              CustomerName = a.CustomerName,
                              Fk_LocatonId = a.Fk_LocatonId,
                              FromDate = a.FromDate,
                              ToDate = a.ToDate,
                              Id = a.Id,
                              LocationName = a.LocationName,
                              Rate = a.Rate,
                              Address = a.Address,
                              LocationNumber = a.LocationNumber,
                          }).AsNoTracking().ToList();


                return dr;


                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<VW_EmployeeVisitingRatesModel> GetEmployeeVisitingLocationRate(string fromDate, string todate)
        {
            try
            {
                List<VW_EmployeeVisitingRatesModel> lists;
                DateTime Fromdate = DateTime.Parse(fromDate);
                DateTime Todate = DateTime.Parse(todate);

                var dr = (from a in _context.VW_EmployeeVisitingRates
                          orderby a.Fk_EmployeeId ascending
                          select new VW_EmployeeVisitingRatesModel()
                          {
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              EmployeeName = a.EmployeeName,
                              FkFromEmployeeId = a.Fk_EmployeeId,
                              FkToEmployeeId = a.Fk_EmployeeId,
                              Fk_LocationId = a.Fk_EmployeeId,
                              IsApplyTravelingCost = a.IsApplyTravelingCost,
                              IsDelete = a.IsDelete,
                              LocationName = a.LocationName,
                              Rate = a.Rate,
                              Id = a.Id,
                              TrDate = a.TrDate,

                          }).AsNoTracking().ToList();

                lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                var wheredr = dr.Where(a => (a.TrDate >= Fromdate && a.TrDate <= Todate)).ToList();

                return lists.Concat(wheredr).ToList();


                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
