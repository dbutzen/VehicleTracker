BEGIN
	DECLARE @ColorId uniqueidentifier;
	SELECT @ColorId = Id from tblColor where Description = 'Hunter Green' 

	DECLARE @MakeId uniqueidentifier;
	SELECT @MakeId = Id from tblMake where Description = 'Ford'

	DECLARE @ModelId uniqueidentifier;
	SELECT @ModelId = Id from tblModel where Description = 'Mustang'

	INSERT INTO DBO.tblVehicle (Id, ColorId, MakeId, ModelId, Year, VIN)
	VALUES
	(NEWID(), @ColorId, @MakeId, @ModelId, 1966, '1Q2W3E4R5T6Y77U8I9') 

	SELECT @ColorId = Id from tblColor where Description = 'Rebecca Purple' 

	SELECT @MakeId = Id from tblMake where Description = 'Chevrolet'

	SELECT @ModelId = Id from tblModel where Description = 'Camaro'

	INSERT INTO DBO.tblVehicle (Id, ColorId, MakeId, ModelId, Year, VIN)
	VALUES
	(NEWID(), @ColorId, @MakeId, @ModelId, 1978, '4R54T3EFDG68IUY') 
END