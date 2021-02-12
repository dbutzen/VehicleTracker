ALTER TABLE [dbo].[tblVehicle]
	ADD CONSTRAINT [tblVehicle_MakeId]
	FOREIGN KEY (MakeId)
	REFERENCES [tblMake] (Id) ON DELETE CASCADE
