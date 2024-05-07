﻿CREATE TABLE [Breed] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[CountryOrigin] NVARCHAR(255)
);


CREATE TABLE [Dog] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[BreedId] INT FOREIGN KEY REFERENCES [Breed](Id)
)