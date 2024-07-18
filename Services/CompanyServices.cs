using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class CompanyServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();

        public List<CompanyModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblCompanies
                          orderby a.Id descending
                          select new CompanyModel()
                          {
                              Id = a.Id,
                              Name = a.Name,

                          }).ToList();
                return dr;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}