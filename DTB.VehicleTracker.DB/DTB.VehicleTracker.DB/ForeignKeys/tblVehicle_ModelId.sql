ALTER TABLE [dbo].[tblVehicle]
	ADD CONSTRAINT [tblVehicle_ModelId]
	FOREIGN KEY (ModelId)
	REFERENCES [tblModel] (Id)
