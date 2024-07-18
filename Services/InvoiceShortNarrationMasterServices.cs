using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace AuditSystem.Services
{
    public class InvoiceShortNarrationMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        
        public List<InvoiceShortNarrationMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_InvoiceShortNarrationMaster
                          orderby a.Id descending
                          select new InvoiceShortNarrationMasterModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              CodeAndNarration = a.Code + " " + a.Narration,
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

    }
}