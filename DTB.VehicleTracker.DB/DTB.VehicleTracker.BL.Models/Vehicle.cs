using System;
using System.Collections.Generic;
using System.Text;

namespace DTB.VehicleTracker.BL.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public Guid ColorId { get; set; }
        public Guid MakeId { get; set; }
        public Guid ModelId { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public string ColorName { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }

    }
}
