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
    public static class ModelManager
    {
        public async static Task<int> Insert(Models.Model model, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblModel newrow = new tblModel();
                    newrow.Id = Guid.NewGuid();
                    newrow.Description = model.Description;

                    model.Id = newrow.Id;

                    dc.tblModels.Add(newrow);
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
                Models.Model model = new Models.Model { Description = description };
                await Insert(model);
                return model.Id;
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
                    tblModel row = dc.tblModels.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblModels.Remove(row);
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

        public async static Task<int> Update(Models.Model model, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblModel row = dc.tblModels.FirstOrDefault(c => c.Id == model.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Description = model.Description;

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

        public async static Task<Models.Model> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblModel tblModel = dc.tblModels.FirstOrDefault(c => c.Id == id);
                    Models.Model model = new Models.Model();
                    if (tblModel != null)
                    {
                        model.Description = tblModel.Description;
                        model.Id = tblModel.Id;
                        return model;
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

        public async static Task<IEnumerable<Models.Model>> Load()
        {
            try
            {
                List<Models.Model> models = new List<Model>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblModels.ToList().ForEach(c => models.Add(new Models.Model
                    {
                        Id = c.Id,
                        Description = c.Description
                    }));
                    return models;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
