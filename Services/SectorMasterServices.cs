using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class SectorMasterServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();




        public List<SectorMasterModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.VW_SectorMaster
                          orderby a.Id descending
                          select new SectorMasterModel()
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