using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class AccountServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();

        public List<AccountModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblAccounts
                          orderby a.Id descending
                          select new AccountModel()
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