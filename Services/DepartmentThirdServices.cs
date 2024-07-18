using System;
using System.Collections.Generic;
using System.Linq;
using WOPHRMSystem.Context;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;

namespace WOPHRMSystem.Services
{
    public class DepartmentThirdServices
    {
        readonly AuditSystemEntities _context = new AuditSystemEntities();


        public TblDepartmentThird GetByName(string code)
        {
            return _context.TblDepartmentThirds.SingleOrDefault(d => d.Code.Equals(code));
        }

        public MessageModel Insert(TblDepartmentThird obj)
        {

            try
            {
                var data = GetByName(obj.Code);
                if (data == null)
                {
                    _context.TblDepartmentThirds.Add(obj);
                    _context.SaveChanges();

                    return new MessageModel()
                    {
                        Status = "success",
                        Text = $"This Record has been registered",
                    };
                }
                else
                {
                    return new MessageModel()
                    {
                        Status = "warning",
                        Text = $"This Record has been already registered",
                    };
                }
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }

        public TblDepartmentThird GetById(int Id)
        {
            return _context.TblDepartmentThirds.SingleOrDefault(i => i.Id == Id);
        }


        public MessageModel Update(TblDepartmentThird obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.Narration = obj.Narration;
                dbobj.Fk_DepartmentIdFirst = obj.Fk_DepartmentIdFirst;
                dbobj.Fk_DepartmentIdSecond = obj.Fk_DepartmentIdSecond;
                dbobj.Code = obj.Code;
                dbobj.Edit_By = obj.Edit_By;
                dbobj.IsActive = obj.IsActive;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;

                _context.SaveChanges();

                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record has been Updated",
                };
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }
        }

        public MessageModel Delete(TblDepartmentThird obj)
        {
            try
            {
                var dbobj = GetById(obj.Id);
                dbobj.IsDelete = true;
                dbobj.Delete_By = obj.Delete_By;
                dbobj.Edit_Date = new CommonResources().LocalDatetime().Date;

                _context.SaveChanges();
                return new MessageModel()
                {
                    Status = "success",
                    Text = $"This Record have been deleted Successfully",
                };
            }
            catch (Exception)
            {
                return new MessageModel()
                {
                    Status = "warning",
                    Text = $"There was a error with retrieving data. Please try again",
                };
            }


        }

        public List<DepartmentThirdModel> GetAll()
        {
            try
            {
                var dr = (from a in _context.TblDepartmentThirds
                          join s in _context.TblDepartments on a.Fk_DepartmentIdFirst equals s.Id
                          join t in _context.TblDepartmentSeconds on a.Fk_DepartmentIdSecond equals t.Id
                          orderby a.Id descending
                          select new DepartmentThirdModel()
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Narration = a.Narration,
                              NarrationFirst = s.Narration,
                              CodeFirst = s.Code,
                              CodeSecond = t.Code,
                              NarrationSecond = t.Narration,
                              Fk_DepartmentIdSecond = t.Id,
                              Fk_DepartmentIdFirst = s.Id,
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



        public List<DepartmentModel> GetDepartmentThirdByDepartmentOneId(int FirstId, int SecondId)
        {
            try
            {



                var department = (from a in _context.TblDepartmentThirds
                                  where a.Fk_DepartmentIdFirst == FirstId && a.Fk_DepartmentIdSecond.Equals(SecondId)  
                                  orderby a.Id descending
                                  select new DepartmentModel()
                                  {
                                      Id = a.Id,
                                      Code = a.Code,
                                      Narration = a.Narration,
                                      IsActive = a.IsActive,
                                      CodeAndNarration = a.Code + " " + a.Narration,
                                      IsDelete = a.IsDelete,
                                  }).Where(d => d.IsDelete.Equals(false)).ToList();

                return department;


            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
