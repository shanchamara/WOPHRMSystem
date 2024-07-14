using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class GradeMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();




        public List<GradeMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_GradeMaster
                          orderby a.Id descending
                          select new GradeMasterModel()
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