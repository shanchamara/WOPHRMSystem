using DocumentFormat.OpenXml.VariantTypes;
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


        public List<VW_LaberUtilizationJObWiseModel> GetAllSummaryLaberUtilizationJObWise(string fromDate, string todate, int fromJObid, int tojobId)
        {
            try
            {

                DateTime Fromdate = DateTime.Parse(fromDate);
                DateTime Todate = DateTime.Parse(todate);

                var dr = (from a in _context.VW_LaberUtilizationJObWise
                          orderby a.JobCode ascending
                          select new VW_LaberUtilizationJObWiseModel()
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
                              CustomerName = a.CustomerName,
                          }).AsNoTracking().ToList();



                var wheredr = dr.Where(a => (a.TrDate >= Fromdate && a.TrDate <= Todate) && (a.Fk_JobMasterId >= fromJObid && a.Fk_JobMasterId <= tojobId)).ToList();

                return wheredr.ToList();


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


        public List<JobMasterForReportModel> GetAllJObListing(bool Ispartners, bool isCompleted, bool Allproject, int fromJObid, int tojobId)
        {
            try
            {

                List<JobMasterForReportModel> lists;


                var dr = (from a in _context.VW_JobMasterViewforReport
                          where (a.Fk_JobMasterId >= fromJObid && a.Fk_JobMasterId <= tojobId)
                          select new JobMasterForReportModel()
                          {
                              BudgetedHours = a.BudgetedHours,
                              CombinedCode = a.CombinedCode,
                              JobCode = a.JobCode,
                              CombinedName = a.CombinedName,
                              CompletedDate = a.CompletedDate,
                              Create_By = a.Create_By,
                              Create_Date = a.Create_Date,
                              CustomerCode = a.CustomerCode,
                              CustomerName = a.CustomerName,
                              Delete_By = a.Delete_By,
                              Delete_Date = a.Delete_Date,
                              DueDate = a.DueDate,
                              Edit_By = a.Edit_By,
                              Edit_Date = a.Edit_Date,
                              FkFromJObId = a.JobmasterId,
                              JobmasterId = a.JobmasterId,
                              JobmasterIsDelete = a.JobmasterIsDelete,
                              PartnerTableId = a.PartnerTableId,
                              PreViewvalue = a.PreViewvalue,
                              Narration = a.Narration,
                              IsReActivate = a.IsReActivate,
                              PartnersIsDelete = a.PartnersIsDelete,
                              ReActivateDate = a.ReActivateDate,
                              StartDate = a.StartDate,
                              IsCompleted = a.IsCompleted,
                              IsActive = a.IsActive,
                              TypeOfTable = a.TypeOfTable,
                              TypeOfTableId = a.TypeOfTableId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              Fk_CustomerId = a.Fk_CustomerId,
                              ManagerCode = a.ManagerCode,
                              ManagerName = a.ManagerName,
                              PartnerCode = a.PartnerCode,
                              PartnerName = a.PartnerName,
                          }).AsNoTracking().ToList();

                lists = dr;


                var fillter = lists.Where(a => Ispartners == true
     ? (a.TypeOfTable == "Partners" && (Allproject || a.IsCompleted == isCompleted))
     : (a.TypeOfTable == "Manager" && (Allproject || a.IsCompleted == isCompleted))).ToList();



                return fillter;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public List<VW_DataEntryEmployeesWiseModel> GetAllDataEntrySheetEmployeesWise(int fromEmployee, int toEmployee)
        {
            try
            {

                var dr = (from a in _context.VW_DataEntryEmployeesWise
                          orderby a.Fk_EmployeeId ascending
                          select new VW_DataEntryEmployeesWiseModel()
                          {
                              Create_By = a.Create_By,
                              Create_Date = a.Create_Date,
                              CustomerName = a.CustomerName,
                              Delete_By = a.Delete_By,
                              Delete_Date = a.Delete_Date,
                              Edit_By = a.Edit_By,
                              Edit_Date = a.Edit_Date,
                              Fk_CustomerId = a.Fk_CustomerId,
                              Fk_EmployeeId = a.Fk_EmployeeId,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              Fk_LocationId = a.Fk_LocationId,
                              Fk_WorkTypeId = a.Fk_WorkTypeId,
                              Hours = a.Hours,
                              Id = a.Id,
                              IsApplyTravelingCost = a.IsApplyTravelingCost,
                              IsDelete = a.IsDelete,
                              Narration = a.Narration,
                              TrDate = a.TrDate,
                              WorkName = a.WorkName,
                              locationsName = a.locationsName,
                              JobCode = a.JobCode,
                          }).AsNoTracking().ToList();


                var wheredr = dr.Where(a => (a.Fk_EmployeeId >= fromEmployee && a.Fk_EmployeeId <= toEmployee)).ToList();

                return wheredr.ToList();


                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<VW_DataEntryDetailsModel> GetAllDataEntryDetails(string fromDate,string todate, bool Ispartners)
        {
            try
            {
                DateTime Fromdate = DateTime.Parse(fromDate);
                DateTime Todate = DateTime.Parse(todate);

                var dr = (from a in _context.VW_DataEntryDetails

                          select new VW_DataEntryDetailsModel()
                          {
                              Hours = a.Hours,
                              IsActive = a.IsActive,
                              Fk_JobMasterId = a.Fk_JobMasterId,
                              Fk_CustomerId = a.Fk_CustomerId,
                              FromDate = Fromdate,
                              CustomerName = a.CustomerName,
                              DueDate = a.DueDate,
                              CustomerCode = a.CustomerCode,
                              CompletedDate = a.CompletedDate,
                              CombinedCode = a.CombinedCode,
                              BudgetedHours = a.BudgetedHours,
                              CombinedName = a.CombinedName,
                              IsCompleted = a.IsCompleted,
                              IsReActivate = a.IsReActivate,
                              JobmasterId = a.JobmasterId,
                              JobmasterIsDelete = a.JobmasterIsDelete,
                              PartnersIsDelete = a.PartnersIsDelete,
                              PreViewvalue = a.PreViewvalue,
                              PartnerTableId = a.PartnerTableId,
                              ReActivateDate = a.ReActivateDate,
                              StartDate = a.StartDate,
                              TransNarration = a.TransNarration,
                              TypeOfTable = a.TypeOfTable,
                              TypeOfTableId = a.TypeOfTableId,
                              Narration = a.Narration,
                              TrDate = a.TrDate,
                              WorkName = a.WorkName,
                              locationsName = a.locationsName,
                              JobCode = a.JobCode,
                          }).AsNoTracking().ToList();



                var wheredr = dr.Where(a => (a.TrDate >= Fromdate && a.TrDate <= Todate)).ToList();



                var fillter = wheredr.Where(a => Ispartners == true
    ? (a.TypeOfTable == "Partners")
    : (a.TypeOfTable == "Manager")).ToList();


                return fillter.ToList();


                //return queryResult;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
