ALTER TABLE [dbo].[tblVehicle]
	ADD CONSTRAINT [tblVehicle_ColorId]
	FOREIGN KEY (ColorId)
	REFERENCES [tblColor] (Id) ON DELETE CASCADE
