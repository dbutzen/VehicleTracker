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
    public static class ColorManager
    {
        public async static Task<int> Insert(Models.Color color, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblColor newrow = new tblColor();
                    newrow.Id = Guid.NewGuid();
                    newrow.Code = color.Code;
                    newrow.Description = color.Description;

                    color.Id = newrow.Id;

                    dc.tblColors.Add(newrow);
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

        public async static Task<Guid> Insert( int code, string description, bool rollback = false)
        {
            try
            {
                Models.Color color = new Models.Color { Code = code, Description = description };
                await Insert(color);
                return color.Id;
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
                    tblColor row = dc.tblColors.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblColors.Remove(row);
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

        public async static Task<int> Update(Models.Color color, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblColor row = dc.tblColors.FirstOrDefault(c => c.Id == color.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Code = color.Code;
                        row.Description = color.Description;

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

        public async static Task<Models.Color> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblColor tblColor = dc.tblColors.FirstOrDefault(c => c.Id == id);
                    Models.Color color = new Models.Color();
                    if (tblColor != null)
                    {
                        color.Code = tblColor.Code;
                        color.Description = tblColor.Description;
                        color.Id = tblColor.Id;
                        return color;
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

        public async static Task<IEnumerable<Models.Color>> Load()
        {
            try
            {
                List<Models.Color> colors = new List<Color>();
                using(VehicleEntities dc = new VehicleEntities()){
                    dc.tblColors.ToList().ForEach(c => colors.Add(new Models.Color 
                    { 
                        Id = c.Id,
                        Code = c.Code,
                        Description = c.Description
                    }));
                    return colors;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
