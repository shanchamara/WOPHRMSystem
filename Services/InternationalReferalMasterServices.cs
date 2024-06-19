using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WOPHRMSystem.Services
{
    public class InternationalReferalMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


       
        public List<InternationalReferalMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_InternationalReferalMaster
                          orderby a.Id descending
                          select new InternationalReferalMasterModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Narration = a.Narration,
                              CodeAndNarration = a.Code + " " + a.Narration,
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