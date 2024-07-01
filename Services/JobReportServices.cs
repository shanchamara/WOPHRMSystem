﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class JobReportServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();

        /// <summary>
        /// Get All Job Partners wise
        /// Manager wise 
        /// Iscompleted 
        /// IsPending 
        /// </summary>
        /// <returns></returns>
        public List<IGrouping<string, JobMasterForReportModel>> GetAllJobDetails(bool Ispartners, bool Ismanager, bool isCompleted, bool IsPending, string todateString)
        {
            try
            {
                DateTime todate = DateTime.Parse(todateString);

                var groupedData = (from a in _context.VW_JobMasterViewforReport
                                   where /*!a.JobmasterIsDelete && */
                                   Ispartners == true ? (a.TypeOfTable == "Partners" && a.IsCompleted == isCompleted && a.StartDate.Value == todate) : (a.TypeOfTable == "Manager" && a.IsCompleted == isCompleted && a.StartDate.Value == todate)

                                   orderby a.JobmasterId descending
                                   group new JobMasterForReportModel
                                   {
                                       BudgetedHours = a.BudgetedHours,
                                       CombinedCode = a.CombinedCode,
                                       CombinedName = a.CombinedName,
                                       CompletedDate = a.CompletedDate,
                                       Create_By = a.Create_By,
                                       Create_Date = a.Create_Date,
                                       CustomerName = a.CustomerName,
                                       Delete_By = a.Delete_By,
                                       Delete_Date = a.Delete_Date,
                                       DueDate = a.DueDate,
                                       Edit_By = a.Edit_By,
                                       Edit_Date = a.Edit_Date,
                                       Fk_CustomerId = a.Fk_CustomerId,
                                       Fk_JobMasterId = a.Fk_JobMasterId,
                                       JobmasterId = a.JobmasterId,
                                       IsActive = a.IsActive,
                                       IsCompleted = a.IsCompleted,
                                       IsReActivate = a.IsReActivate,
                                       JobCode = a.JobCode,
                                       JobmasterIsDelete = a.JobmasterIsDelete,
                                       Narration = a.Narration,
                                       PartnersIsDelete = a.PartnersIsDelete,
                                       PartnerTableId = a.PartnerTableId,
                                       PreViewvalue = a.PreViewvalue,
                                       ReActivateDate = a.ReActivateDate,
                                       StartDate = a.StartDate,
                                       TypeOfTable = a.TypeOfTable,
                                       CustomerCode = a.CustomerCode,
                                       TypeOfTableId = a.TypeOfTableId,
                                   } by a.CombinedName into grouped
                                   select grouped).ToList();



                return groupedData;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}