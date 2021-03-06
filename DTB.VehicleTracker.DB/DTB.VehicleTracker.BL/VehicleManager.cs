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
    public static class VehicleManager
    {
        public async static Task<int> Insert(Models.Vehicle vehicle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblVehicle newrow = new tblVehicle();
                    newrow.Id = Guid.NewGuid();
                    newrow.ColorId = vehicle.ColorId;
                    newrow.MakeId = vehicle.MakeId;
                    newrow.ModelId = vehicle.ModelId;
                    newrow.VIN = vehicle.VIN;
                    newrow.Year = vehicle.Year;

                    vehicle.Id = newrow.Id;

                    dc.tblVehicles.Add(newrow);
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

        /*public async static Task<Guid> Insert(int code, string description, bool rollback = false)
        {
            try
            {
                Models.Vehicle vehicle = new Models.Vehicle { Description = description };
                await Insert(vehicle);
                return vehicle.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblVehicles.Remove(row);
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

        public async static Task<int> Update(Models.Vehicle vehicle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Id == vehicle.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.ColorId = vehicle.ColorId;
                        row.ModelId = vehicle.ModelId;
                        row.MakeId = vehicle.MakeId;
                        row.VIN = vehicle.VIN;
                        row.Year = vehicle.Year;

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

        public async static Task<Models.Vehicle> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle tblVehicle = dc.tblVehicles.FirstOrDefault(c => c.Id == id);
                    Models.Vehicle vehicle = new Models.Vehicle();
                    if (tblVehicle != null)
                    {
                        vehicle.ColorId = tblVehicle.ColorId;
                        vehicle.ModelId = tblVehicle.ModelId;
                        vehicle.MakeId = tblVehicle.MakeId;
                        vehicle.VIN = tblVehicle.VIN;
                        vehicle.Year = tblVehicle.Year;
                        vehicle.Id = tblVehicle.Id;
                        vehicle.ColorName = tblVehicle.Color.Description;
                        vehicle.ModelName = tblVehicle.Model.Description;
                        vehicle.MakeName = tblVehicle.Make.Description;
                        return vehicle;
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

        public async static Task<IEnumerable<Models.Vehicle>> Load()
        {
            try
            {
                List<Models.Vehicle> vehicles = new List<Vehicle>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblVehicles.ToList().ForEach(c => vehicles.Add(new Models.Vehicle
                    {
                        Id = c.Id,
                        ColorId = c.ColorId,
                        ModelId = c.ModelId,
                        MakeId = c.MakeId,
                        VIN = c.VIN,
                        Year = c.Year,
                        ColorName = c.Color.Description,
                        MakeName = c.Make.Description,
                        ModelName = c.Model.Description
                    })) ;
                    return vehicles;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async static Task<IEnumerable<Models.Vehicle>> Load(string colorName)
        {
            try
            {
                List<Models.Vehicle> vehicles = new List<Models.Vehicle>();

                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblVehicles
                        .Where(v => v.Color.Description.Contains(colorName))
                        .ToList()
                        .ForEach(c => vehicles.Add(new Models.Vehicle
                        {
                            Id = c.Id,
                            ColorId = c.ColorId,
                            ModelId = c.ModelId,
                            MakeId = c.MakeId,
                            VIN = c.VIN,
                            Year = c.Year,
                            ColorName = c.Color.Description,
                            MakeName = c.Make.Description,
                            ModelName = c.Model.Description
                        }));
                    return vehicles;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
