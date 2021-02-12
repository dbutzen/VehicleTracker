using System;
using System.Collections.Generic;

#nullable disable

namespace DTB.VehicleTracker.PL
{
    public partial class tblVehicle
    {
        public Guid Id { get; set; }
        public Guid ColorId { get; set; }
        public Guid MakeId { get; set; }
        public Guid ModelId { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }

        public virtual tblColor Color { get; set; }
        public virtual tblMake Make { get; set; }
        public virtual tblModel Model { get; set; }
    }
}
