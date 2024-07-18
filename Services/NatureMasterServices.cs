using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace AuditSystem.Services
{
    public class NatureMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        
        public List<NatureMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_NatureMaster
                          orderby a.Id descending
                          select new NatureMasterModel()
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