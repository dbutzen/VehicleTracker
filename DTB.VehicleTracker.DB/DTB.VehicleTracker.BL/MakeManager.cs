using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.VehicleTracker.BL.Models;
using DTB.VehicleTracker.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DTB.VehicleTracker.BL
{
    public static class MakeManager
    {
        public async static Task<int> Insert(Models.Make make, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMake newrow = new tblMake();
                    newrow.Id = Guid.NewGuid();
                    newrow.Description = make.Description;

                    make.Id = newrow.Id;

                    dc.tblMakes.Add(newrow);
                    int results = dc.SaveChanges();
                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<Guid> Insert(int code, string description, bool rollback = false)
        {
            try
            {
                Models.Make make = new Models.Make { Description = description };
                await Insert(make);
                return make.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblMake row = dc.tblMakes.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblMakes.Remove(row);
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async static Task<int> Update(Models.Make make, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblMake row = dc.tblMakes.FirstOrDefault(c => c.Id == make.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Description = make.Description;

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<Models.Make> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblMake tblMake = dc.tblMakes.FirstOrDefault(c => c.Id == id);
                    Models.Make make = new Models.Make();
                    if (tblMake != null)
                    {
                        make.Description = tblMake.Description;
                        make.Id = tblMake.Id;
                        return make;
                    }
                    else
                    {
                        throw new Exception("Could not find row.");
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<IEnumerable<Models.Make>> Load()
        {
            try
            {
                List<Models.Make> makes = new List<Make>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblMakes.ToList().ForEach(c => makes.Add(new Models.Make
                    {
                        Id = c.Id,
                        Description = c.Description
                    }));
                    return makes;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
