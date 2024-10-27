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


        public List<LaberUtilizationStatementWorkTypeAndGroup> GetAllLaberUtilizationDailyWorkTypeAndGroups(string fromDate, string todate, int fromJObid, int tojobId)
        {
            try
            {
               
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

                //lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                var wheredr = dr.Where(a => (a.TrDate >= Fromdate && a.TrDate <= Todate) && (a.Fk_JobMasterId >= fromJObid && a.Fk_JobMasterId <= tojobId)).ToList();

                return wheredr;

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

        public List<JObWiseCositingDetailsWithAssignEmployeeModel> GetJObWiseCositingDetailsWithAssignEmployee(string fromDate, int JobmasterId)
        {
            try
            {
                List<JObWiseCositingDetailsWithAssignEmployeeModel> lists;
                DateTime Fromdate = DateTime.Parse(fromDate);


                var dr = (from a in _context.VW_JObWiseCositingDetailsWithAssignEmployee
                          orderby a.Fk_JobMasterId ascending
                          where a.Fk_JobMasterId == JobmasterId
                          select new JObWiseCositingDetailsWithAssignEmployeeModel()
                          {
                              ActualValue = a.ActualValue,
                              BudgetedValue = a.BudgetedValue,
                              EmployeeCode = a.EmployeeCode,
                              EmployeeName = a.EmployeeName,
                              EmployeeNo = a.EmployeeNo,
                              ActualHours = a.ActualHours,
                              BudgetedHours = a.BudgetedHours,
                              CompletedDate = a.CompletedDate,
                              CustomerCode = a.CustomerCode,
                              CustomerName = a.CustomerName,
                              DueDate = a.DueDate,
                              EmployeeRateValue = a.EmployeeRateValue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              HoursVarianceValue = a.HoursVarianceValue,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              JobCode = a.JobCode,
                              LocationRate = a.LocationRate,
                              Narration = a.Narration,
                              PartnerTableId = a.PartnerTableId,
                              PreViewvalue = a.PreViewvalue,
                              ReActivateDate = a.ReActivateDate,
                              StartDate = a.StartDate,
                              TypeOfTable = a.TypeOfTable,
                              VarianceValue = a.VarianceValue,
                              TrDate = a.TrDate,

                          }).AsNoTracking().ToList();

                lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                var wheredr = dr.Where(a => a.TrDate <= Fromdate).ToList();

                var finalReSult = lists.Concat(wheredr).ToList();

                var groupedResult = finalReSult
                   .GroupBy(g => g.EmployeeCode)
                   .Select(grp => new JObWiseCositingDetailsWithAssignEmployeeModel
                   {

                       EmployeeCode = grp.Key,
                       EmployeeName = grp.First().EmployeeName,
                       ActualValue = grp.Sum(x => x.ActualValue), // Sum of ActualValue per employee
                       BudgetedValue = grp.First().BudgetedValue, // Sum of BudgetedValue per employee
                       ActualHours = grp.Sum(x => x.ActualHours), // Sum of ActualHours per employee
                       BudgetedHours = grp.First().BudgetedHours, // Sum of BudgetedHours per employee
                       EmployeeRateValue = grp.First().EmployeeRateValue ?? 0,
                       StartDate = grp.First().StartDate,
                       CustomerCode = grp.First().CustomerCode,
                       CustomerName = grp.First().CustomerName,
                       JobCode = grp.First().JobCode
                   })
                   .ToList();

                return groupedResult;
                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<JObWiseCositingDetailsWithAssignEmployeeModel> GetJobCositingDetailsWithAssignEmployeeSummary(string fromDate, bool IsCompleted, bool IsCustomer)
        {
            try
            {
                List<JObWiseCositingDetailsWithAssignEmployeeModel> lists;
                DateTime Fromdate = DateTime.Parse(fromDate);

                // The BDO customer number is defaulting to the first record 1 . 

                var dr = (from a in _context.VW_JObWiseCositingDetailsWithAssignEmployee
                          orderby a.Fk_JobMasterId ascending
                          where a.IsCompleted == IsCompleted && (IsCustomer || a.Fk_CustomerId == 1)
                          select new JObWiseCositingDetailsWithAssignEmployeeModel()
                          {
                              ActualValue = a.ActualValue,
                              BudgetedValue = a.BudgetedValue,
                              EmployeeCode = a.EmployeeCode,
                              EmployeeName = a.EmployeeName,
                              EmployeeNo = a.EmployeeNo,
                              ActualHours = a.ActualHours,
                              BudgetedHours = a.BudgetedHours,
                              CompletedDate = a.CompletedDate,
                              CustomerCode = a.CustomerCode,
                              CustomerName = a.CustomerName,
                              DueDate = a.DueDate,
                              EmployeeRateValue = a.EmployeeRateValue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              HoursVarianceValue = a.HoursVarianceValue,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              JobCode = a.JobCode,
                              LocationRate = a.LocationRate,
                              Narration = a.Narration,
                              PartnerTableId = a.PartnerTableId,
                              PreViewvalue = a.PreViewvalue,
                              ReActivateDate = a.ReActivateDate,
                              StartDate = a.StartDate,
                              TypeOfTable = a.TypeOfTable,
                              VarianceValue = a.VarianceValue,
                              TrDate = a.TrDate,

                          }).AsNoTracking().ToList();

                lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                var wheredr = dr.Where(a => a.TrDate <= Fromdate).ToList();

                var finalReSult = lists.Concat(wheredr).ToList();

                var groupedResult = finalReSult
                   .GroupBy(g => new { g.Fk_JobMasterId, g.CustomerCode, g.EmployeeCode })
                   .Select(grp => new JObWiseCositingDetailsWithAssignEmployeeModel
                   {

                       Fk_JobMasterId = grp.Key.Fk_JobMasterId,
                       EmployeeName = grp.First().EmployeeName,
                       ActualValue = grp.Sum(x => x.ActualValue),
                       BudgetedValue = grp.First().BudgetedValue,
                       ActualHours = grp.Sum(x => x.ActualHours),
                       BudgetedHours = grp.First().BudgetedHours,
                       EmployeeRateValue = grp.First().EmployeeRateValue ?? 0,
                       StartDate = grp.First().StartDate,
                       CustomerCode = grp.First().CustomerCode,
                       CustomerName = grp.First().CustomerName,
                       JobCode = grp.First().JobCode
                   })
                   .ToList();

                return groupedResult;
                // return finalReSult;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<VW_WIPReportDailyAndMonthlyModel> GetReportWIPReportDaily(int JobmasterId)
        {
            try
            {
                List<VW_WIPReportDailyAndMonthlyModel> lists;

                var dr = (from a in _context.VW_WIPReportDailyAndMonthly
                          orderby a.Fk_JobMasterId ascending
                          where a.Fk_JobMasterId.Equals(JobmasterId)
                          select new VW_WIPReportDailyAndMonthlyModel()
                          {
                              ActualValue = a.ActualValue,
                              BudgetedValue = a.BudgetedValue,
                              EmployeeCode = a.EmployeeCode,
                              EmployeeName = a.EmployeeName,
                              EmployeeNo = a.EmployeeNo,
                              ActualHours = a.ActualHours,
                              BudgetedHours = a.BudgetedHours,
                              CompletedDate = a.CompletedDate,
                              CustomerCode = a.CustomerCode,
                              CustomerName = a.CustomerName,
                              DueDate = a.DueDate,
                              EmployeeRateValue = a.EmployeeRateValue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              HoursVarianceValue = a.HoursVarianceValue,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              JobCode = a.JobCode,
                              LocationRate = a.LocationRate,
                              Narration = a.Narration,
                              PartnerTableId = a.PartnerTableId,
                              PreViewvalue = a.PreViewvalue,
                              ReActivateDate = a.ReActivateDate,
                              StartDate = a.StartDate,
                              TypeOfTable = a.TypeOfTable,
                              VarianceValue = a.VarianceValue,
                              TrDate = a.TrDate,
                              Month = a.Month,
                              NarrationTrans = a.NarrationTrans

                          }).AsNoTracking().ToList();

                lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                //var wheredr = dr.Where(a => a.TrDate <= Fromdate).ToList();

                var finalReSult = lists;

                //var groupedResult = finalReSult
                //   .GroupBy(g => new { g.Fk_JobMasterId, g.CustomerCode, g.EmployeeCode })
                //   .Select(grp => new JObWiseCositingDetailsWithAssignEmployeeModel
                //   {

                //       Fk_JobMasterId = grp.Key.Fk_JobMasterId,
                //       EmployeeName = grp.First().EmployeeName,
                //       ActualValue = grp.Sum(x => x.ActualValue),
                //       BudgetedValue = grp.First().BudgetedValue,
                //       ActualHours = grp.Sum(x => x.ActualHours),
                //       BudgetedHours = grp.First().BudgetedHours,
                //       EmployeeRateValue = grp.First().EmployeeRateValue ?? 0,
                //       StartDate = grp.First().StartDate,
                //       CustomerCode = grp.First().CustomerCode,
                //       CustomerName = grp.First().CustomerName,
                //       JobCode = grp.First().JobCode
                //   })
                //   .ToList();

                //return groupedResult;
                return finalReSult;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<VW_WIPReportMonthlyModel> GetReportWIPReportMonthly(int JobmasterId)
        {
            try
            {
                //List<VW_WIPReportMonthlyModel> lists;

                var dr = (from a in _context.VW_WIPReportMonthly
                          orderby a.MonthNumber ascending
                          where a.Fk_JobMasterId.Equals(JobmasterId)
                          select new VW_WIPReportMonthlyModel()
                          {
                              Month = a.Month,
                              StartDate = a.StartDate,
                              Narration = a.Narration,
                              JobCode = a.JobCode,
                              CustomerName = a.CustomerName,
                              DesignationName = a.DesignationName,
                              EmployeeName = a.EmployeeName,
                              EmployeeRateValue = a.EmployeeRateValue,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              JobNarration = a.JobNarration,
                              TotalHours = a.TotalHours,
                              MonthNumber = a.MonthNumber,
                              TotalValue = a.TotalValue,
                          }).AsNoTracking().ToList();

                //lists = dr.Where(d => d.TrDate.Equals(null)).ToList();

                //var wheredr = dr.Where(a => a.TrDate <= Fromdate).ToList();

                //var finalReSult = lists;

                //var groupedResult = dr
                //   .GroupBy(g => new { g.Fk_JobMasterId, g.Fk_CustomerId, g.Fk_EmployeeId, g.Month })
                //   .Select(grp => new VW_WIPReportMonthlyModel
                //   {

                //       Fk_JobMasterId = grp.Key.Fk_JobMasterId,
                //       EmployeeName = grp.First().EmployeeName,
                //       ActualValue = grp.Sum(x => x.ActualValue),
                //       BudgetedValue = grp.First().BudgetedValue,
                //       ActualHours = grp.Sum(x => x.ActualHours),
                //       BudgetedHours = grp.First().BudgetedHours,
                //       EmployeeRateValue = grp.First().EmployeeRateValue ?? 0,
                //       StartDate = grp.First().StartDate,
                //       CustomerCode = grp.First().CustomerCode,
                //       CustomerName = grp.First().CustomerName,
                //       JobCode = grp.First().JobCode
                //   })
                //   .ToList();

                //return groupedResult;
                return dr;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
