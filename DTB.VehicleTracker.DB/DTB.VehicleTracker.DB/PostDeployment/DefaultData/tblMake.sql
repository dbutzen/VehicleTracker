BEGIN
	INSERT INTO tblMake (Id, Description)
	VALUES
	(NEWID(), 'Ford'),
	(NEWID(), 'Toyota'),
	(NEWID(), 'Chevrolet')
END
