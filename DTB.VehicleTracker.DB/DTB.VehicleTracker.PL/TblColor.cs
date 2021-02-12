using System;
using System.Collections.Generic;

#nullable disable

namespace DTB.VehicleTracker.PL
{
    public partial class tblColor
    {
        public tblColor()
        {
            tblVehicles = new HashSet<tblVehicle>();
        }

        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblVehicle> tblVehicles { get; set; }
    }
}
