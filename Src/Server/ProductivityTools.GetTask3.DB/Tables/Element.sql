﻿CREATE TABLE [gt].[Element]
(
	[ElementId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ParentId] INT NULL,
	[Name] VARCHAR(100), 
    [Type] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [Created] DATETIME2 NOT NULL, 
    [Start] DATETIME2 NOT NULL, 
    [Finished] DATETIME2 NULL, 
	CONSTRAINT FK_ParentId FOREIGN KEY ([ParentId]) REFERENCES [gt].[Element](ElementId)
)