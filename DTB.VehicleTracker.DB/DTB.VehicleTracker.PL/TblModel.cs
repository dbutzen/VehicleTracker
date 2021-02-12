using System;
using System.Collections.Generic;

#nullable disable

namespace DTB.VehicleTracker.PL
{
    public partial class tblModel
    {
        public tblModel()
        {
            tblVehicles = new HashSet<tblVehicle>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblVehicle> tblVehicles { get; set; }
    }
}
